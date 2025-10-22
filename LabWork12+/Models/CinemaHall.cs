using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CinemaHall
{
    [Key]
    public byte HallId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string CinemaName { get; set; }
    
    [Required]
    public byte RowsCount { get; set; }
    
    [Required]
    public byte SeatsCount { get; set; }
    
    [Required]
    public bool IsVip { get; set; }
    
    // Навигационные свойства
    public virtual ICollection<Session> Sessions { get; set; }
    
    public CinemaHall()
    {
        Sessions = new HashSet<Session>();
    }
}