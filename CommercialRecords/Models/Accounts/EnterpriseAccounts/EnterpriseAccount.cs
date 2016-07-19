using CommercialRecords.Models.Accounts;

namespace CommercialRecords.Models.Accounts
{
    public class EnterpriseAccount : AccountBase
    {
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public double Balance { get; set; }
    }
}
