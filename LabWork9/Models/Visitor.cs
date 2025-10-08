using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork9.Models
{
    [Table("Visitor")]
    public class Visitor
    {
        public int VisitorId { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
