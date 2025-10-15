using System.Windows;
using System.Windows.Controls;
using LabWork11.Services;
using System.Linq;

namespace LabWork11.Views
{
    /// <summary>
    /// Главное окно приложения
    /// Задание 5: Основное окно для работы с фильмами
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FilmService _filmService;

        public MainWindow()
        {
            InitializeComponent();
            _filmService = new FilmService();
            LoadFilms();
        }

        /// <summary>
        /// Задание 5.1.1: Загрузка фильмов в DataGrid
        /// </summary>
        private void LoadFilms()
        {
            dataGridFilms.ItemsSource = _filmService.GetAllFilms();
        }

        /// <summary>
        /// Задание 5.3.1: Открытие окна добавления фильма
        /// </summary>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var filmWindow = new FilmWindow();
            if (filmWindow.ShowDialog() == true)
            {
                // Задание 5.3.5: Обновление DataGrid после сохранения
                LoadFilms();
            }
        }

        /// <summary>
        /// Задание 5.1.3: Обработчик удаления фильмов
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранных записей из DataGrid
            var selectedFilms = dataGridFilms.SelectedItems.Cast<Models.Film>().ToList();

            if (selectedFilms.Count == 0)
            {
                MessageBox.Show("Выберите фильмы для удаления", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Задание 5.2.1: Подтверждение удаления
            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить {selectedFilms.Count} записей?",
                "Удаление",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var filmIds = selectedFilms.Select(f => f.FilmId).ToList();
                    _filmService.DeleteFilms(filmIds);

                    // Задание 5.2.2: Информация об успешном удалении
                    MessageBox.Show("Данные успешно удалены.", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadFilms(); // Обновление DataGrid после удаления
                }
                catch (System.Exception ex)
                {
                    // Задание 5.1.4 и 5.2.3: Перехват и отображение исключений
                    MessageBox.Show($"Не удалось удалить записи. Причина: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Задание 5.4.2: Обработчик двойного клика для редактирования
        /// </summary>
        private void DataGridFilms_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedFilm = dataGridFilms.SelectedItem as Models.Film;
            if (selectedFilm != null)
            {
                // Передача выбранного объекта в конструктор формы
                var filmWindow = new FilmWindow(selectedFilm);
                if (filmWindow.ShowDialog() == true)
                {
                    LoadFilms(); // Обновление DataGrid после сохранения
                }
            }
        }
    }
}