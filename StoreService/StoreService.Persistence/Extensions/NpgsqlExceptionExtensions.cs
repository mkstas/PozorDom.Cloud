using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace StoreService.Persistence.Extensions
{
    public static class NpgsqlExceptionExtensions
    {
        public static bool IsUniqueConstraintViolation(this DbUpdateException ex, string constraintName)
        {
            return ex.InnerException is PostgresException pg &&
                   pg.SqlState == PostgresErrorCodes.UniqueViolation &&
                   pg.ConstraintName == constraintName;
        }
    }
}
