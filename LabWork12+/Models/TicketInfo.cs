using System;

public class TicketInfo
{
    public int TicketId { get; set; }
    public string FilmName { get; set; }
    public DateTime SessionDate { get; set; }
    public byte Row { get; set; }
    public byte Seat { get; set; }
    public decimal Price { get; set; }
}