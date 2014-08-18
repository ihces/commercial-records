using System;
using SQLite;

namespace CommercialRecordSystem.Models
{
    class CurrentAccount
    {
        public enum AccountType{SALE, PAYMENT, PRE_ORDER};
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CustumerId { get; set; }
        public DateTime TransactDate { get; set; }
        public AccountType Type { get; set; }
        public double Amount { get; set; }
    }
}
