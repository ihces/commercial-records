using System;
using SQLite;

namespace CommercialRecordSystem.Models
{
    class SaleEntry : ModelBase
    {
        public int TransactId { get; set; }
        public double Amount{ get; set; }
        public int Measure{ get; set; }
        public string Detail{ get; set; }
        public double UnitCost { get; set; }
        public double Cost { get; set; }
    }
}
