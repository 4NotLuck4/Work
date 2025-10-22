namespace LabWork12.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public short Duration { get; set; }
        public short ReleaseDate { get; set; }
        public string Description { get; set; }
        public byte[] Poster { get; set; }
        public string AgeRating { get; set; }
        public DateTime? StartRental { get; set; }
        public DateTime? FinishRental { get; set; }
        public bool? IsDeleted { get; set; }
        public List<Session> Sessions { get; set; } = new();
    }
}