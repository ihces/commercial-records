using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
using CommercialRecordSystem.ViewModels.DataVMs.Goods;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class BrandInfoFrameVM : InfoFrameVMBase<BrandVM, Firm>
    {
        public BrandInfoFrameVM(FrameNavigation navigation)
            : base(navigation, "Marka", 1.25)
        {

        }
    }
}
