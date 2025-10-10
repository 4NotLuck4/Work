using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDbLibrary.Models
{
    [Table("Film")]
    public class Film
    {
        public int FilmId { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; }
        public int Duration { get; set; }
        public List<Genre> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}