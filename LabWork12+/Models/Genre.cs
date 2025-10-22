using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Genre
{
    [Key]
    public int GenreId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    // Навигационные свойства
    public virtual ICollection<FilmGenre> FilmGenres { get; set; }
    
    public Genre()
    {
        FilmGenres = new HashSet<FilmGenre>();
    }
}