using LabWork7;
using System.Data;

//5.1.1
Console.WriteLine("Исходная строка подключения:");
Console.WriteLine(DataAccessLayer.ConnectionString);

//5.1.2
DataAccessLayer.ChangeConnectionSetting("NEW_SERVER", "NEW_BD", "admin", "newpass");
Console.WriteLine("Новая строка подключения:");
Console.WriteLine(DataAccessLayer.ConnectionString);

//5.1.3
bool canConnect = DataAccessLayer.TestConnection();
Console.WriteLine($"Подключение к БД: {(canConnect ? "УСПЕШНО" : "ОШИБКА")}");

//5.2.1
string createTableSql = @"
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TestTable' AND xtype='U')
    CREATE TABLE TestTable (Id INT IDENTITY(1,1), Name NVARCHAR(50))";

int result = await DataAccessLayer.ExecuteNonQueryAsync(createTableSql);
Console.WriteLine($"Выполнена команда CREATE TABLE. Затронуто строк: {result}");

string insertSql = "INSERT INTO TestTable (Name) VALUES ('Test Record')";
result = await DataAccessLayer.ExecuteNonQueryAsync(insertSql);
Console.WriteLine($"Выполнена команда INSERT. Затронуто строк: {result}");
Console.WriteLine();

//5.2.2
string countSql = "SELECT COUNT(*) FROM TestTable";
object? countResult = await DataAccessLayer.ExecuteScalarAsync(countSql);
Console.WriteLine($"Количество записей в TestTable: {countResult}");

string maxIdSql = "SELECT MAX(Id) FROM TestTable";
object? maxIdResult = await DataAccessLayer.ExecuteScalarAsync(maxIdSql);
Console.WriteLine($"Максимальный ID в TestTable: {maxIdResult}");
Console.WriteLine();

//5.3
int updatedRows = await DataAccessLayer.UpdateTicketPriceAsync(1, 15.50m);
Console.WriteLine($"Обновлено записей о ценах билетов: {updatedRows}");
Console.WriteLine();

//5.4.2
try
{
    int uploadResult = await DataAccessLayer.UploadMoviePosterAsync(1, @"C:\posters\movie1.jpg");
    Console.WriteLine($"Файл загружен в БД. Обновлено записей: {uploadResult}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка при загрузке файла: {ex.Message}");
}
Console.WriteLine();

//5.4.3
bool downloadResult = await DataAccessLayer.DownloadMoviePosterAsync(1, @"C:\downloads\movie1_poster.jpg");
Console.WriteLine($"Файл выгружен из БД: {(downloadResult ? "УСПЕШНО" : "НЕ УДАЛОСЬ")}");
Console.WriteLine();

//5.5
DataTable movies = await DataAccessLayer.GetUpcomingMoviesAsync();

Console.WriteLine("Предстоящие фильмы:");
foreach (DataRow row in movies.Rows)
{
    Console.WriteLine($"ID: {row["MovieId"]}, Название: {row["Title"]}, " +
                      $"Дата выхода: {((DateTime)row["ReleaseDate"]).ToShortDateString()}");
}
Console.WriteLine();