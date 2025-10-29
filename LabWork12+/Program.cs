using LabWork12_1.Context;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using var context = new CinemaContext();
        var service = new CinemaService(context);

        Console.WriteLine("Лабораторная работа №12 - SQL команды в EF Core");
        Console.WriteLine("==============================================");

        try
        {
            // 5.1 Тестирование сортировки
            Console.WriteLine("\n5.1 Фильмы отсортированные по названию:");
            var filmsSorted = service.GetFilmsSorted("Name");
            filmsSorted.ForEach(f => Console.WriteLine($"  {f.Name} ({f.ReleaseYear})"));

            // 5.2 Фильмы по названию и году
            Console.WriteLine("\n5.2 Фильмы с 'Аватар' с 2020 года:");
            var filmsFiltered = service.GetFilmsByNameAndYear("Аватар", 2020);
            filmsFiltered.ForEach(f => Console.WriteLine($"  {f.Name} - {f.ReleaseYear}"));

            // 5.3 Увеличение цены
            Console.WriteLine("\n5.3 Увеличение цены сеансов:");
            var updatedRows = service.IncreaseSessionPrice(1, 50);
            Console.WriteLine($"  Обновлено строк: {updatedRows}");

            // 5.4 Жанры фильма
            Console.WriteLine("\n5.4 Жанры фильма (ID=1):");
            var genres = service.GetFilmGenres(1);
            genres.ForEach(g => Console.WriteLine($"  {g}"));

            // 5.5 Дата сеанса по билету
            Console.WriteLine("\n5.5 Дата сеанса для билета ID=1:");
            var sessionDate = service.GetSessionDateByTicket(1);
            Console.WriteLine($"  {sessionDate}");

            // 5.6.1 Фильмы по диапазону букв
            Console.WriteLine("\n5.6.1 Фильмы от А до Д:");
            var filmsByLetters = service.GetFilmsByLetterRange('А', 'Д');
            filmsByLetters.ForEach(f => Console.WriteLine($"  {f.Name}"));

            // 5.6.2 Статистика цен
            Console.WriteLine("\n5.6.2 Статистика цен для фильма ID=1:");
            var stats = service.GetSessionPriceStats(1);
            Console.WriteLine($"  Min: {stats.min}, Max: {stats.max}, Avg: {stats.avg:F2}");

            // 5.7 Билеты посетителя
            Console.WriteLine("\n5.7 Билеты посетителя:");
            var tickets = service.GetVisitorTickets("79101234567");
            if (tickets.Any())
                tickets.ForEach(t => Console.WriteLine($"  {t.FilmName} - {t.SessionDate}"));
            else
                Console.WriteLine("  Билеты не найдены");

            // 5.8 Добавление посетителя
            Console.WriteLine("\n5.8 Добавление посетителя:");
            var result = service.AddVisitorWithOutput("79101234599", out int visitorId);
            Console.WriteLine($"  Добавлен посетитель с ID: {visitorId}");

            // 5.9 Сеансы фильма
            Console.WriteLine("\n5.9 Сеансы фильма ID=1:");
            var sessions = service.GetFilmSessions(1);
            sessions.ForEach(s => Console.WriteLine($"  {s.StartDate} - {s.Price} руб."));

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\nРабота завершена! Нажмите любую клавишу...");
        Console.ReadKey();
    }
}