using System;
using SQLite;

namespace TicariKayitSistemi.Models
{
    class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int    Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int sincerity { get; set; }
        public string Address { get; set; }
        public string phoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string profilePhotoFileName { get; set; }
        public DateTime lastTransactDate { get; set; }
        public double accountCost { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}
