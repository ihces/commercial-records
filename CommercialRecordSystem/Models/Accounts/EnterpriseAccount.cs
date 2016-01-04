using CommercialRecordSystem.Models.Accounts;

namespace CommercialRecordSystem.Models.Accounts
{
    class EnterpriseAccount : AccountBase
    {
        public string DepositType { get; set; }
        public string CurrencyType { get; set; }
        public string Iban { get; set; }
    }
}
