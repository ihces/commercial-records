using SQLite;
using System;

namespace CommercialRecordSystem.Models
{
    class Transact
    {
        public enum TYPE {SALE, PAYMENT, ORDER}

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public TYPE Type { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
    }
}
