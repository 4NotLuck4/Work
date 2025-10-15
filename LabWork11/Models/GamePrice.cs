using System;

namespace LabWork11.Models
{
    public class GamePrice
    {
        public int GameId { get; set; }
        public decimal OldPrice { get; set; }
        public DateTime ChangingDate { get; set; }
    }
}