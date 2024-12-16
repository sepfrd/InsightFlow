using FakeItEasy;
using InsightFlow.Business.Interfaces;

namespace InsightFlow.UnitTests.Common.FakeItEasyProviders;

public static class AuthBusinessFakeExtensions
{
    public static IAuthBusiness SetupGetSignedInUserExternalIdToReturn(this IAuthBusiness authBusinessFake, Guid expectedUserGuid)
    {
        A.CallTo(() => authBusinessFake.GetSignedInUserExternalId())
            .Returns(expectedUserGuid.ToString());

        return authBusinessFake;
    }
}