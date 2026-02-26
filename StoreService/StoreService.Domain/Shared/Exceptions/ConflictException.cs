namespace StoreService.Domain.Shared.Exceptions
{
    public class ConflictException(string? message = null) : Exception(message)
    {
    }
}
