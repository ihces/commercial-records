using SQLite;

namespace TicariKayitSistemi.Models
{
    class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int accountId { get; set; }
        public int quantity { get; set; }
        public int unitType { get; set; }
        public string detail { get; set; }
        public double unitCost { get; set; }
    }
}
