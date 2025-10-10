using Microsoft.EntityFrameworkCore;
using CinemaDbLibrary.Context;
using CinemaDbLibrary.Models;

namespace CinemaDbLibrary.Services
{
    public class FilmService : BaseService<Film>
    {
        public FilmService(CinemaContext context) : base(context) { }

        // 3.1.4 Включение загрузки связанных данных
        public override async Task<Film?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(m => m.Genre)
                .Include(m => m.Tickets)
                .FirstOrDefaultAsync(m => m.FilmId == id);
        }

        public override async Task<List<Film>> GetAllAsync()
        {
            return await _dbSet
                .Include(m => m.Genre)
                .Include(m => m.Tickets)
                .ToListAsync();
        }
        public async Task<List<Film>> GetFilmsPaginatedAsync(Pagination pagination)
        {
            return await _dbSet
                .Include(m => m.Genre)
                .Include(m => m.Tickets)
                .Skip(pagination.Skip)
                .Take(pagination.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalFilmsCountAsync()
        {
            return await _dbSet.CountAsync();
        }
        public async Task<List<Film>> GetFilmsSortedAsync(Sorting sorting, Pagination? pagination = null)
        {
            var query = _dbSet.Include(m => m.Genre).Include(m => m.Tickets);

            // Сортировка
            query = sorting.SortBy?.ToLower() switch
            {
                "title" => sorting.Ascending ?
                    query.OrderBy(m => m.Title) :
                    query.OrderByDescending(m => m.Title),
                "duration" => sorting.Ascending ?
                    query.OrderBy(m => m.Duration) :
                    query.OrderByDescending(m => m.Duration),
                "releaseyear" => sorting.Ascending ?
                    query.OrderBy(m => m.ReleaseYear) :
                    query.OrderByDescending(m => m.ReleaseYear),
                _ => sorting.Ascending ?
                    query.OrderBy(m => m.MovieId) :
                    query.OrderByDescending(m => m.MovieId)
            };

            // Пагинация (если указана)
            if (pagination != null)
            {
                query = query.Skip(pagination.Skip).Take(pagination.PageSize);
            }

            return await query.ToListAsync();
        }
    }
}