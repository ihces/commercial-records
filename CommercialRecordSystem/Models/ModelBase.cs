using SQLite;

namespace CommercialRecordSystem.Models
{
    class ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
