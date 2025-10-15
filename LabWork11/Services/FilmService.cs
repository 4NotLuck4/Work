using LabWork11.Contexts;
using LabWork11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork11.Services
{
    class FilmService
    {
        private readonly AppDbContext _context;
        public FilmService(AppDbContext context)
        {
            _context = context;
        }
        public List<Film> GetFilms()
        {
            return _context.Films.ToList();
        }

        public void DeleteFilms(List<Film> filmsToDelete)
        {
            _context.Films.RemoveRange(filmsToDelete);
            _context.SaveChanges();
        }
    }
}
