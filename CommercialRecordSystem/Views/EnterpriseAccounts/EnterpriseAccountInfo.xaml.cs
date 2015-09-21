using CommercialRecordSystem.ViewModels.FrameVMs.EnterpriseAccounts;

namespace CommercialRecordSystem.Views.EnterpriseAccounts
{
    public sealed partial class EnterpriseAccountInfo : ViewBase
    {
        public EnterpriseAccountInfo()
            : base(typeof(EnterpriseAccountInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
