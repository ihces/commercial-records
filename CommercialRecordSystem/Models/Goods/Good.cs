using System;

namespace CommercialRecordSystem.Models.Goods
{
    class Good : InfoModelBase
    {
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int StockAmount { get; set; }
        public string Unit { get; set; }
        public string Detail { get; set; }
        public double Vat { get; set; }
        public double Price { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
