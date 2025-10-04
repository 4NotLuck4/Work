using System;
using System.Data;
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

        //5.2.2
        public static async Task<object?> ExecuteScalarAsync(string sqlCommand)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    return await command.ExecuteScalarAsync();
                }
            }
        }

        //5.3
        public static async Task<int> UpdateTicketPriceAsync(int ticketTypeId, decimal newPrice)
        {
            string sql = "UPDATE TicketTypes SET Price = @Price WHERE TicketTypeId = @TicketTypeId";

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Price", newPrice);
                    command.Parameters.AddWithValue("@TicketTypeId", ticketTypeId);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        //5.4.2
        public static async Task<int> UploadMoviePosterAsync(int movieId, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден", filePath);

            byte[] fileData = await File.ReadAllBytesAsync(filePath);

            string sql = "UPDATE Movies SET Poster = @Poster WHERE MovieId = @MovieId";

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Poster", fileData);
                    command.Parameters.AddWithValue("@MovieId", movieId);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        //5.4.3
        // Метод для сохранения файла из БД на ПК
        public static async Task<bool> DownloadMoviePosterAsync(int movieId, string savePath)
        {
            string sql = "SELECT Poster FROM Movies WHERE MovieId = @MovieId";

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MovieId", movieId);

                    var result = await command.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        byte[] fileData = (byte[])result;
                        await File.WriteAllBytesAsync(savePath, fileData);
                        return true;
                    }

                    return false;
                }
            }
        }

        //5.5
        // Метод для выборки данных в формате DataTable
        public static async Task<DataTable> GetUpcomingMoviesAsync()
        {
            var dataTable = new DataTable();

            string sql = @"
            SELECT MovieId, Title, ReleaseDate, Genre, Duration 
            FROM Movies 
            WHERE ReleaseDate > GETDATE() 
            ORDER BY ReleaseDate";

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }
    }
}
