using Microsoft.EntityFrameworkCore;
using CinemaDbLibrary.Context;
using CinemaDbLibrary.Models;

namespace CinemaDbLibrary.Services
{
    public class TicketService : BaseService<Ticket>
    {
        public TicketService(CinemaContext context) : base(context) { }

        // 3.1.4 Включение загрузки связанных данных
        public override async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Visitor)
                .FirstOrDefaultAsync(t => t.TicketId == id);
        }

        public override async Task<List<Ticket>> GetAllAsync()
        {
            return await _dbSet
                .Include(t => t.Visitor)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsSortedAsync(Sorting sorting, Pagination? pagination = null)
        {
            var query = _dbSet.Include(t => t.Visitor);

            // Сортировка
            query = sorting.SortBy?.ToLower() switch
            {
                "row" => sorting.Ascending ?
                    query.OrderBy(t => t.Row) :
                    query.OrderByDescending(t => t.Row),
                "seat" => sorting.Ascending ?
                    query.OrderBy(t => t.Seat) :
                    query.OrderByDescending(t => t.Seat),
                _ => sorting.Ascending ?
                    query.OrderBy(t => t.TicketId) :
                    query.OrderByDescending(t => t.TicketId)
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