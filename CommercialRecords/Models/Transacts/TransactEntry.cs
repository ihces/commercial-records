using System;
namespace CommercialRecords.Models.Transacts
{
    class TransactEntry : ModelBase
    {
        public int TransactId { get; set; }
        public DateTime Date { get; set; }
        public string Detail { get; set; }
        public double Cost { get; set; }
    }
}
