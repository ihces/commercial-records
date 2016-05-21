using System;

namespace CommercialRecords.Models
{
    class TransactReport : ModelBase
    {
        public string TransType { get; set; }
        public int OperatorId { get; set; }
        public DateTime Date { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
    }
}
