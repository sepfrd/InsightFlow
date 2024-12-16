using FakeItEasy;
using InsightFlow.DataAccess.Dtos;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Sieve.Models;

namespace InsightFlow.UnitTests.Common.FakeItEasyHelpers;

public static class BaseRepositoryFakeExtensions
{
    public static IBaseRepository<T> SetupCreateAsyncToReturn<T>(this IBaseRepository<T> baseRepositoryFake, T expectedEntity)
        where T : BaseEntity
    {
        A.CallTo(() => baseRepositoryFake.CreateAsync(A<T>.Ignored, A<CancellationToken>.Ignored))
            .Returns(expectedEntity);

        return baseRepositoryFake;
    }

    public static IBaseRepository<T> SetupGetAllAsyncToReturn<T>(this IBaseRepository<T> baseRepositoryFake, PagedEntitiesResponseDto<T> expectedPagedEntities)
        where T : BaseEntity
    {
        A.CallTo(() =>
                baseRepositoryFake.GetAllAsync(
                    A<SieveModel>.Ignored,
                    A<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>.Ignored,
                    A<CancellationToken>.Ignored))
            .Returns(expectedPagedEntities);

        return baseRepositoryFake;
    }

    public static IBaseRepository<T> SetupGetAllActiveAsyncToReturn<T>(this IBaseRepository<T> baseRepositoryFake, PagedEntitiesResponseDto<T> expectedPagedEntities)
        where T : BaseEntity
    {
        A.CallTo(() =>
                baseRepositoryFake.GetAllActiveAsync(
                    A<SieveModel>.Ignored,
                    A<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>.Ignored,
                    A<CancellationToken>.Ignored))
            .Returns(expectedPagedEntities);

        return baseRepositoryFake;
    }

    public static IBaseRepository<T> SetupGetByIdAsyncToReturn<T>(this IBaseRepository<T> baseRepositoryFake, T expectedEntity)
        where T : BaseEntity
    {
        A.CallTo(() =>
                baseRepositoryFake.GetByIdAsync(
                    expectedEntity.Id,
                    A<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>.Ignored,
                    A<CancellationToken>.Ignored))
            .Returns(expectedEntity);

        return baseRepositoryFake;
    }

    public static IBaseRepository<T> SetupGetByGuidAsyncToReturn<T>(this IBaseRepository<T> baseRepositoryFake, T expectedEntity)
        where T : BaseEntity
    {
        A.CallTo(() =>
                baseRepositoryFake.GetByGuidAsync(
                    expectedEntity.Guid,
                    A<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>.Ignored,
                    A<CancellationToken>.Ignored))
            .Returns(expectedEntity);

        return baseRepositoryFake;
    }
}