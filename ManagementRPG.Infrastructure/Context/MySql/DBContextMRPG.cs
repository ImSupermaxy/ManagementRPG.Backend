using ManagementRPG.Domain.Abstractions.Context;
using MySql.Data.MySqlClient;
using System.Data;

namespace ManagementRPG.Infrastructure.Context.MySql
{
    public class DBContextMRPG : IDBContext, IDisposable
    {
        public IDbConnection Connection { get; private set; }

        public DBContextMRPG(string conectString)
        {
            //Connection = new MySqlConnection(conectString);

            //if (Connection.State == ConnectionState.Closed)
            //    Connection.Open();
        }

        public void Dispose()
        {
            //if (Connection.State == ConnectionState.Open)
            //    Connection.Close();
        }
    }
}
