using CommercialRecords.Views;
using CommercialRecords.ViewModels;

namespace CommercialRecords.Views.Accounts
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
