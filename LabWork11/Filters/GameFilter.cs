namespace LabWork11.Filters
{
    public class GameFilter
    {
        public string Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
    }
}