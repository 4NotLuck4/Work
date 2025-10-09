using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services
{
    public class TicketService(AppDbContext context) : IService<Ticket>
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Ticket>> GetAsync()
            => await _context.Tickets.ToListAsync();

        public async Task AddAsync(Ticket entity)
        {
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket is not null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Ticket entity)
        {
        }
    }
}
