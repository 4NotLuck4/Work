using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Film
{
    public int FilmId { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public int ReleaseYear { get; set; }
    public string Description { get; set; }
    
    public byte[] Poster { get; set; }
    public string AgeLimit { get; set; }
    
    public System.DateTime? RentalStart { get; set; }
    public System.DateTime? RentalFinish { get; set; }
    public bool isDeleted { get; set; }
    
    // Навигационные свойства
    public virtual ICollection<FilmGenre> FilmGenres { get; set; }
    public virtual ICollection<Session> Sessions { get; set; }
    
    public Film()
    {
        FilmGenres = new HashSet<FilmGenre>();
        Sessions = new HashSet<Session>();
    }
}