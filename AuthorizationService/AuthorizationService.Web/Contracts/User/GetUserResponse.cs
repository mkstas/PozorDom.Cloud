namespace AuthorizationService.Web.Contracts.User
{
    public record GetUserResponse(
        Guid Id,
        string EmailAddress);
}
