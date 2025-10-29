using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Visitor
{
    [Key]
    public int VisitorId { get; set; }

    [Required]
    [MaxLength(11)]
    public string Phone { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    public DateTime? BirthDate { get; set; }

    [MaxLength(150)]
    public string Email { get; set; }

    // Навигационные свойства
    public virtual ICollection<Ticket> Tickets { get; set; }

    public Visitor()
    {
        Tickets = new HashSet<Ticket>();
    }
}