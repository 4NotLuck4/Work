using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDbLibrary.Models
{
    [Table("Film")]
    public class Film
    {
        public int FilmId { get; set; }
        public string Title { get; set; } = null!;
        public int Duration { get; set; }
        public int ReleaseYear { get; set; }
        public int GenreId { get; set; }

        public Genre? Genre { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}