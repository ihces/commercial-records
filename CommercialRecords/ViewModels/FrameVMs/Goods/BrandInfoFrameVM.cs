using CommercialRecords.Common;
using CommercialRecords.Models.Goods;
using CommercialRecords.ViewModels.DataVMs.Goods;

namespace CommercialRecords.ViewModels.FrameVMs.Goods
{
    class BrandInfoFrameVM : InfoFrameVMBase<BrandVM, Brand>
    {
        public BrandInfoFrameVM(FrameNavigation navigation)
            : base(navigation, CrsDictionary.getInstance().lookup("infoPageTitles", "brand"), 1.25)
        {

        }
    }
}
