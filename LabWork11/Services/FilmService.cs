using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LabWork11.Context;
using LabWork11.Models;
using LabWork11.Pagination;
using LabWork11.Filters;

namespace LabWork11.Services
{
    /// <summary>
    /// Сервис для работы с фильмами
    /// Задание 5: Создание сервиса для работы с БД
    /// </summary>
    public class FilmService
    {
        private readonly AppDbContext _context;

        public FilmService()
        {
            _context = new AppDbContext();
        }

        /// <summary>
        /// Задание 5.1.1: Получение списка фильмов для отображения в DataGrid
        /// </summary>
        public List<Film> GetAllFilms()
        {
            return _context.Films
                .Where(f => f.IsDeleted != true)
                .OrderBy(f => f.Name)
                .ToList();
        }

        /// <summary>
        /// Задание 5.1.3: Удаление фильма по ID
        /// </summary>
        public void DeleteFilm(int filmId)
        {
            try
            {
                var film = _context.Films.Find(filmId);
                if (film != null)
                {
                    _context.Films.Remove(film);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Задание 5.1.4: Перехват исключений
                throw new Exception($"Не удалось удалить запись. Причина: {ex.Message}");
            }
        }

        /// <summary>
        /// Задание 5.1.3: Удаление нескольких фильмов
        /// </summary>
        public void DeleteFilms(List<int> filmIds)
        {
            try
            {
                var films = _context.Films
                    .Where(f => filmIds.Contains(f.FilmId))
                    .ToList();

                _context.Films.RemoveRange(films);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Задание 5.1.4: Перехват исключений
                throw new Exception($"Не удалось удалить записи. Причина: {ex.Message}");
            }
        }

        /// <summary>
        /// Задание 5.3.2: Получение уникальных возрастных рейтингов
        /// </summary>
        public List<string> GetAgeRatings()
        {
            var ratings = _context.Films
                .Where(f => f.AgeRating != null && f.AgeRating != "")
                .Select(f => f.AgeRating)
                .Distinct()
                .ToList();

            // Добавляем дополнительные рейтинги
            ratings.Add("G");
            ratings.Add("PG-13");

            return ratings.OrderBy(r => r).ToList();
        }

        /// <summary>
        /// Задание 5.3.4: Добавление нового фильма
        /// </summary>
        public void AddFilm(Film film)
        {
            try
            {
                // Задание 5.3.4: Обрезаем пробелы по краям строки
                if (!string.IsNullOrEmpty(film.Name))
                    film.Name = film.Name.Trim();

                if (!string.IsNullOrEmpty(film.Description))
                    film.Description = film.Description.Trim();

                if (!string.IsNullOrEmpty(film.AgeRating))
                    film.AgeRating = film.AgeRating.Trim();

                film.IsDeleted = false;

                _context.Films.Add(film);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Задание 5.3.5: Перехват исключений
                throw new Exception($"Не удалось сохранить запись. Причина: {ex.Message}");
            }
        }

        /// <summary>
        /// Задание 5.4.3: Обновление существующего фильма
        /// </summary>
        public void UpdateFilm(Film film)
        {
            try
            {
                // Задание 5.3.4: Обрезаем пробелы по краям строки
                if (!string.IsNullOrEmpty(film.Name))
                    film.Name = film.Name.Trim();

                if (!string.IsNullOrEmpty(film.Description))
                    film.Description = film.Description.Trim();

                if (!string.IsNullOrEmpty(film.AgeRating))
                    film.AgeRating = film.AgeRating.Trim();

                _context.Films.Update(film);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Задание 5.3.5: Перехват исключений
                throw new Exception($"Не удалось сохранить запись. Причина: {ex.Message}");
            }
        }

        /// <summary>
        /// Получение фильма по ID
        /// </summary>
        public Film GetFilmById(int id)
        {
            return _context.Films.Find(id);
        }

        /// <summary>
        /// Получение фильмов с пагинацией и фильтрацией
        /// </summary>
        public PagedList<Film> GetPagedFilms(int pageNumber, int pageSize, FilmFilter filter = null)
        {
            var query = _context.Films
                .Where(f => f.IsDeleted != true)
                .AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                    query = query.Where(f => f.Name.Contains(filter.Name));

                if (filter.MinDuration.HasValue)
                    query = query.Where(f => f.Duration >= filter.MinDuration.Value);

                if (filter.MaxDuration.HasValue)
                    query = query.Where(f => f.Duration <= filter.MaxDuration.Value);

                if (!string.IsNullOrEmpty(filter.AgeRating))
                    query = query.Where(f => f.AgeRating == filter.AgeRating);
            }

            query = query.OrderBy(f => f.Name);

            return PagedList<Film>.ToPagedList(query, pageNumber, pageSize);
        }
    }
}