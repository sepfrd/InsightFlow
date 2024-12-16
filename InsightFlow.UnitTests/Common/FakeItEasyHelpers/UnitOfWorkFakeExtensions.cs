using FakeItEasy;
using InsightFlow.DataAccess.Interfaces;

namespace InsightFlow.UnitTests.Common.FakeItEasyHelpers;

public static class UnitOfWorkFakeExtensions
{
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