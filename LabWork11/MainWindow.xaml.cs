using LabWork11.Contexts;
using LabWork11.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace LabWork11
{
    public partial class MainWindow : Window
    {
        private readonly FilmService _filmService;

        private ObservableCollection<Models.Film> _films;

        public MainWindow()
        {
            InitializeComponent();

            var context = new AppDbContext();
            context.Database.EnsureCreated();
            _filmService = new FilmService(context);

            LoadFilms();
        }

        private void LoadFilms()
        {
            _films = new ObservableCollection<Models.Film>((IEnumerable<Models.Film>)_filmService.GetFilms());
            FilmsDataGrid.ItemsSource = _films;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedFilms = FilmsDataGrid.SelectedItems.Cast<Models.Film>().ToList();
            if (selectedFilms.Count == 0)
            {
                MessageBox.Show("Выберите фильмы для удаления", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                _filmService.DeleteFilms(selectedFilms);

                LoadFilms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}