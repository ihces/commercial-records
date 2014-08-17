using System;
using SQLite;

namespace TicariKayitSistemi.Models
{
    class CurrentAccount
    {
        public enum AccountType{SALE, PAYMENT, PRE_ORDER};
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int custumerId { get; set; }
        public DateTime transactDate { get; set; }
        public AccountType type { get; set; }
        public double amount { get; set; }
    }
}
