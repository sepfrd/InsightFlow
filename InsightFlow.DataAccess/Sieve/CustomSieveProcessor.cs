using System.Security.Claims;
using InsightFlow.Common.Constants;
using InsightFlow.DataAccess.Sieve.SieveConfigurations;
using InsightFlow.Model.BaseEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve;

public class CustomSieveProcessor : SieveProcessor
{
    private readonly string? _userRole;

    public CustomSieveProcessor(IOptions<SieveOptions> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _userRole = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper) =>
        mapper
            .ApplyConfiguration<AnswerSieveConfiguration>()
            .ApplyConfiguration<AnswerSieveConfiguration>()
            .ApplyConfiguration<BookmarkSieveConfiguration>()
            .ApplyConfiguration<PersonSieveConfiguration>()
            .ApplyConfiguration<ProfileSieveConfiguration>()
            .ApplyConfiguration<QuestionSieveConfiguration>()
            .ApplyConfiguration<QuestionVoteSieveConfiguration>()
            .ApplyConfiguration<RoleSieveConfiguration>()
            .ApplyConfiguration<UserRoleSieveConfiguration>()
            .ApplyConfiguration<UserSieveConfiguration>();

    protected override IQueryable<TEntity> ApplyFiltering<TEntity>(SieveModel model, IQueryable<TEntity> result, object[]? dataForCustomMethods = null)
    {
        if (_userRole is null || _userRole != ApplicationConstants.AdminRoleName)
        {
            model.Filters = string.Join(
                ',',
                model.Filters
                    .Split(',')
                    .Where(filter => !filter.Contains(nameof(BaseEntity.Id))));
        }

        return base.ApplyFiltering(model, result);
    }
}