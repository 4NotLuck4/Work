using System;

namespace LabWork11.Models
{
    public class DeletedVisitor
    {
        public int VisitorId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedInformation { get; set; }
    }
}