using System;
namespace CommercialRecords.Models.Accounts.EnterpriseAccounts
{
    class EnterpriseAccTransact : ModelBase
    {
        public int Type { get; set; }
        public int TransactId { get; set; }
        public int TransactEntryId { get; set; }
        public int AccountId { get; set; }
        public string Detail { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
