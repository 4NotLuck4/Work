using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Session
{
    [Key]
    public int SessionId { get; set; }

    [Required]
    public int FilmId { get; set; }

    [Required]
    public byte HallId { get; set; }

    public decimal? Price { get; set; }
    public DateTime? StartDate { get; set; }
    public bool? IsFilm3d { get; set; }

    // Навигационные свойства
    public virtual Film Film { get; set; }
    public virtual CinemaHall CinemaHall { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }

    public Session()
    {
        Tickets = new HashSet<Ticket>();
    }
}