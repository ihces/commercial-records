

namespace CommercialRecordSystem.Models.Goods
{
    class Category : InfoModelBase
    {
        public int ParentId { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }
    }
}
