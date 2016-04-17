using SQLite;

namespace CommercialRecords.Models
{
    public class ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
