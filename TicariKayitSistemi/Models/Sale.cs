using SQLite;

namespace CommercialRecordSystem.Models
{
    class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public int UnitType { get; set; }
        public string Detail { get; set; }
        public double UnitCost { get; set; }
    }
}
