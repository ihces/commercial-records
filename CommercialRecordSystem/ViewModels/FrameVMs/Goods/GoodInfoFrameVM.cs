using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class GoodInfoFrameVM : InfoFrameVMBase<GoodVM, Good>
    {
        public GoodInfoFrameVM(FrameNavigation navigation)
            : base(navigation, "Ürün", 1.0)
        {

        }
    }
}
