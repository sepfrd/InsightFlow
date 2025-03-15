namespace Common.Constants;

public static class StringConstants
{
    public const string BadRequest = "Bad Request";
    public const string Unauthenticated = "Unauthenticated";
    public const string Forbidden = "Forbidden";
    public const string NotFound = "Not Found";
    public const string InternalServerError = "Something went wrong in our side, please try again later.";
    public const string AlreadySignedIn = "You are already signed in.";
    public const string ForbiddenActionTemplate = "You do not have permission to {0} this {1}.";
    public const string PropertyNotUniqueTemplate = "{0} is already taken.\nChoose another {1}.";
    public const string InvalidCredentials = "Username and/or password not correct.";
    public const string InvalidParametersTemplate = "Invalid value for parameter(s) {0}.";
    public const string IdenticalNewPropertyValuesTemplate = "At least one new property value must be different from its current value.";
    public const string IdenticalNewValueTemplate = "{0} value is already {1}";
    public const string EntityNotFoundByUuidTemplate = "{0} with UUID of {1} not found.";
    public const string SuccessfulLogin = "Successfully authenticated.";
    public const string SuccessfulCreationTemplate = "Successfully updated {0} entity.";
    public const string SuccessfulUpdateTemplate = "Successfully updated {0} entity.";
    public const string SuccessfulDeletionTemplate = "Successfully deleted {0} entity.";
    public const string ExceptionTemplate = "Application encountered an unhandled exception of type: {ExceptionType}";
    public const string ApplicationInternalServerErrorTemplate = "Application encountered an unexpected behavior: {Error}";
    public const string ApplicationFatalExceptionTemplate = "Application stopped due to a {exceptionType} exception";

    // ------------------------------ Internal Server Errors Log ------------------------------
    public const string DatabasePersistenceErrorLogTemplate = "Could not persist changes of entity of type {EntityType} while trying to {ActionName}";
    public const string InvalidPersistedDataErrorLogTemplate = "Could not retrieve entity/entities of type {EntityType} or the persisted data was not valid";
    public const string MappingErrorLogTemplate = "Could not map entity of type {SourceEntityType} to entity of type {DestinationEntityType}";
    public const string JwtCreationErrorLog = "Could not create a JWT instance while data was valid";

    // ------------------------------ Action Names ------------------------------
    public const string CreateActionName = "Create";
    public const string UpdateActionName = "Update";
    public const string DeleteActionName = "Delete";
}
