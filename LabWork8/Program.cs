//using LabWork8;
//using LabWork8.Repository;

//try
//{
//	var dbContext = new DatabaseContext("mssql", "ispp3102", "ispp3102", "3102");

//	var visitorRepo = new VisitorRepository(dbContext);
//	var genreRepo = new GenreRepository(dbContext);

//    using var connection = dbContext.CreateConnection();
//    connection.Open();
//    Console.WriteLine("Подключение к БД успешно открыто");
//    Console.WriteLine($"Состояние подключения: {connection.State}");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Ошибка подключения: {ex.Message}");
//}


//3.3
//using LabWork8;
//using System.Data;

//try
//{
//    // Создание контекста БД и репозиториев
//    var dbContext = new DatabaseContext("mssql", "ispp3102", "ispp3102", "3102");
//    var visitorRepo = new VisitorRepository(dbContext);
//    var genreRepo = new GenreRepository(dbContext);

//    // Проверка GetAllAsync для посетителей
//    Console.WriteLine("--- ВСЕ ПОСЕТИТЕЛИ ---");
//    var allVisitors = await visitorRepo.GetAllAsync();
//    foreach (var visitor in allVisitors)
//    {
//        Console.WriteLine($"ID: {visitor.Id}, Имя: {visitor.Name}, Email: {visitor.Email}");
//    }

//    // Проверка GetByIdAsync для посетителей
//    Console.WriteLine("\n--- ПОИСК ПОСЕТИТЕЛЯ ПО ID ---");
//    var visitorById = await visitorRepo.GetByIdAsync(1);
//    if (visitorById != null)
//    {
//        Console.WriteLine($"Найден: ID: {visitorById.Id}, Имя: {visitorById.Name}");
//    }
//    else
//    {
//        Console.WriteLine("Посетитель с указанным ID не найден");
//    }

//    // Проверка GetAllAsync для жанров
//    Console.WriteLine("\n--- ВСЕ ЖАНРЫ ---");
//    var allGenres = await genreRepo.GetAllAsync();
//    foreach (var genre in allGenres)
//    {
//        Console.WriteLine($"ID: {genre.Id}, Название: {genre.Name}");
//    }

//    // Проверка GetByIdAsync для жанров
//    Console.WriteLine("\n--- ПОИСК ЖАНРА ПО ID ---");
//    var genreById = await genreRepo.GetByIdAsync(1);
//    if (genreById != null)
//    {
//        Console.WriteLine($"Найден: ID: {genreById.Id}, Название: {genreById.Name}");
//    }
//    else
//    {
//        Console.WriteLine("Жанр с указанным ID не найден");
//    }

//    // Проверка случая, когда запись не найдена
//    Console.WriteLine("\n--- ПРОВЕРКА НЕСУЩЕСТВУЮЩЕГО ID ---");
//    var notFoundVisitor = await visitorRepo.GetByIdAsync(999);
//    Console.WriteLine(notFoundVisitor == null ? "Запись не найдена (ожидаемо)" : "Ошибка: запись найдена");

//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Ошибка: {ex.Message}");
//    Console.WriteLine($"Подробности: {ex.InnerException?.Message}");
//}

//3.4.2
//using LabWork8.Model;
//using LabWork8;
//using Microsoft.Data.SqlClient;

//try
//{
//    // Создание контекста БД и репозиториев
//    var dbContext = new DatabaseContext("mssql", "ispp3102", "ispp3102", "3102");
//    var visitorRepo = new VisitorRepository(dbContext);
//    var genreRepo = new GenreRepository(dbContext);

//    Console.WriteLine("=== ТЕСТИРОВАНИЕ ДОБАВЛЕНИЯ ДАННЫХ ===\n");

//    // Тест 1: Добавление нового посетителя
//    Console.WriteLine("1. ДОБАВЛЕНИЕ НОВОГО ПОСЕТИТЕЛЯ:");
//    var newVisitor = new Visitor
//    {
//        Name = "Мария Сидорова",
//        Email = "maria@example.com",
//        Phone = "+7-999-123-45-67",
//        RegistrationDate = DateTime.Now
//    };

//    int newVisitorId = await visitorRepo.AddAsync(newVisitor);
//    Console.WriteLine($"✅ Новый посетитель добавлен с ID: {newVisitorId}");

//    // Проверка, что посетитель действительно добавлен
//    var addedVisitor = await visitorRepo.GetByIdAsync(newVisitorId);
//    if (addedVisitor != null)
//    {
//        Console.WriteLine($"✅ Проверка: найден посетитель - {addedVisitor.Name}");
//    }

//    // Тест 2: Добавление нового жанра
//    Console.WriteLine("\n2. ДОБАВЛЕНИЕ НОВОГО ЖАНРА:");
//    var newGenre = new Genre
//    {
//        Name = "Фэнтези"
//    };

//    int newGenreId = await genreRepo.AddAsync(newGenre);
//    Console.WriteLine($"✅ Новый жанр добавлен с ID: {newGenreId}");

//    // Проверка, что жанр действительно добавлен
//    var addedGenre = await genreRepo.GetByIdAsync(newGenreId);
//    if (addedGenre != null)
//    {
//        Console.WriteLine($"✅ Проверка: найден жанр - {addedGenre.Name}");
//    }

//    // Тест 3: Добавление нескольких записей
//    Console.WriteLine("\n3. ДОБАВЛЕНИЕ НЕСКОЛЬКИХ ПОСЕТИТЕЛЕЙ:");

//    var visitorsToAdd = new[]
//    {
//        new Visitor("Алексей Козлов", "alex@mail.ru", "+7-911-111-11-11"),
//        new Visitor("Ольга Новикова", "olga@mail.ru", "+7-922-222-22-22"),
//        new Visitor("Сергей Васильев", "sergey@mail.ru") // без телефона
//    };

//    foreach (var visitor in visitorsToAdd)
//    {
//        int id = await visitorRepo.AddAsync(visitor);
//        Console.WriteLine($"✅ Добавлен посетитель: {visitor.Name} (ID: {id})");
//    }

//    // Тест 4: Показать всех посетителей после добавления
//    Console.WriteLine("\n4. ВСЕ ПОСЕТИТЕЛИ В БАЗЕ:");
//    var allVisitors = await visitorRepo.GetAllAsync();
//    foreach (var visitor in allVisitors)
//    {
//        Console.WriteLine($"ID: {visitor.Id}, Имя: {visitor.Name}, Email: {visitor.Email}, Телефон: {visitor.Phone ?? "не указан"}");
//    }

//    // Тест 5: Показать все жанры после добавления
//    Console.WriteLine("\n5. ВСЕ ЖАНРЫ В БАЗЕ:");
//    var allGenres = await genreRepo.GetAllAsync();
//    foreach (var genre in allGenres)
//    {
//        Console.WriteLine($"ID: {genre.Id}, Название: {genre.Name}");
//    }

//}
//catch (SqlException sqlEx)
//{
//    Console.WriteLine($"Ошибка базы данных: {sqlEx.Message}");
//    if (sqlEx.Number == 2627) // Ошибка уникальности
//    {
//        Console.WriteLine("Нарушение уникальности (возможно, дублирование данных)");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Общая ошибка: {ex.Message}");
//    Console.WriteLine($"Подробности: {ex.InnerException?.Message}");
//}

//3.5.3
using LabWork8.Model;
using LabWork8;
using Microsoft.Data.SqlClient;

try
{
    // Создание контекста БД и репозиториев
    var dbContext = new DatabaseContext("mssql", "ispp3102", "ispp3102", "3102");
    var visitorRepo = new VisitorRepository(dbContext);
    var genreRepo = new GenreRepository(dbContext);

    Console.WriteLine("=== ТЕСТИРОВАНИЕ ОБНОВЛЕНИЯ И УДАЛЕНИЯ ДАННЫХ ===\n");

    // Подготовка: создаем тестовые данные
    Console.WriteLine("ПОДГОТОВКА: СОЗДАНИЕ ТЕСТОВЫХ ДАННЫХ");

    var testVisitor = new Visitor("Тестовый Посетитель", "test@example.com", "+7-000-000-00-00");
    int testVisitorId = await visitorRepo.AddAsync(testVisitor);
    Console.WriteLine($"✅ Создан тестовый посетитель (ID: {testVisitorId})");

    var testGenre = new Genre("Тестовый Жанр", "Описание тестового жанра");
    int testGenreId = await genreRepo.AddAsync(testGenre);
    Console.WriteLine($"Создан тестовый жанр (ID: {testGenreId})");

    // Тест 1: Обновление посетителя
    Console.WriteLine("\n1. ТЕСТ ОБНОВЛЕНИЯ ПОСЕТИТЕЛЯ:");

    var visitorToUpdate = await visitorRepo.GetByIdAsync(testVisitorId);
    if (visitorToUpdate != null)
    {
        Console.WriteLine($"До обновления: {visitorToUpdate.Name}, {visitorToUpdate.Email}");

        visitorToUpdate.Name = "Обновленное Имя";
        visitorToUpdate.Email = "updated@example.com";
        visitorToUpdate.Phone = "+7-999-888-77-66";

        await visitorRepo.UpdateAsync(visitorToUpdate);
        Console.WriteLine("Данные посетителя обновлены");

        // Проверяем обновление
        var updatedVisitor = await visitorRepo.GetByIdAsync(testVisitorId);
        if (updatedVisitor != null)
        {
            Console.WriteLine($"После обновления: {updatedVisitor.Name}, {updatedVisitor.Email}");
            Console.WriteLine(updatedVisitor.Name == "Обновленное Имя"
                ? "Проверка пройдена: имя обновлено корректно"
                : "Ошибка: имя не обновлено");
        }
    }

    // Тест 2: Обновление жанра
    Console.WriteLine("\n2. ТЕСТ ОБНОВЛЕНИЯ ЖАНРА:");

    var genreToUpdate = await genreRepo.GetByIdAsync(testGenreId);
    if (genreToUpdate != null)
    {
        Console.WriteLine($"До обновления: {genreToUpdate.Name}");

        genreToUpdate.Name = "Обновленный Жанр";

        await genreRepo.UpdateAsync(genreToUpdate);
        Console.WriteLine("Данные жанра обновлены");

        // Проверяем обновление
        var updatedGenre = await genreRepo.GetByIdAsync(testGenreId);
        if (updatedGenre != null)
        {
            Console.WriteLine($"После обновления: {updatedGenre.Name}");
            Console.WriteLine(updatedGenre.Name == "Обновленный Жанр"
                ? "Проверка пройдена: название жанра обновлено корректно"
                : "Ошибка: название жанра не обновлено");
        }
    }

    // Тест 3: Удаление посетителя
    Console.WriteLine("\n3. ТЕСТ УДАЛЕНИЯ ПОСЕТИТЕЛЯ:");

    // Создаем посетителя для удаления
    var visitorToDelete = new Visitor("Удаляемый Посетитель", "delete@example.com");
    int deleteVisitorId = await visitorRepo.AddAsync(visitorToDelete);
    Console.WriteLine($"Создан посетитель для удаления (ID: {deleteVisitorId})");

    // Проверяем, что посетитель существует
    var existsBeforeDelete = await visitorRepo.GetByIdAsync(deleteVisitorId);
    Console.WriteLine(existsBeforeDelete != null
        ? "Посетитель существует перед удалением"
        : "Ошибка: посетитель не найден перед удалением");

    // Удаляем посетителя
    await visitorRepo.DeleteAsync(deleteVisitorId);
    Console.WriteLine("✅ Запрос на удаление выполнен");

    // Проверяем, что посетитель удален
    var existsAfterDelete = await visitorRepo.GetByIdAsync(deleteVisitorId);
    Console.WriteLine(existsAfterDelete == null
        ? "Проверка пройдена: посетитель успешно удален"
        : "Ошибка: посетитель все еще существует после удаления");

    // Тест 4: Удаление жанра
    Console.WriteLine("\n4. ТЕСТ УДАЛЕНИЯ ЖАНРА:");

    // Создаем жанр для удаления
    var genreToDelete = new Genre("Удаляемый Жанр");
    int deleteGenreId = await genreRepo.AddAsync(genreToDelete);
    Console.WriteLine($"Создан жанр для удаления (ID: {deleteGenreId})");

    // Проверяем, что жанр существует
    var genreExistsBefore = await genreRepo.GetByIdAsync(deleteGenreId);
    Console.WriteLine(genreExistsBefore != null
        ? "Жанр существует перед удалением"
        : "Ошибка: жанр не найден перед удалением");

    // Удаляем жанр
    await genreRepo.DeleteAsync(deleteGenreId);
    Console.WriteLine("Запрос на удаление выполнен");

    // Проверяем, что жанр удален
    var genreExistsAfter = await genreRepo.GetByIdAsync(deleteGenreId);
    Console.WriteLine(genreExistsAfter == null
        ? "Проверка пройдена: жанр успешно удален"
        : "Ошибка: жанр все еще существует после удаления");

    // Тест 5: Попытка удаления несуществующей записи
    Console.WriteLine("\n5. ТЕСТ УДАЛЕНИЯ НЕСУЩЕСТВУЮЩЕЙ ЗАПИСИ:");

    try
    {
        await visitorRepo.DeleteAsync(99999); // Несуществующий ID
        Console.WriteLine("Удаление несуществующей записи не вызвало ошибку (ожидаемо)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Неожиданная ошибка при удалении несуществующей записи: {ex.Message}");
    }

    // Тест 6: Комплексный сценарий - полный цикл CRUD
    Console.WriteLine("\n6. КОМПЛЕКСНЫЙ ТЕСТ (ПОЛНЫЙ ЦИКЛ CRUD):");

    // Create
    var complexVisitor = new Visitor("Комплексный Тест", "crud@test.com");
    int complexId = await visitorRepo.AddAsync(complexVisitor);
    Console.WriteLine($"CREATE: Создан посетитель (ID: {complexId})");

    // Read
    var readVisitor = await visitorRepo.GetByIdAsync(complexId);
    Console.WriteLine(readVisitor != null
        ? $"READ: Прочитан посетитель: {readVisitor.Name}"
        : "READ: Ошибка чтения");

    // Update
    if (readVisitor != null)
    {
        readVisitor.Name = "Обновленный Комплексный Тест";
        await visitorRepo.UpdateAsync(readVisitor);
        Console.WriteLine("UPDATE: Данные обновлены");

        // Проверка Update
        var afterUpdate = await visitorRepo.GetByIdAsync(complexId);
        Console.WriteLine(afterUpdate?.Name == "Обновленный Комплексный Тест"
            ? "Проверка UPDATE: данные обновлены корректно"
            : "Проверка UPDATE: ошибка обновления");
    }

    // Delete
    await visitorRepo.DeleteAsync(complexId);
    var afterDelete = await visitorRepo.GetByIdAsync(complexId);
    Console.WriteLine(afterDelete == null
        ? "DELETE: Посетитель успешно удален"
        : "DELETE: Ошибка удаления");

    Console.WriteLine(afterDelete == null
        ? "ВЕСЬ ЦИКЛ CRUD ВЫПОЛНЕН УСПЕШНО!"
        : "ЦИКЛ CRUD НЕ ЗАВЕРШЕН КОРРЕКТНО");

    // Финальная проверка состояния БД
    Console.WriteLine("\n--- ФИНАЛЬНОЕ СОСТОЯНИЕ БАЗЫ ДАННЫХ ---");

    var finalVisitors = await visitorRepo.GetAllAsync();
    Console.WriteLine($"Всего посетителей в БД: {finalVisitors.Count()}");

    var finalGenres = await genreRepo.GetAllAsync();
    Console.WriteLine($"Всего жанров в БД: {finalGenres.Count()}");

}
catch (SqlException sqlEx)
{
    Console.WriteLine($"Ошибка базы данных: {sqlEx.Message}");
    Console.WriteLine($"Код ошибки: {sqlEx.Number}");
}
catch (Exception ex)
{
    Console.WriteLine($"Общая ошибка: {ex.Message}");
    Console.WriteLine($"Подробности: {ex.InnerException?.Message}");
}