using System;

namespace CommercialRecordSystem.Models
{
    class Transact : ModelBase
    {
        public const int 
            TYPE_SALE = 0,
            TYPE_ORDER = 1,
            TYPE_PAYMENT = 2;

        public int Type { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
        public double Paid { get; set; }
        public int EntryCount { get; set; }
    }
}
