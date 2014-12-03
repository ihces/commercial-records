using CommercialRecordSystem.Models.Transacts;

namespace CommercialRecordSystem.Models
{
    class PaymentEntry : TransactEntry
    {
        public enum TYPE {CASH, CREDIT_CARD, OTHER}
        public TYPE Type { get; set; }
    }
}
