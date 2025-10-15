using System.ComponentModel.DataAnnotations;

namespace LabWork11.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Название игры обязательно")]
        [StringLength(100, ErrorMessage = "Название игры не должно превышать 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Цена должна быть указана")]
        [Range(0.01, 1000000, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Статус удаления должен быть указан")]
        public bool IsDeleted { get; set; }

        public short? KeysAmount { get; set; }
    }
}