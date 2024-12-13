using InsightFlow.DataAccess.Dtos;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Sieve.Models;

namespace InsightFlow.UnitTests.Common.MoqProviders;

public static class BaseRepositoryMoqExtensions
{
    public static Mock<IBaseRepository<T>> SetupCreateAsyncToReturn<T>(this Mock<IBaseRepository<T>> baseRepositoryMock, T expectedEntity)
        where T : BaseEntity
    {
        baseRepositoryMock
            .Setup(baseRepository =>
                baseRepository.CreateAsync(
                    It.IsAny<T>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedEntity)
            .Verifiable();

        return baseRepositoryMock;
    }

    public static Mock<IBaseRepository<T>> SetupGetAllAsyncToReturn<T>(
        this Mock<IBaseRepository<T>> baseRepositoryMock,
        PagedEntitiesResponseDto<T> expectedPagedEntities)
        where T : BaseEntity
    {
        baseRepositoryMock
            .Setup(baseRepository =>
                baseRepository.GetAllAsync(
                    It.IsAny<SieveModel>(),
                    It.IsAny<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPagedEntities)
            .Verifiable();

        return baseRepositoryMock;
    }

    public static Mock<IBaseRepository<T>> SetupGetAllActiveAsyncToReturn<T>(
        this Mock<IBaseRepository<T>> baseRepositoryMock,
        PagedEntitiesResponseDto<T> expectedPagedEntities)
        where T : BaseEntity
    {
        baseRepositoryMock
            .Setup(baseRepository =>
                baseRepository.GetAllActiveAsync(
                    It.IsAny<SieveModel>(),
                    It.IsAny<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPagedEntities)
            .Verifiable();

        return baseRepositoryMock;
    }

    public static Mock<IBaseRepository<T>> SetupGetByIdAsyncToReturn<T>(this Mock<IBaseRepository<T>> baseRepositoryMock, T expectedEntity)
        where T : BaseEntity
    {
        baseRepositoryMock
            .Setup(baseRepository =>
                baseRepository.GetByIdAsync(
                    expectedEntity.Id,
                    It.IsAny<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedEntity)
            .Verifiable();

        return baseRepositoryMock;
    }

    public static Mock<IBaseRepository<T>> SetupGetByGuidAsyncToReturn<T>(this Mock<IBaseRepository<T>> baseRepositoryMock, T expectedEntity)
        where T : BaseEntity
    {
        baseRepositoryMock
            .Setup(baseRepository =>
                baseRepository.GetByGuidAsync(
                    expectedEntity.Guid,
                    It.IsAny<Func<IQueryable<T>, IIncludableQueryable<T, object?>>?>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedEntity)
            .Verifiable();

        return baseRepositoryMock;
    }

    public static Mock<IBaseRepository<T>> SetupDeleteToReturn<T>(this Mock<IBaseRepository<T>> baseRepositoryMock, T expectedEntity)
        where T : BaseEntity
    {
        baseRepositoryMock
            .Setup(baseRepository => baseRepository.Delete(expectedEntity))
            .Verifiable();

        return baseRepositoryMock;
    }
}