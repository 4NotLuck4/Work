using Microsoft.EntityFrameworkCore;
using System.Linq;

public class CinemaContext : DbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<FilmGenre> FilmGenres { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<CinemaHall> CinemaHalls { get; set; }
    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    // DbSet для результатов хранимых процедур
    public DbSet<TicketInfo> TicketInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=HOLLOW;Initial Catalog=mssql;User ID=ispp3102;Password=3102;Trust Server Certificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Составной ключ для FilmGenre
        modelBuilder.Entity<FilmGenre>()
            .HasKey(fg => new { fg.FilmId, fg.GenreId });

        // Уникальный индекс для Ticket (место в зале)
        modelBuilder.Entity<Ticket>()
            .HasIndex(t => new { t.SessionId, t.Row, t.Seat })
            .IsUnique();

        // Настройка TicketInfo как keyless entity для хранимых процедур
        modelBuilder.Entity<TicketInfo>()
            .HasNoKey()
            .ToView(null);
    }
}