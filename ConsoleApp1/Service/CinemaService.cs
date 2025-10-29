// CinemaService.cs
using Microsoft.EntityFrameworkCore;

public class CinemaService
{
    private readonly CinemaContext _context;

    public CinemaService(CinemaContext context)
    {
        _context = context;
    }

    public List<Film> GetFilmsSorted(string sortColumn)
    {
        var validColumns = new[] { "Name", "ReleaseYear", "Duration", "FilmId" };
        if (!validColumns.Contains(sortColumn))
            throw new ArgumentException($"Недопустимый столбец: {sortColumn}");

        return _context.Films
            .FromSqlRaw($"SELECT * FROM Film WHERE isDeleted = 0 ORDER BY {sortColumn}")
            .AsNoTracking()
            .ToList();
    }

    public void SeedTestData()
    {
        _context.Database.ExecuteSqlRaw(@"
            INSERT INTO Film (Name, Duration, ReleaseYear, Description, isDeleted) VALUES 
            ('Аватар: Путь воды', 192, 2022, 'Продолжение эпической саги', 0),
            ('Оппенгеймер', 180, 2023, 'История создания атомной бомбы', 0),
            ('Барби', 114, 2023, 'Приключения в мире Барби', 0),
            ('Джон Уик 4', 169, 2023, 'Продолжение истории наемного убийцы', 0)");
    }
}