using InsightFlow.Business.Interfaces;
using NSubstitute;

namespace InsightFlow.UnitTests.Common.NSubstituteHelpers;

public static class AuthBusinessFakeExtensions
{
    public static IAuthBusiness SetupGetSignedInUserExternalIdToReturn(this IAuthBusiness authBusinessSubstitute, Guid expectedUserGuid)
    {
        authBusinessSubstitute.GetSignedInUserExternalId().Returns(expectedUserGuid.ToString());

        return authBusinessSubstitute;
    }
}