namespace CinemaDbLibrary
{
    public class Pagination
    {
        public int PageSize { get; set; } = 3;
        public int PageNumber { get; set; } = 1;

        public Pagination(int pageSize = 3, int pageNumber = 1)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
