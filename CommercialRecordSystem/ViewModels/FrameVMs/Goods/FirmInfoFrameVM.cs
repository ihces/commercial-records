using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
using CommercialRecordSystem.ViewModels.DataVMs.Goods;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class FirmInfoFrameVM : InfoFrameVMBase<FirmVM, Firm>
    {
        public FirmInfoFrameVM(FrameNavigation navigation)
            : base(navigation, "Firma", 1.25)
        {

        }
    }
}
