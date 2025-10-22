using LabWork12_.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class CinemaService
{
    private readonly CinemaContext _context;

    public CinemaService(CinemaContext context)
    {
        _context = context;
    }

    // 5.1 FromSqlRaw - сортировка фильмов по указанному столбцу
    public List<Film> GetFilmsSorted(string sortColumn)
    {
        var validColumns = new[] { "Name", "ReleaseYear", "Duration" };
        if (!validColumns.Contains(sortColumn))
            throw new ArgumentException("Недопустимый столбец для сортировки");

        return _context.Films
            .FromSqlRaw($"SELECT * FROM Film ORDER BY {sortColumn}")
            .AsNoTracking()
            .ToList();
    }

    // 5.2 FromSql с параметрами - фильмы по названию и году
    public List<Film> GetFilmsByNameAndYear(string name, int minYear)
    {
        return _context.Films
            .FromSqlInterpolated($"SELECT * FROM Film WHERE Name LIKE '%' + {name} + '%' AND ReleaseYear >= {minYear}")
            .AsNoTracking()
            .ToList();
    }

    // 5.3 ExecuteSql - увеличение цены сеансов
    public int IncreaseSessionPrice(byte hallId, decimal increaseAmount)
    {
        return _context.Database.ExecuteSqlRaw(
            "UPDATE Session SET Price = Price + {0} WHERE HallId = {1}",
            increaseAmount, hallId);
    }

    // 5.4 SqlQuery - список жанров фильма
    public List<string> GetFilmGenres(int filmId)
    {
        var connection = _context.Database.GetDbConnection();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT g.Name FROM Genre g INNER JOIN FilmGenre fg ON g.GenreId = fg.GenreId WHERE fg.FilmId = @filmId";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@filmId";
        parameter.Value = filmId;
        command.Parameters.Add(parameter);

        var result = new List<string>();

        try
        {
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader.GetString(0));
            }
        }
        finally
        {
            connection.Close();
        }

        return result;
    }

    // 5.5 SqlQuery - дата сеанса по номеру билета
    public DateTime? GetSessionDateByTicket(int ticketId)
    {
        return _context.Sessions
            .FromSqlRaw("SELECT s.* FROM Session s INNER JOIN Ticket t ON s.SessionId = t.SessionId WHERE t.TicketId = {0}", ticketId)
            .AsNoTracking()
            .Select(s => s.StartDate)
            .FirstOrDefault();
    }

    // 5.6.1 EF.Functions.Like - фильмы по диапазону букв
    public List<Film> GetFilmsByLetterRange(char start, char end)
    {
        return _context.Films
            .Where(f => EF.Functions.Like(f.Name, $"[{start}-{end}]%"))
            .AsNoTracking()
            .ToList();
    }

    // 5.6.2 Агрегатные функции - статистика цен сеансов
    public (decimal min, decimal max, decimal avg) GetSessionPriceStats(int filmId)
    {
        var stats = _context.Sessions
            .Where(s => s.FilmId == filmId && s.Price.HasValue)
            .AsNoTracking()
            .GroupBy(s => 1)
            .Select(g => new
            {
                MinPrice = g.Min(s => s.Price),
                MaxPrice = g.Max(s => s.Price),
                AvgPrice = g.Average(s => s.Price)
            })
            .FirstOrDefault();

        return (stats?.MinPrice ?? 0, stats?.MaxPrice ?? 0, stats?.AvgPrice ?? 0);
    }

    // 5.7 Вызов хранимой процедуры - билеты по номеру телефона
    public List<TicketInfo> GetVisitorTickets(string phone)
    {
        return _context.TicketInfos
            .FromSqlRaw("EXEC GetVisitorTickets @Phone = {0}", phone)
            .AsNoTracking()
            .ToList();
    }

    // 5.8 Вызов хранимой процедуры с выходным параметром
    public int AddVisitorWithOutput(string phone, out int visitorId)
    {
        var phoneParam = new SqlParameter("@Phone", phone);
        var visitorIdParam = new SqlParameter
        {
            ParameterName = "@VisitorId",
            SqlDbType = System.Data.SqlDbType.Int,
            Direction = System.Data.ParameterDirection.Output
        };

        var result = _context.Database.ExecuteSqlRaw(
            "EXEC AddVisitor @Phone, @VisitorId OUTPUT",
            phoneParam, visitorIdParam);

        visitorId = (int)(visitorIdParam.Value ?? 0);
        return result;
    }

    // 5.9 Вызов табличной функции
    public List<Session> GetFilmSessions(int filmId)
    {
        return _context.Sessions
            .FromSqlRaw("SELECT * FROM dbo.GetFilmSessions({0})", filmId)
            .AsNoTracking()
            .ToList();
    }
}