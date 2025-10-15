using System.Windows;
using System.Text;
using LabWork11.Services;
using LabWork11.Models;

namespace LabWork11.Views
{
    /// <summary>
    /// Окно для добавления и редактирования фильмов
    /// </summary>
    public partial class FilmWindow : Window
    {
        private readonly FilmService _filmService;
        private Film _film;

        /// <summary>
        /// Задание 5.3.1: Конструктор для добавления фильма
        /// </summary>
        public FilmWindow()
        {
            InitializeComponent();
            _filmService = new FilmService();
            _film = new Film();
            DataContext = _film;
            LoadAgeRatings();
        }

        /// <summary>
        /// Задание 5.4.1: Конструктор для редактирования фильма
        /// </summary>
        public FilmWindow(Film film = null)
        {
            InitializeComponent();
            _filmService = new FilmService();

            if (film != null)
            {
                _film = film;
                Title = "Редактирование фильма";
            }
            else
            {
                _film = new Film();
            }

            // Задание 5.3.3: Привязка данных к полю фильм
            DataContext = _film;
            LoadAgeRatings();
        }

        /// <summary>
        /// Задание 5.3.2: Загрузка возрастных рейтингов из БД
        /// </summary>
        private void LoadAgeRatings()
        {
            var ratings = _filmService.GetAgeRatings();
            cmbAgeRating.ItemsSource = ratings;
        }

        /// <summary>
        /// Задание 5.3.4: Сохранение данных фильма
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Задание 5.5.2: Проверка корректности данных
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_film.Name))
                errors.AppendLine("Название обязательно должно быть указано");

            if (_film.Duration <= 0)
                errors.AppendLine("Длительность должна быть положительным числом");

            if (_film.ReleaseDate < 1900 || _film.ReleaseDate > 2100)
                errors.AppendLine("Год выпуска должен быть от 1900 до 2100");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Задание 5.4.3: Определение операции (добавление или обновление)
                if (_film.FilmId == 0)
                {
                    _filmService.AddFilm(_film);
                }
                else
                {
                    _filmService.UpdateFilm(_film);
                }

                // Задание 5.5.1: Информация об успешном сохранении
                MessageBox.Show("Данные успешно сохранены.", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                DialogResult = true;
                Close();
            }
            catch (System.Exception ex)
            {
                // Задание 5.3.5 и 5.5.3: Перехват исключений
                MessageBox.Show($"Не удалось сохранить запись. Причина: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}