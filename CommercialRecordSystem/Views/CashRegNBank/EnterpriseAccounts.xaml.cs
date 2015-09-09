using CommercialRecordSystem.ViewModels.FrameVMs.Transacts;

namespace CommercialRecordSystem.Views.CashRegNBank
{
    public sealed partial class EnterpriseAccounts : ViewBase
    {
        public EnterpriseAccounts()
            : base(typeof(TransactsFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
