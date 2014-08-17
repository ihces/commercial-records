using System;
using SQLite;

namespace TicariKayitSistemi.Models
{
    class Firm
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public string authorizedReseller { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string pictureFileName { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}
