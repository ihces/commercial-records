﻿using System;

namespace CommercialRecordSystem.Models
{
    class Good : InfoModelBase
    {
        public string Name { get; set; }
        public int StockAmount { get; set; }
        public int Unit { get; set; }
        public string Detail { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
