using System;

namespace CommercialRecords.Models
{
    class Actor : InfoModelBase
    {
        public const int TYPE_PERSON = 0, TYPE_FIRM = 1;
        public int Type { get; set; }
        public bool Registered { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Sincerity { get; set; }
        public string Address { get; set; }
        public string Detail { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public DateTime LastTransactDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public double ReceivableAccTotal{ get; set; }
        public double ReceivableAccPaid { get; set; }
        public double DebtAcctTotal { get; set; }
        public double DebtAcctPaid { get; set; }
        public int TotalAccount { get; set; }
        public int ActiveAccNum { get; set; }
    }
}
