using CommercialRecordSystem.Models.Accounts;

namespace CommercialRecordSystem.Models.Accounts
{
    class EnterpriseAccount : AccountBase
    {
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public double Balance { get; set; }
    }
}
