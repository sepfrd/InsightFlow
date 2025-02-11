namespace InsightFlow.Domain.Common;

public static class DomainErrors
{
    public static readonly Error BadRequest = new(400, "Bad Request");
    public static readonly Error Unauthenticated = new(401, "You need to log in first.");
    public static readonly Error Forbidden = new(403, "Forbidden");
    public static readonly Error NotFound = new(404, "Not Found");
    public static readonly Error InternalServerError = new(500, "Something went wrong in our side, please try again later.");
}