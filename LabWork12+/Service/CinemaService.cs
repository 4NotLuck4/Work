using Microsoft.EntityFrameworkCore;
using System.Data;

public class CinemaService
{
    private readonly CinemaContext _context;

    public CinemaService(CinemaContext context)
    {
        _context = context;
    }

    //// 5.2 
    //public List<Film> GetFilmsByNameAndYear(string name, int minYear)
    //{
    //    if (string.IsNullOrWhiteSpace(name))
    //        throw new ArgumentException("Название не может быть пустым");

    //    return _context.Films
    //        .FromSql($"SELECT * FROM Film WHERE Name LIKE '%' + {name} + '%' AND ReleaseYear >= {minYear} AND isDeleted = 0")
    //        .AsNoTracking()
    //        .ToList();
    //}

    // 5.4 
    public List<string> GetFilmGenres(int filmId)
    {
        var connection = _context.Database.GetDbConnection();
        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT g.Name 
            FROM Genre g 
            INNER JOIN FilmGenre fg ON g.GenreId = fg.GenreId 
            WHERE fg.FilmId = @filmId
            ORDER BY g.Name";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@filmId";
        parameter.Value = filmId;
        parameter.DbType = DbType.Int32;
        command.Parameters.Add(parameter);

        var result = new List<string>();

        try
        {
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                    result.Add(reader.GetString(0));
            }
        }
        finally
        {
            connection.Close();
        }

        return result;
    }

    // 5.5
    public DateTime? GetSessionDateByTicket(int ticketId)
    {
        var connection = _context.Database.GetDbConnection();
        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT s.StartDate 
            FROM Session s 
            INNER JOIN Ticket t ON s.SessionId = t.SessionId 
            WHERE t.TicketId = @ticketId";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@ticketId";
        parameter.Value = ticketId;
        parameter.DbType = DbType.Int32;
        command.Parameters.Add(parameter);

        try
        {
            connection.Open();
            var result = command.ExecuteScalar();
            return result as DateTime?;
        }
        finally
        {
            connection.Close();
        }
    }
}