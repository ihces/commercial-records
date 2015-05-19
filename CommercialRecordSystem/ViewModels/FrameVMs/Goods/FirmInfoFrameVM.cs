using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;

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
