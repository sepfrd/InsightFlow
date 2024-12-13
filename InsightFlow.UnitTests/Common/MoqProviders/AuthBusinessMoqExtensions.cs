using InsightFlow.Business.Interfaces;
using Moq;

namespace InsightFlow.UnitTests.Common.MoqProviders;

public static class AuthBusinessMoqExtensions
{
    public static Mock<IAuthBusiness> SetupGetSignedInUserExternalIdToReturn(this Mock<IAuthBusiness> authBusinessMock, Guid expectedUserGuid)
    {
        authBusinessMock
            .Setup(authBusiness => authBusiness.GetSignedInUserExternalId())
            .Returns(expectedUserGuid.ToString())
            .Verifiable();

        return authBusinessMock;
    }
}