using SQLite;

namespace TicariKayitSistemi.Models
{
    class Payment
    {
        public enum PaymentType {CASH, CREDIT_CARD, OTHER}
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int accountId { get; set; }
        public PaymentType type { get; set; }
        public string detail { get; set; }
        public double cost { get; set; }
    }
}
