using System;
using System.ComponentModel.DataAnnotations;

namespace LabWork12.Models
{
    public class Visitor
    {
        public int VisitorId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public List<Ticket> Tickets { get; set; } = new();

    }
}