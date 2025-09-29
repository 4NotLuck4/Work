using LabWork7;

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