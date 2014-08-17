using SQLite;

namespace TicariKayitSistemi.Models
{
    class Payment
    {
        enum Type {CASH, CREDIT_CARD, OTHER}
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int accountId { get; set; }
        public Type type { get; set; }
        public string detail { get; set; }
        public double cost { get; set; }
    }
}
