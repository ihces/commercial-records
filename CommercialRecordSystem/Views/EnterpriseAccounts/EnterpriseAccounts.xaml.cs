using CommercialRecordSystem.ViewModels.FrameVMs.CashRegNBank;

namespace CommercialRecordSystem.Views.CashRegNBank
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
