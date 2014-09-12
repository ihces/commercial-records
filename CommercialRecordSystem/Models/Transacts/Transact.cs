using SQLite;
using System;

namespace CommercialRecordSystem.Models
{
    class Transact
    {
        public enum TRANSACT_TYPE {SALE, PAYMENT, PRE_ORDER}

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public TRANSACT_TYPE Type { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
    }
}
