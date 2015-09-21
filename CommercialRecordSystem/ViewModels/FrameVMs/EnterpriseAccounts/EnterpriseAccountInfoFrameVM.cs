using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.EnterpriseAccounts;
using CommercialRecordSystem.ViewModels.DataVMs.EnterpriseAccounts;

namespace CommercialRecordSystem.ViewModels.FrameVMs.EnterpriseAccounts
{
    class EnterpriseAccountInfoFrameVM : InfoFrameVMBase<EnterpriseAccountVM, EnterpriseAccount>
    {
        public EnterpriseAccountInfoFrameVM(FrameNavigation navigation)
            : base(navigation, "İşletme Hesabı", 1)
        {

        }
    }
}
