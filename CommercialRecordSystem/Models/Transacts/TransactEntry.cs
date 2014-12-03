namespace CommercialRecordSystem.Models.Transacts
{
    class TransactEntry : ModelBase
    {
        public int TransactId { get; set; }
        public string Detail { get; set; }
        public double Cost { get; set; }
    }
}
