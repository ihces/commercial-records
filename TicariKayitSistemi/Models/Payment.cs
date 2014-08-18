using SQLite;

namespace TicariKayitSistemi.Models
{
    class Payment
    {
        public enum PaymentType {CASH, CREDIT_CARD, OTHER}
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public PaymentType Type { get; set; }
        public string Detail { get; set; }
        public double Cost { get; set; }
    }
}
