using System;
using System.ComponentModel.DataAnnotations;

namespace LabWork11.Models
{
    /// <summary>
    /// Модель фильма для работы с таблицей Film
    /// Задание 5.3.1: Модель данных для работы с фильмами
    /// </summary>
    public class Film
    {
        public int FilmId { get; set; }

        [Required(ErrorMessage = "Название обязательно должно быть указано")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Длительность должна быть указана")]
        [Range(1, 500, ErrorMessage = "Длительность должна быть от 1 до 500 минут")]
        public short Duration { get; set; }

        [Required(ErrorMessage = "Год выпуска должен быть указан")]
        [Range(1900, 2100, ErrorMessage = "Год выпуска должен быть от 1900 до 2100")]
        public short ReleaseDate { get; set; }

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string Description { get; set; }

        public byte[] Poster { get; set; }

        [StringLength(10, ErrorMessage = "Возрастной рейтинг не должен превышать 10 символов")]
        public string AgeRating { get; set; }

        public DateTime? StartRental { get; set; }
        public DateTime? FinishRental { get; set; }
        public bool? IsDeleted { get; set; }
    }
}