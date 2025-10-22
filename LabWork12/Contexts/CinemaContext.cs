using LabWork12.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12.Contexts
{
    public class CinemaContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.FilmId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            } );
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.SessionId);
                entity.HasOne(e => e.FilmId)
                    .WithMany(f => f.Sessions)
                    .HasForeignKey(e => e.FilmId);
            });
        }
    }

    
}
