using CommercialRecords.Models.Transacts;

namespace CommercialRecords.Models
{
    class PaymentEntry : TransactEntry
    {
        public int Type { get; set; }
    }
}
