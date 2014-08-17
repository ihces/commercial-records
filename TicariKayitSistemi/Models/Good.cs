using System;
using SQLite;

namespace TicariKayitSistemi.Models
{
    class Good
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public int stockAmount { get; set; }
        public int unit { get; set; }
        public string detail { get; set; }
        public double cost { get; set; }
        public double price { get; set; }
        public string pictureFileName { get; set; }
        public string barcode { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}
