using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CinemaDbLibrary.Context;
using CinemaDbLibrary.Services;
using CinemaDbLibrary.Models;
using MathNet.Numerics;

class Program
{
    static async Task Main(string[] args)
    {
        // Настройка DI контейнера
        var services = new ServiceCollection();

        services.AddDbContext<CinemaDbContext>(options =>
            options.UseSqlServer(@"Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True"));

        services.AddScoped<VisitorService>();
        services.AddScoped<MovieService>();
        services.AddScoped<TicketService>();
        services.AddScoped<GenreService>();

        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var visitorService = scope.ServiceProvider.GetRequiredService<VisitorService>();
        var movieService = scope.ServiceProvider.GetRequiredService<MovieService>();
        var ticketService = scope.ServiceProvider.GetRequiredService<TicketService>();

        try
        {
            await TestBasicOperations(visitorService, movieService, ticketService);
            await TestPagination(movieService);
            await TestSorting(movieService, ticketService);

            Console.WriteLine("\nВсе тесты завершены успешно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static async Task TestBasicOperations(VisitorService visitorService, MovieService movieService, TicketService ticketService)
    {
        Console.WriteLine("=== Проверка базовых операций ===");

        var visitors = await visitorService.GetAllAsync();
        Console.WriteLine($"Всего посетителей: {visitors.Count}");

        var movies = await movieService.GetAllAsync();
        Console.WriteLine($"Всего фильмов: {movies.Count}");

        var tickets = await ticketService.GetAllAsync();
        Console.WriteLine($"Всего билетов: {tickets.Count}");
    }

    static async Task TestPagination(MovieService movieService)
    {
        Console.WriteLine("\n=== Проверка пагинации ===");

        var pagination = new Pagination { PageSize = 2, PageNumber = 1 };

        var paginatedMovies = await movieService.GetMoviesPaginatedAsync(pagination);
        Console.WriteLine($"Страница {pagination.PageNumber}, фильмов на странице: {paginatedMovies.Count}");

        var totalMovies = await movieService.GetTotalMoviesCountAsync();
        Console.WriteLine($"Всего фильмов в БД: {totalMovies}");

        pagination.PageNumber = 2;
        var secondPageMovies = await movieService.GetMoviesPaginatedAsync(pagination);
        Console.WriteLine($"Страница {pagination.PageNumber}, фильмов на странице: {secondPageMovies.Count}");
    }

    static async Task TestSorting(MovieService movieService, TicketService ticketService)
    {
        Console.WriteLine("\n=== Проверка сортировки ===");

        // Сортировка фильмов по названию
        var sortingByTitle = new Sorting { SortBy = "title", Ascending = true };
        var sortedMovies = await movieService.GetMoviesSortedAsync(sortingByTitle);
        Console.WriteLine("Фильмы отсортированы по названию (A-Z):");
        foreach (var movie in sortedMovies.Take(3))
        {
            Console.WriteLine($"- {movie.Title} ({movie.ReleaseYear})");
        }

        // Сортировка фильмов по году выпуска
        var sortingByYear = new Sorting { SortBy = "releaseyear", Ascending = false };
        var sortedMoviesDesc = await movieService.GetMoviesSortedAsync(sortingByYear);
        Console.WriteLine("\nФильмы отсортированы по году выпуска (новые сначала):");
        foreach (var movie in sortedMoviesDesc.Take(3))
        {
            Console.WriteLine($"- {movie.Title} ({movie.ReleaseYear})");
        }

        // Комбинированная сортировка с пагинацией
        var combinedSorting = new Sorting { SortBy = "title", Ascending = true };
        var combinedPagination = new Pagination { PageSize = 2, PageNumber = 1 };
        var combinedResult = await movieService.GetMoviesSortedAsync(combinedSorting, combinedPagination);
        Console.WriteLine($"\nКомбинированная сортировка + пагинация (стр. {combinedPagination.PageNumber}):");
        foreach (var movie in combinedResult)
        {
            Console.WriteLine($"- {movie.Title}");
        }
    }
}