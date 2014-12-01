using SQLite;
using System;

namespace CommercialRecordSystem.Models
{
    class Transact : ModelBase
    {
        public enum TYPE {SALE, PAYMENT, ORDER}
        public int ParentId { get; set; }
        public TYPE Type { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
    }
}
