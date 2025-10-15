using System.ComponentModel.DataAnnotations;

namespace LabWork11.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Название категории обязательно")]
        [StringLength(100, ErrorMessage = "Название категории не должно превышать 100 символов")]
        public string Name { get; set; }
    }
}