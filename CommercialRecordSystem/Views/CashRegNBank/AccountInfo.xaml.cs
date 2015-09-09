using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.CashRegNBank
{

    public sealed partial class AccountInfo : ViewBase
    {
        public AccountInfo()
            : base(typeof(CustomerInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
