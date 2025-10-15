using System;
using System.ComponentModel.DataAnnotations;

namespace LabWork11.Models
{
    public class Visitor
    {
        public int VisitorId { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        [StringLength(10, ErrorMessage = "Телефон должен содержать 10 символов")]
        public string Phone { get; set; }

        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [StringLength(150, ErrorMessage = "Email не должен превышать 150 символов")]
        public string Email { get; set; }
    }
}