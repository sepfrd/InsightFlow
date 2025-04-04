using System.Linq.Expressions;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Common;

public abstract class FilterDtoBase
{
    protected FilterDtoBase(
        DateTime? createdAtStartDate = null,
        DateTime? createdAtEndDate = null,
        DateTime? updatedAtStartDate = null,
        DateTime? updatedAtEndDate = null)
    {
        CreatedAtStartDate = createdAtStartDate;
        CreatedAtEndDate = createdAtEndDate;
        UpdatedAtStartDate = updatedAtStartDate;
        UpdatedAtEndDate = updatedAtEndDate;
    }

    public DateTime? CreatedAtStartDate { get; init; }

    public DateTime? CreatedAtEndDate { get; init; }

    public DateTime? UpdatedAtStartDate { get; init; }

    public DateTime? UpdatedAtEndDate { get; init; }

    protected Expression? ToBaseExpression(ParameterExpression parameterExpression)
    {
        var expressions = new List<Expression>();

        if (CreatedAtStartDate is not null)
        {
            var createdAtMember = Expression.Property(parameterExpression, nameof(DomainEntity.CreatedAt));
            var createdAtStartDateConstant = Expression.Constant(CreatedAtStartDate.Value);

            expressions.Add(Expression.GreaterThanOrEqual(createdAtMember, createdAtStartDateConstant));
        }

        if (CreatedAtEndDate is not null)
        {
            var createdAtMember = Expression.Property(parameterExpression, nameof(DomainEntity.CreatedAt));
            var createdAtEndDateConstant = Expression.Constant(CreatedAtEndDate.Value);

            expressions.Add(Expression.LessThanOrEqual(createdAtMember, createdAtEndDateConstant));
        }

        if (UpdatedAtStartDate is not null)
        {
            var updatedAtMember = Expression.Property(parameterExpression, nameof(DomainEntity.UpdatedAt));
            var updatedAtStartDateConstant = Expression.Constant(UpdatedAtStartDate.Value);

            expressions.Add(Expression.GreaterThanOrEqual(updatedAtMember, updatedAtStartDateConstant));
        }

        if (UpdatedAtEndDate is not null)
        {
            var updatedAtMember = Expression.Property(parameterExpression, nameof(DomainEntity.UpdatedAt));
            var updatedAtEndDateConstant = Expression.Constant(UpdatedAtEndDate.Value);

            expressions.Add(Expression.LessThanOrEqual(updatedAtMember, updatedAtEndDateConstant));
        }

        Expression? baseExpression = null;

        foreach (var expressionItem in expressions)
        {
            baseExpression = baseExpression switch
            {
                null => expressionItem,
                _ => Expression.AndAlso(baseExpression, expressionItem)
            };
        }

        return baseExpression;
    }
}