using CommercialRecordSystem.Models.Transacts;

namespace CommercialRecordSystem.Models
{
    class PaymentEntry : TransactEntry
    {
        public int Type { get; set; }
    }
}
