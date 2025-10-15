namespace LabWork11.Models
{
    public class Hall
    {
        public byte HallId { get; set; }
        public string Cinema { get; set; }
        public byte HallNumber { get; set; }
        public byte? RowsCount { get; set; }
        public byte? SeatsCount { get; set; }
        public bool? IsVip { get; set; }
    }
}