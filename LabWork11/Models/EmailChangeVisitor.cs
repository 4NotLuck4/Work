using System;

namespace LabWork11.Models
{
    public class EmailChangeVisitor
    {
        public int VisitorId { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}