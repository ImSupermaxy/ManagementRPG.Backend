using System.Data;

namespace ManagementRPG.Domain.Abstractions.Context
{
    public interface IDBContext : IDisposable
    {
        string ConnectionString { get; }
        IDbConnection Connection { get; }
        IDbConnection CreateConnection();
    }
}