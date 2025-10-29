using System.ComponentModel.DataAnnotations;

public class Ticket
{
    [Key]
    public int TicketId { get; set; }

    [Required]
    public int SessionId { get; set; }

    [Required]
    public int VisitorId { get; set; }

    [Required]
    public byte Row { get; set; }

    [Required]
    public byte Seat { get; set; }

    // Навигационные свойства
    public virtual Session Session { get; set; }
    public virtual Visitor Visitor { get; set; }
}