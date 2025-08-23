using System.Data;

namespace ManagementRPG.Domain.Abstractions.Context
{
    public interface IDBContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}