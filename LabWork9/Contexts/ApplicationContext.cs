using LabWork9.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LabWork9.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Visitor> Visitors => Set<Visitor>();
        public DbSet<Ticket> Tickets => Set<Ticket>();

        public ApplicationContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3102;Persist Security Info=True;User ID=ispp3102;Password=3102;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
