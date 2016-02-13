using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
using CommercialRecordSystem.ViewModels.DataVMs.Goods;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class BrandInfoFrameVM : InfoFrameVMBase<BrandVM, Brand>
    {
        public BrandInfoFrameVM(FrameNavigation navigation)
            : base(navigation, CrsDictionary.getInstance().lookup("infoPageTitles", "brand"), 1.25)
        {

        }
    }
}
