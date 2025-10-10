using Microsoft.EntityFrameworkCore;
using CinemaDbLibrary.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CinemaDbLibrary.Context
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связей
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Visitor)
                .WithMany(v => v.Tickets)
                .HasForeignKey(t => t.VisitorId);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId);

            // Добавляем связь Ticket-Movie через SessionId
            modelBuilder.Entity<Ticket>()
                .HasOne<Movie>()
                .WithMany(m => m.Tickets)
                .HasForeignKey(t => t.SessionId)
                .HasPrincipalKey(m => m.MovieId);
        }
    }
}