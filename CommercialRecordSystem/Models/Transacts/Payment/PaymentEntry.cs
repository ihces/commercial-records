namespace CommercialRecordSystem.Models
{
    class PaymentEntry : ModelBase
    {
        public enum TYPE {CASH, CREDIT_CARD, OTHER}
        public int TransactId { get; set; }
        public TYPE Type { get; set; }
        public string Detail { get; set; }
        public double Cost { get; set; }
    }
}
