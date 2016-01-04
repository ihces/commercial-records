using CommercialRecordSystem.Models.Transacts;
using System;

namespace CommercialRecordSystem.Models
{
    class SaleEntry : TransactEntry
    {
        public DateTime ModifyDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int OrderState{get; set;}
        public string MoreDetail { get; set; }
        public double Amount{ get; set; }
        public string Measure{ get; set; }
        public int GoodId { get; set; }
        public double UnitCost { get; set; }
        public double Cost { get; set; }
    }
}
