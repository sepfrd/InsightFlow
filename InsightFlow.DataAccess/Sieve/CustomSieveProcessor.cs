using System.Security.Claims;
using InsightFlow.Common.Constants;
using InsightFlow.DataAccess.Sieve.SieveConfigurations;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Sieve.Models;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve;

public class CustomSieveProcessor : SieveProcessor
{
    private readonly ILogger _logger;
    private readonly string? _userRole;

    private readonly string[] _idPhrases =
    [
        nameof(BaseEntity.Id),
        nameof(Answer) + nameof(BaseEntity.Id),
        nameof(Profile) + nameof(BaseEntity.Id),
        nameof(ProfileImage) + nameof(BaseEntity.Id),
        nameof(Question) + nameof(BaseEntity.Id),
        nameof(Role) + nameof(BaseEntity.Id),
        nameof(User) + nameof(BaseEntity.Id),
        nameof(UserRole) + nameof(BaseEntity.Id)
    ];

    public CustomSieveProcessor(
        IOptions<SieveOptions> options,
        IHttpContextAccessor httpContextAccessor,
        ILogger logger)
        : base(options)
    {
        _logger = logger;
        _userRole = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper) =>
        mapper
            .ApplyConfiguration<AnswerSieveConfiguration>()
            .ApplyConfiguration<QuestionSieveConfiguration>()
            .ApplyConfiguration<UserSieveConfiguration>();

    protected override IQueryable<TEntity> ApplyFiltering<TEntity>(SieveModel model, IQueryable<TEntity> result, object[]? dataForCustomMethods = null)
    {
        try
        {
            var filters = model.GetFiltersParsed();

            if (filters.IsNullOrEmpty())
            {
                return result;
            }

            if (_userRole is not null && _userRole == ApplicationConstants.AdminRoleName)
            {
                return base.ApplyFiltering(model, result);
            }

            var finalFilters = filters.Where(filter =>
                    !_idPhrases.Intersect(filter.Names).Any() &&
                    !filter.Names.Any(filterName => filterName.Contains(".id", StringComparison.InvariantCultureIgnoreCase)))
                .ToList();

            if (finalFilters.Count == 0)
            {
                return result.Except(result);
            }

            var finalStringFilters = finalFilters.Select(filterTerm =>
                {
                    var names = filterTerm.Names.Length == 1 ? filterTerm.Names.First() : '(' + string.Join('|', filterTerm.Names) + ')';
                    var values = filterTerm.Values.Length == 1 ? filterTerm.Values.First() : string.Join('|', filterTerm.Values);

                    return $"{names}{filterTerm.Operator}{values}";
                }
            );

            model.Filters = string.Join(',', finalStringFilters);

            return base.ApplyFiltering(model, result);
        }
        catch (Exception exception)
        {
            _logger.Error(exception, exception.Message);

            return result.Except(result);
        }
    }
}