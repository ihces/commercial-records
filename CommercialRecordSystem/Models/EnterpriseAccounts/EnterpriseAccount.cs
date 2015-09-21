
namespace CommercialRecordSystem.Models.EnterpriseAccounts
{
    class EnterpriseAccount : InfoModelBase
    {
        public enum TYPE { CASH_REGISTER, BANK_ACCOUNT };
        public enum DEPOSIT_TYPE{TERM, DEMAND};

        public TYPE Type { get; set; }
        public string Name { get; set; }
        public DEPOSIT_TYPE DepositType { get; set; }
        public int CurrencyType { get; set; }
        public string Iban { get; set; }
        public string Detail { get; set; }
    }
}
