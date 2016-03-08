namespace CommercialRecords.Models.Settings
{
    class User : ModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int Role { get; set; }
        public int CashRegisterId { get; set; }
        public string Password { get; set; }
        public string ImageFileName { get; set; }
    }
}
