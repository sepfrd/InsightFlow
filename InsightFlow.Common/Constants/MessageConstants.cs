namespace InsightFlow.Common.Constants;

public static class MessageConstants
{
    // ---------------------------- Bad Request (400) Messages ----------------------------
    public const string AlreadySignedInMessage = "You are already signed in.";
    public const string InvalidCredentialsMessage = "Username and/or password not correct.";
    public const string InvalidParametersMessage = "Invalid value for parameter(s) {0}.";
    public const string InvalidProfileImageFormatMessage = "{0} is not a valid image format for profile image.\nValid formats: {1}";

    // ---------------------------- Forbidden (403) Messages ----------------------------
    public const string ForbiddenActionMessage = "You do not have permission to {0} this {1}.";

    // ---------------------------- Not Found (404) Messages ----------------------------
    public const string EntityNotFoundByGuidMessage = "{0} with GUID of {1} not found.";
    public const string EntityNotFoundByIdMessage = "{0} with ID of {1} not found.";

    // ---------------------------- OK (200) Messages ----------------------------
    public const string SuccessfulLoginMessage = "Successfully logged in.";
    public const string SuccessfulUpdateMessage = "Successfully updated {0} entity.";
    public const string SuccessfulDeleteMessage = "Successfully deleted {0} entity.";
    public const string SuccessfulProfileImageUploadMessage = "Successfully uploaded profile image.";
    public const string NoProfileImageUploadedYetMessage = "You have not uploaded a profile image yet.";

    // ---------------------------- Exception Messages ----------------------------
    public const string ExceptionMessage = "Application encountered an unhandled exception of type: {exceptionType}";
    public const string ApplicationFatalExceptionMessage = "Application stopped due to a {exceptionType} exception";

    public const string SwaggerAuthorizationMessage = "Please enter only the token (without Bearer)";
}