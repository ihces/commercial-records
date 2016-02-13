using System;

namespace CommercialRecordSystem.Models
{
    class Transact : ModelBase
    {
        public const int 
            TYPE_SALE = 0,
            TYPE_ORDER = 1,
            TYPE_PURCHASE = 2,
            TYPE_PAYMENT = 3;

        public int Type { get; set; }
        public int ActorId { get; set; }
        public int AccountId { get; set; }
        public int EnterpriseAccId { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
        public double Paid { get; set; }
        public int EntryCount { get; set; }
    }
}
