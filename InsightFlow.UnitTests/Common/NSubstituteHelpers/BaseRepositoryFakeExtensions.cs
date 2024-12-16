using InsightFlow.DataAccess.Dtos;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NSubstitute;
using Sieve.Models;

namespace InsightFlow.UnitTests.Common.NSubstituteHelpers;

public static class BaseRepositoryFakeExtensions
{
    public static IBaseRepository<T> SetupCreateAsyncToReturn<T>(this IBaseRepository<T> baseRepositorySubstitute, T expectedEntity)
        where T : BaseEntity
    {
        baseRepositorySubstitute.CreateAsync(Arg.Any<T>(), Arg.Any<CancellationToken>()).Returns(expectedEntity);

        return baseRepositorySubstitute;
    }

    public static IBaseRepository<T> SetupGetAllAsyncToReturn<T>(this IBaseRepository<T> baseRepositorySubstitute, PagedEntitiesResponseDto<T> expectedPagedEntities)
        where T : BaseEntity
    {
        baseRepositorySubstitute.GetAllAsync(
                Arg.Any<SieveModel>(),
                Arg.Any<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>(),
                Arg.Any<CancellationToken>())
            .Returns(expectedPagedEntities);

        return baseRepositorySubstitute;
    }

    public static IBaseRepository<T> SetupGetAllActiveAsyncToReturn<T>(this IBaseRepository<T> baseRepositorySubstitute, PagedEntitiesResponseDto<T> expectedPagedEntities)
        where T : BaseEntity
    {
        baseRepositorySubstitute.GetAllActiveAsync(
                Arg.Any<SieveModel>(),
                Arg.Any<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>(),
                Arg.Any<CancellationToken>())
            .Returns(expectedPagedEntities);

        return baseRepositorySubstitute;
    }

    public static IBaseRepository<T> SetupGetByIdAsyncToReturn<T>(this IBaseRepository<T> baseRepositorySubstitute, T expectedEntity)
        where T : BaseEntity
    {
        baseRepositorySubstitute.GetByIdAsync(
                expectedEntity.Id,
                Arg.Any<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>(),
                Arg.Any<CancellationToken>())
            .Returns(expectedEntity);

        return baseRepositorySubstitute;
    }

    public static IBaseRepository<T> SetupGetByGuidAsyncToReturn<T>(this IBaseRepository<T> baseRepositorySubstitute, T expectedEntity)
        where T : BaseEntity
    {
        baseRepositorySubstitute.GetByGuidAsync(
                expectedEntity.Guid,
                Arg.Any<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>(),
                Arg.Any<CancellationToken>())
            .Returns(expectedEntity);

        return baseRepositorySubstitute;
    }
}