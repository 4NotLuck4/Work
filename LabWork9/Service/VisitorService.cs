using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services
{
    public class VisitorService(AppDbContext context) : IService<Visitor>
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Visitor>> GetAsync()
            => await _context.Visitors.ToListAsync();

        public async Task AddAsync(Visitor entity)
        {
        }

        public async Task DeleteAsync(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor is not null)
            {
                _context.Visitors.Remove(visitor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Visitor entity)
        {
        }
    }
}
