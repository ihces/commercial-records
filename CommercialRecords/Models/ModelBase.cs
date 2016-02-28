using SQLite;

namespace CommercialRecords.Models
{
    class ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
