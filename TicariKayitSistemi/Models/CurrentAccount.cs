using System;
using SQLite;

namespace TicariKayitSistemi.Models
{
    class CurrentAccount
    {
        enum types{SALE, PAYMENT, PRE_ORDER};
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int custumerId { get; set; }
        public DateTime transactDate { get; set; }
        public types type { get; set; }
        public double amount { get; set; }
    }
}
