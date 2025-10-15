using Microsoft.EntityFrameworkCore;
using LabWork11.Models;

namespace LabWork11.Context
{
    /// <summary>
    /// Контекст базы данных для работы с Entity Framework Core
    /// Задание 5: Работа с фильмами и связанными таблицами
    /// </summary>
    public class AppDbContext : DbContext
    {
        // Основные таблицы для работы
        public DbSet<Film> Films { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<FilmGame> FilmGames { get; set; }
        public DbSet<GamePrice> GamePrices { get; set; }
        public DbSet<DeletedCategory> DeletedCategories { get; set; }
        public DbSet<DeletedVisitor> DeletedVisitors { get; set; }
        public DbSet<EmailChangeVisitor> EmailChangeVisitors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Строка подключения к базе данных
            optionsBuilder.UseSqlServer(
                "Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация таблицы Film - основная таблица для заданий
            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("Film");
                entity.HasKey(e => e.FilmId);

                entity.Property(e => e.FilmId).HasColumnName("FilmId");
                entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Duration).HasColumnName("Duration").IsRequired();
                entity.Property(e => e.ReleaseDate).HasColumnName("ReleaseDate");
                entity.Property(e => e.Description).HasColumnName("Description").HasMaxLength(500);
                entity.Property(e => e.Poster).HasColumnName("Poster");
                entity.Property(e => e.AgeRating).HasColumnName("AgeRating").HasMaxLength(10);
                entity.Property(e => e.StartRental).HasColumnName("StartRental");
                entity.Property(e => e.FinishRental).HasColumnName("FinishRental");
                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            });

            // Остальные таблицы...
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryId).HasColumnName("Categoryid");
                entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");
                entity.HasKey(e => e.GameId);
                entity.Property(e => e.GameId).HasColumnName("Gameld");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
                entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Price).HasColumnName("Price").HasColumnType("decimal(16,2)").IsRequired();
                entity.Property(e => e.Description).HasColumnName("Description").HasMaxLength(500);
                entity.Property(e => e.IsDeleted).HasColumnName("IsDeleted").IsRequired();
                entity.Property(e => e.KeysAmount).HasColumnName("KeysAmount");
            });

            // Другие таблицы опущены для краткости...
        }
    }
}