using System.Linq.Expressions;
using Humanizer;
using InsightFlow.Application.Common;
using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.Features.BlogPosts.Dtos;

public class BlogPostFilterDto : FilterDtoBase<BlogPost>
{
    public string? Title { get; init; }

    public string? Body { get; init; }

    public Guid? AuthorUuid { get; init; }

    public Expression<Func<BlogPost, bool>>? ToExpression()
    {
        var blogPost = Expression.Parameter(typeof(BlogPost), nameof(BlogPost).Camelize());

        var expressions = new List<Expression>();

        var parentDtoExpression = ToBaseExpression(blogPost);

        if (parentDtoExpression is not null)
        {
            expressions.Add(parentDtoExpression);
        }

        if (!string.IsNullOrWhiteSpace(Title))
        {
            var titleMember = Expression.Property(blogPost, nameof(BlogPost.Title));
            var titleConstant = Expression.Constant(Title);

            var containsMethod = typeof(string)
                .GetMethods()
                .First(methodInfo => methodInfo.Name == nameof(string.Contains) && methodInfo.GetParameters().Length == 1);

            var titleExpression = Expression.Call(titleMember, containsMethod, titleConstant);

            expressions.Add(titleExpression);
        }

        if (!string.IsNullOrWhiteSpace(Body))
        {
            var bodyMember = Expression.Property(blogPost, nameof(BlogPost.Body));
            var bodyConstant = Expression.Constant(Body);

            var containsMethod = typeof(string)
                .GetMethods()
                .First(methodInfo => methodInfo.Name == nameof(string.Contains) && methodInfo.GetParameters().Length == 1);

            var bodyExpression = Expression.Call(bodyMember, containsMethod, bodyConstant);

            expressions.Add(bodyExpression);
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

        if (baseExpression is null)
        {
            return null;
        }

        var lambda = Expression.Lambda<Func<BlogPost, bool>>(baseExpression, blogPost);

        return lambda;
    }
}