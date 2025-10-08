using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork9.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        public int TicketId { get; set; }
        public int SessionId { get; set; }
        public int VisitorId { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
    }
}
