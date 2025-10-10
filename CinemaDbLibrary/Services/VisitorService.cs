using Microsoft.EntityFrameworkCore;
using CinemaDbLibrary.Context;
using CinemaDbLibrary.Models;

namespace CinemaDbLibrary.Services
{
    public class VisitorService : BaseService<Visitor>
    {
        public VisitorService(CinemaDbContext context) : base(context) { }

        // 3.1.4 Включение загрузки связанных данных
        public override async Task<Visitor?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(v => v.Tickets)
                .FirstOrDefaultAsync(v => v.VisitorId == id);
        }

        public override async Task<List<Visitor>> GetAllAsync()
        {
            return await _dbSet
                .Include(v => v.Tickets)
                .ToListAsync();
        }
        public async Task<List<Visitor>> GetVisitorsPaginatedAsync(Pagination pagination)
        {
            return await _dbSet
                .Include(v => v.Tickets)
                .Skip(pagination.Skip)
                .Take(pagination.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalVisitorsCountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}