using System;
using Microsoft.Data.SqlClient;

namespace LabWork7
{
    public static class DataAccessLayer
    {
        //5.1
        //5.1.1
        private static string _server = "mssql";
        private static string _datebase = "ispp3102";
        private static string _login = "ispp3102";
        private static string _password = "ispp3102";

        public static string ConnectionString
        {
            get
            {
                var bulder = new SqlConnectionStringBuilder
                {
                    DataSource = _server,
                    InitialCatalog = _datebase,
                    UserID = _login,
                    Password = _password,
                    TrustServerCertificate = true
                };
                return bulder.ConnectionString;
            }
        }

        //5.1.2
        public static void ChangeConnectionSetting(string server, string database, string login, string password)
        {
            _server = server;
            _datebase = database;
            _login = login;
            _password = password;
        }

        //5.1.3
        public static bool TestConnection()
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //5.2
        //5.2.1
        public static async Task<int> ExecuteNonQueryAsync(string sqlCommand)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
