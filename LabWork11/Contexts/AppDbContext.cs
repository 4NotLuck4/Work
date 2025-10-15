using LabWork11.Models;
using Microsoft.EntityFrameworkCore;
namespace LabWork11.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True");
        }
    }
}