using LabWork8;
using LabWork8.Repository;

try
{
	var dbContext = new DatabaseContext("mssql", "ispp3102", "ispp3102", "3102");

	var visitorRepo = new VisitorRepository(dbContext);
	var genreRepo = new GenreRepository(dbContext);

    using var connection = dbContext.CreateConnection();
    connection.Open();
    Console.WriteLine("Подключение к БД успешно открыто");
    Console.WriteLine($"Состояние подключения: {connection.State}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка подключения: {ex.Message}");
}