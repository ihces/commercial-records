namespace CommercialRecords.Models.Settings
{
    public class User : InfoModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public int CashRegisterId { get; set; }
        public string Password { get; set; }
        public string Detail { get; set; }
    }
}