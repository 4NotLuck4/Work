namespace CinemaDbLibrary.Services
{
    public class Pagination
    {
        public int PageSize { get; set; } = 3;
        public int PageNumber { get; set; } = 1;

        public int Skip => (PageNumber - 1) * PageSize;
    }
}