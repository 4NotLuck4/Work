namespace LabWork12.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int? FilmId { get; set; }
        public byte? HallId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? Is3d { get; set; }
        public List<Ticket> Tickets { get; set; } = new();

    }
}