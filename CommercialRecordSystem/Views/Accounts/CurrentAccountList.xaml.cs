using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.Accounts
{
    public sealed partial class CurrentAccountList : ViewBase
    {
        public CurrentAccountList()
            : base(typeof(CurrentAccountListFrameVM)) 
        {
            this.InitializeComponent();
        }
    }
}
