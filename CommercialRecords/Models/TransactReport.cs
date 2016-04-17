using System;

namespace CommercialRecords.Models
{
    class TransactReport : ModelBase
    {
        public int Type { get; set; }
        public int OperatorId { get; set; }
        public int TransactId { get; set; }
        public DateTime Date { get; set; }
        public string Detail { get; set; }
        public double Cost { get; set; }
    }
}
