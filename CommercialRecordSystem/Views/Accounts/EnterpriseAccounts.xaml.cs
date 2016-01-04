using CommercialRecordSystem.ViewModels.FrameVMs;

namespace CommercialRecordSystem.Views.Accounts
{
    public sealed partial class EnterpriseAccounts : ViewBase
    {
        public EnterpriseAccounts()
            : base(typeof(EnterpriseAccountsFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
