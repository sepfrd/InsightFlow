using InsightFlow.DataAccess.Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace InsightFlow.UnitTests.Common.NSubstituteHelpers;

public static class UnitOfWorkFakeExtensions
{
    public static IUnitOfWork SetupCommitAsyncToReturn(this IUnitOfWork unitOfWorkSubstitute, int expectedResult)
    {
        unitOfWorkSubstitute.CommitAsync(Arg.Any<CancellationToken>()).Returns(expectedResult);

        return unitOfWorkSubstitute;
    }

    public static IUnitOfWork SetupCommitAsyncToThrow(this IUnitOfWork unitOfWorkSubstitute, Exception exception)
    {
        unitOfWorkSubstitute.CommitAsync(Arg.Any<CancellationToken>()).ThrowsAsync(exception);

        return unitOfWorkSubstitute;
    }
}