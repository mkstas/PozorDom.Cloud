namespace StoreService.Domain.Shared.Exceptions
{
    public class NotFoundException(string? message = null) : Exception(message)
    {
    }
}
