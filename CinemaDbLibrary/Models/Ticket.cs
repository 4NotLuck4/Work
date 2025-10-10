using CinemaDbLibrary.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDbLibrary.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        public int TicketId { get; set; }
        public int SessionId { get; set; }
        public int VisitorId { get; set; }
        public byte Row { get; set; }
        public byte Seat { get; set; }

        public Visitor? Visitor { get; set; }
        public Film? Film { get; set; }
    }
}
