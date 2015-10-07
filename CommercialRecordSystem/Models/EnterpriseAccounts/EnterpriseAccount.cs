
namespace CommercialRecordSystem.Models.EnterpriseAccounts
{
    class EnterpriseAccount : InfoModelBase
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string DepositType { get; set; }
        public string CurrencyType { get; set; }
        public string Iban { get; set; }
        public string Detail { get; set; }
    }
}
