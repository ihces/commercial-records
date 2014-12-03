using CommercialRecordSystem.Models.Transacts;

namespace CommercialRecordSystem.Models
{
    class SaleEntry : TransactEntry
    {
        public double Amount{ get; set; }
        public int Measure{ get; set; }
        public double UnitCost { get; set; }
        public double Cost { get; set; }
    }
}
