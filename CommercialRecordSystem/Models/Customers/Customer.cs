﻿using System;

namespace CommercialRecordSystem.Models
{
    class Customer : InfoModelBase
    {
        public enum TYPE { REGISTERED, UNREGISTERED };

        public TYPE Type { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Sincerity { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public DateTime LastTransactDate { get; set; }
        public double AccountCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
