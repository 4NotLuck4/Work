using Microsoft.EntityFrameworkCore;
using CinemaDbLibrary.Context;
using CinemaDbLibrary.Models;

namespace CinemaDbLibrary.Services
{
    public class GenreService : BaseService<Genre>
    {
        public GenreService(CinemaContext context) : base(context) { }

        // 3.1.4 Включение загрузки связанных данных
        public override async Task<Genre?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(g => g.Movies)
                .FirstOrDefaultAsync(g => g.GenreId == id);
        }

        public override async Task<List<Genre>> GetAllAsync()
        {
            return await _dbSet
                .Include(g => g.Movies)
                .ToListAsync();
        }
    }
}