using Microsoft.Data.SqlClient;
using System.Data;

namespace TranscriberApi.DataBased.Context
{
    public class ClientsContext
    {
        private readonly string _connectionString;

        public ClientsContext() : base()
        {
            _connectionString = string.Empty;
        }

        public ClientsContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
