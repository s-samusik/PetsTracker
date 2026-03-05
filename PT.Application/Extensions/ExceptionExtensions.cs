using Npgsql;

namespace PT.Application.Extensions;

public static class ExceptionExtensions
{
    public static bool IsPostgreDbError(this Exception ex, string sqlState) 
        => ex.InnerException is PostgresException pg && pg.SqlState == sqlState;
}
