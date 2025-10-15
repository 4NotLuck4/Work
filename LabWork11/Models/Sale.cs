using System;

namespace LabWork11.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int GameId { get; set; }
        public short KeysAmount { get; set; }
        public DateTime SaleDate { get; set; }
    }
}