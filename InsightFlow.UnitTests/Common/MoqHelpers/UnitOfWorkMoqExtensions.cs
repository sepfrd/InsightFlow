using InsightFlow.DataAccess.Interfaces;
using Moq;

namespace InsightFlow.UnitTests.Common.MoqHelpers;

public static class UnitOfWorkMoqExtensions
{
    public static Mock<IUnitOfWork> SetupCommitAsyncToReturn(this Mock<IUnitOfWork> unitOfWorkMock, int expectedResult)
    {
        unitOfWorkMock
            .Setup(unitOfWork =>
                unitOfWork.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        return unitOfWorkMock;
    }

    public static Mock<IUnitOfWork> SetupCommitAsyncToThrow(this Mock<IUnitOfWork> unitOfWorkMock, Exception exception)
    {
        unitOfWorkMock
            .Setup(unitOfWork =>
                unitOfWork.CommitAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

        return unitOfWorkMock;
    }
}