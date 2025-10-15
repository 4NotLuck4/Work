namespace LabWork11.Filters
{
    /// <summary>
    /// Фильтр для поиска фильмов
    /// </summary>
    public class FilmFilter
    {
        public string Name { get; set; }
        public short? MinDuration { get; set; }
        public short? MaxDuration { get; set; }
        public string AgeRating { get; set; }
    }
}