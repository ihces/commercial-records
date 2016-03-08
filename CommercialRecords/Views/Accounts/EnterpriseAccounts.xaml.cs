using CommercialRecords.ViewModels.FrameVMs;

namespace CommercialRecords.Views.Accounts
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
