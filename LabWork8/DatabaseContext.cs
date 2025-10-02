using Microsoft.Data.SqlClient;
using System.Data;

namespace LabWork8
{
    public class DatabaseContext
    {
        //3.1.1
        private readonly string _connectionString;
        public DatabaseContext(string server, string database, string login, string password)
        {
            _connectionString = $"Server={server};Database={database};User ID={login};Password={password};TrustServerCertificate=true;";
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
