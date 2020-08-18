using System.Data;
using System.Data.SqlClient;
using TestCoreApp.Domain.Persistence;

namespace TestCoreApp.Infrastructure.Persistence
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection connection;
        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection GetOpenConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = new SqlConnection(_connectionString);
            }

            return connection;
        }
    }
}
