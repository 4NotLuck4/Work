using CinemaDbLibrary;
using CinemaDbLibrary.Context;
using CinemaDbLibrary.Services;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Настройка DI контейнера
var services = new ServiceCollection();

// Замените на вашу строку подключения
services.AddDbContext<CinemaContext>(options =>
    options.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3103;Persist Security Info=True;User ID=ispp3103;Password=3103;Trust Server Certificate=True"));

// Регистрация сервисов
services.AddScoped<VisitorService>();
services.AddScoped<FilmService>();
services.AddScoped<TicketService>();
services.AddScoped<GenreService>();

var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();

// Получение сервисов
var visitorService = scope.ServiceProvider.GetRequiredService<VisitorService>();
var filmService = scope.ServiceProvider.GetRequiredService<FilmService>();
var ticketService = scope.ServiceProvider.GetRequiredService<TicketService>();
var genreService = scope.ServiceProvider.GetRequiredService<GenreService>();

// 3.1 - Тестирование базовых операций
await TestBasicOperations(visitorService, filmService, ticketService, genreService);

// 3.2 - Тестирование пагинации
await TestPagination(visitorService, filmService);

// 3.3 - Тестирование сортировки
await TestSorting(filmService, ticketService);

Console.WriteLine("\n=== Все задания выполнены успешно! ===");



// 3.1 - Тестирование базовых операций
static async Task TestBasicOperations(VisitorService visitorService, FilmService filmService,
                                     TicketService ticketService, GenreService genreService)
{
    // Тестирование VisitorService
    var visitors = await visitorService.GetAllAsync();
    Console.WriteLine($" Всего посетителей: {visitors.Count}");

    if (visitors.Count > 0)
    {
        var firstVisitor = await visitorService.GetByIdAsync(visitors[0].VisitorId);
        Console.WriteLine($" Первый посетитель: {firstVisitor?.Name ?? "Не указано"}, Телефон: {firstVisitor?.Phone}");
        Console.WriteLine($" Куплено билетов: {firstVisitor?.Tickets?.Count}");
    }
    else
    {
        Console.WriteLine("В базе нет посетителей");
    }

    // Тестирование FilmService
    Console.WriteLine("\n2. Тестирование FilmService:");
    var films = await filmService.GetAllAsync();
    Console.WriteLine($" Всего фильмов: {films.Count}");

    if (films.Count > 0)
    {
        var firstFilm = await filmService.GetByIdAsync(films[0].FilmId);
        Console.WriteLine($" Первый фильм: {firstFilm?.Title}, Длительность: {firstFilm?.Duration}");
        Console.WriteLine($" Жанр: {firstFilm?.Genre?.Name}");
        Console.WriteLine($" Продано билетов: {firstFilm?.Tickets?.Count}");
    }
    else
    {
        Console.WriteLine("В базе нет фильмов");
    }
    // Тестирование TicketService
    Console.WriteLine("\n3. Тестирование TicketService:");
    var tickets = await ticketService.GetAllAsync();
    Console.WriteLine($"Всего билетов: {tickets.Count}");

    if (tickets.Count > 0)
    {
        var firstTicket = await ticketService.GetByIdAsync(tickets[0].TicketId);
        Console.WriteLine($"Первый билет: Ряд {firstTicket?.Row}, Место {firstTicket?.Seat}");
        Console.WriteLine($"Посетитель: {firstTicket?.Visitor?.Name ?? "Не указано"}");
        Console.WriteLine($"Фильм: {firstTicket?.Film?.Title ?? "Не указан"}");
    }
    else
    {
        Console.WriteLine("В базе нет билетов");
    }

    // Тестирование GenreService
    Console.WriteLine("\n4. Тестирование GenreService:");
    var genres = await genreService.GetAllAsync();
    Console.WriteLine($"Всего жанров: {genres.Count}");

    if (genres.Count > 0)
    {
        var firstGenre = await genreService.GetByIdAsync(genres[0].GenreId);
        Console.WriteLine($"Первый жанр: {firstGenre?.Name}");
        Console.WriteLine($"Фильмов в жанре: {firstGenre?.Films?.Count ?? 0}");
    }
    else
    {
        Console.WriteLine("В базе нет жанров");
    }

    Console.WriteLine("Задание 3.1 завершено успешно!");
}

// 3.2 - Тестирование пагинации
static async Task TestPagination(VisitorService visitorService, FilmService filmService)
{

    // Тестирование пагинации для фильмов
    var totalfilms = await filmService.GetTotalFilmsCountAsync();
    Console.WriteLine($"✅ Всего фильмов в БД: {totalfilms}");

    var filmPagination = new Pagination { PageSize = 2, PageNumber = 1 };

    for (int i = 1; i <= Math.Ceiling(totalfilms / 2.0); i++)
    {
        filmPagination.PageNumber = i;
        var paginatedFilms = await filmService.GetFilmsPaginatedAsync(filmPagination);
        Console.WriteLine($"✅ Страница {i}: {paginatedFilms.Count} фильмов");

        foreach (var film in paginatedFilms)
        {
            Console.WriteLine($"   - {film.Title} ({film.ReleaseDate})");
        }
    }

    // Тестирование пагинации для посетителей
    Console.WriteLine("\n2. Пагинация посетителей:");
    var totalVisitors = await visitorService.GetTotalVisitorsCountAsync();
    Console.WriteLine($"✅ Всего посетителей в БД: {totalVisitors}");
    // movie
    var visitorPagination = new Pagination { PageSize = 2, PageNumber = 1 };

    for (int i = 1; i <= Math.Ceiling(totalVisitors / 2.0); i++)
    {
        visitorPagination.PageNumber = i;
        var paginatedVisitors = await visitorService.GetVisitorsPaginatedAsync(visitorPagination);
        Console.WriteLine($"✅ Страница {i}: {paginatedVisitors.Count} посетителей");

        foreach (var visitor in paginatedVisitors)
        {
            Console.WriteLine($"   - {visitor.Name ?? "Не указано"} ({visitor.Phone})");
        }
    }

    // Тестирование граничных случаев
    Console.WriteLine("\n3. Граничные случаи пагинации:");

    // Страница с размером больше общего количества
    var largePage = new Pagination { PageSize = 100, PageNumber = 1 };
    var largePageResult = await filmService.GetFilmsPaginatedAsync(largePage);
    Console.WriteLine($"✅ Большая страница (100 элементов): получено {largePageResult.Count} фильмов");

    // Несуществующая страница
    var nonExistentPage = new Pagination { PageSize = 2, PageNumber = 100 };
    var nonExistentResult = await filmService.GetFilmsPaginatedAsync(nonExistentPage);
    Console.WriteLine($"✅ Несуществующая страница: получено {nonExistentResult.Count} фильмов");
    Console.WriteLine("✅ Задание 3.2 завершено успешно!");
}

// 3.3 - Тестирование сортировки
static async Task TestSorting(FilmService filmService, TicketService ticketService)
{
    Console.WriteLine("\n=== ЗАДАНИЕ 3.3 - СОРТИРОВКА ДАННЫХ ===");

    // Тестирование сортировки фильмов
    Console.WriteLine("\n1. Сортировка фильмов:");

    // Сортировка по названию (по возрастанию)
    var sortingByTitleAsc = new Sorting { SortBy = "title", Ascending = true };
    var sortedFilmsAsc = await filmService.GetFilmsSortedAsync(sortingByTitleAsc);
    Console.WriteLine("✅ Фильмы отсортированы по названию (A-Z):");
    foreach (var film in sortedFilmsAsc.Take(5))
    {
        Console.WriteLine($"   - {film.Title} ({film.ReleaseYear})");
    }

    // Сортировка по названию (по убыванию)
    var sortingByTitleDesc = new Sorting { SortBy = "title", Ascending = false };
    var sortedFilmsDesc = await filmService.GetFilmsSortedAsync(sortingByTitleDesc);
    Console.WriteLine("\n✅ Фильмы отсортированы по названию (Z-A):");
    foreach (var film in sortedFilmsDesc.Take(5))
    {
        Console.WriteLine($"   - {film.Title} ({film.ReleaseYear})");
    }


    // Сортировка по году выпуска (по убыванию)
    var sortingByYear = new Sorting { SortBy = "releaseyear", Ascending = false };
    var sortedByYear = await filmService.GetFilmsSortedAsync(sortingByYear);
    Console.WriteLine("\n✅ Фильмы отсортированы по году выпуска (новые сначала):");
    foreach (var film in sortedByYear.Take(5))
    {
        Console.WriteLine($"   - {film.Title} ({film.ReleaseYear})");
    }

    // Сортировка по длительности (по возрастанию)
    var sortingByDuration = new Sorting { SortBy = "duration", Ascending = true };
    var sortedByDuration = await filmService.GetFilmsSortedAsync(sortingByDuration);
    Console.WriteLine("\n✅ Фильмы отсортированы по длительности (короткие сначала):");
    foreach (var film in sortedByDuration.Take(5))
    {
        Console.WriteLine($"   - {film.Title} ({film.Duration} мин.)");
    }

    // Тестирование сортировки билетов
    Console.WriteLine("\n2. Сортировка билетов:");

    // Сортировка билетов по ряду
    var sortingByRow = new Sorting { SortBy = "row", Ascending = true };
    var sortedTickets = await ticketService.GetTicketsSortedAsync(sortingByRow);
    Console.WriteLine("✅ Билеты отсортированы по ряду (по возрастанию):");
    foreach (var ticket in sortedTickets.Take(5))
    {
        Console.WriteLine($"   - Ряд {ticket.Row}, Место {ticket.Seat}");
    }

    // Сортировка билетов по месту (по убыванию)
    var sortingBySeatDesc = new Sorting { SortBy = "seat", Ascending = false };
    var sortedBySeatDesc = await ticketService.GetTicketsSortedAsync(sortingBySeatDesc);
    Console.WriteLine("\n✅ Билеты отсортированы по месту (по убыванию):");
    foreach (var ticket in sortedBySeatDesc.Take(5))
    {
        Console.WriteLine($"   - Ряд {ticket.Row}, Место {ticket.Seat}");
    }

    // Комбинированная сортировка с пагинацией
    Console.WriteLine("\n3. Комбинированная сортировка с пагинацией:");

    var combinedSorting = new Sorting { SortBy = "title", Ascending = true };
    var combinedPagination = new Pagination { PageSize = 3, PageNumber = 1 };

    var combinedResult = await filmService.GetFilmsSortedAsync(combinedSorting, combinedPagination);
    Console.WriteLine($"✅ Комбинированная сортировка + пагинация (стр. {combinedPagination.PageNumber}):");
    foreach (var film in combinedResult)
    {
        Console.WriteLine($"   - {film.Title} ({film.ReleaseYear})");
    }
    // Тестирование сортировки по умолчанию
    Console.WriteLine("\n4. Сортировка по умолчанию (без указания поля):");
    var defaultSorting = new Sorting { SortBy = null, Ascending = true };
    var defaultSorted = await filmService.GetFilmsSortedAsync(defaultSorting);
    Console.WriteLine($"✅ Фильмы отсортированы по умолчанию (по ID): получено {defaultSorted.Count} фильмов");

    Console.WriteLine("✅ Задание 3.3 завершено успешно!");
}


//class Program
//{
//    static async Task Main(string[] args)
//    {
//        // Настройка DI контейнера
//        var services = new ServiceCollection();

//        services.AddDbContext<CinemaContext>(options =>
//            options.UseSqlServer(@"Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True"));

//        services.AddScoped<VisitorService>();
//        services.AddScoped<MovieService>();
//        //services.AddScoped<TicketService>();
//        services.AddScoped<GenreService>();

//        var provider = services.BuildServiceProvider();

//        using var scope = provider.CreateScope();

//        var visitorService = scope.ServiceProvider.GetRequiredService<VisitorService>();
//        var movieService = scope.ServiceProvider.GetRequiredService<MovieService>();
//        var ticketService = scope.ServiceProvider.GetRequiredService<TicketService>();

//        try
//        {
//            await TestBasicOperations(visitorService, movieService, ticketService);
//            await TestPagination(movieService);

//            Console.WriteLine("\nВсе тесты завершены успешно!");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//    }

//    static async Task TestBasicOperations(VisitorService visitorService, MovieService movieService, TicketService ticketService)
//    {
//        Console.WriteLine("=== Проверка базовых операций ===");

//        var visitors = await visitorService.GetAllAsync();
//        Console.WriteLine($"Всего посетителей: {visitors.Count}");

//        var movies = await movieService.GetAllAsync();
//        Console.WriteLine($"Всего фильмов: {movies.Count}");

//        var tickets = await ticketService.GetAllAsync();
//        Console.WriteLine($"Всего билетов: {tickets.Count}");
//    }

//    static async Task TestPagination(MovieService movieService)
//    {
//        Console.WriteLine("\n=== Проверка пагинации ===");

//        var pagination = new Pagination { PageSize = 2, PageNumber = 1 };

//        var paginatedMovies = await movieService.GetMoviesPaginatedAsync(pagination);
//        Console.WriteLine($"Страница {pagination.PageNumber}, фильмов на странице: {paginatedMovies.Count}");

//        var totalMovies = await movieService.GetTotalMoviesCountAsync();
//        Console.WriteLine($"Всего фильмов в БД: {totalMovies}");

//        pagination.PageNumber = 2;
//        var secondPageMovies = await movieService.GetMoviesPaginatedAsync(pagination);
//        Console.WriteLine($"Страница {pagination.PageNumber}, фильмов на странице: {secondPageMovies.Count}");
//    }

//}

