using FakeItEasy;
using InsightFlow.DataAccess.Interfaces;

namespace InsightFlow.UnitTests.Common.FakeItEasyProviders;

public static class UnitOfWorkFakeExtensions
{
    public static IUnitOfWork SetupGetRepository(this IUnitOfWork unitOfWorkFake, Exception exception)
    {
        A.CallTo(() => unitOfWorkFake.CommitAsync(A<CancellationToken>.Ignored))
            .ThrowsAsync(exception);

        return unitOfWorkFake;
    }

    public static IUnitOfWork SetupCommitAsyncToReturn(this IUnitOfWork unitOfWorkFake, int expectedResult)
    {
        A.CallTo(() => unitOfWorkFake.CommitAsync(A<CancellationToken>.Ignored))
            .Returns(expectedResult);

        return unitOfWorkFake;
    }

    public static IUnitOfWork SetupCommitAsyncToThrow(this IUnitOfWork unitOfWorkFake, Exception exception)
    {
        A.CallTo(() => unitOfWorkFake.CommitAsync(A<CancellationToken>.Ignored))
            .ThrowsAsync(exception);

        return unitOfWorkFake;
    }
}