using ManagementRPG.Domain.Abstractions.Context;
using Npgsql;
using System.Data;

namespace ManagementRPG.Infrastructure.Context.Postgres
{
    public sealed class DBContextPostgres : IDBContext, IDisposable
    {
        public string ConnectionString { get; private set; }
        public IDbConnection Connection { get; private set; }

        public DBContextPostgres(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            Connection = connection;
            Connection.Open();

            return Connection;
        }

        public bool CloseConnection()
        {
            if (Connection == null)
                return false;

            if (Connection.State != ConnectionState.Open)
                return false;

            Connection.Close();
            return true;
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
