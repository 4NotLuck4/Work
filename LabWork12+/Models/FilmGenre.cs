using System.ComponentModel.DataAnnotations;

public class FilmGenre
{
    [Key]
    public int FilmId { get; set; }
    
    [Key]
    public int GenreId { get; set; }
    
    // Навигационные свойства
    public virtual Film Film { get; set; }
    public virtual Genre Genre { get; set; }
}