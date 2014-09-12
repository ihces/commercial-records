using System;
using SQLite;

namespace CommercialRecordSystem.Models
{
    class SaleEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id{ get; set; }
        public int CustomerId { get; set; }
        public int TransactId { get; set; }
        public DateTime Date{ get; set; }
        public double Amount{ get; set; }
        public int Measure{ get; set; }
        public string Detail{ get; set; }
        public double UnitCost { get; set; }
        public double Cost { get; set; }
    }
}
