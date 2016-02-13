using System;

namespace CommercialRecordSystem.Models.Goods
{
    class Brand : InfoModelBase
    {
        public string Name { get; set; }
        public string AuthorizedReseller { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
