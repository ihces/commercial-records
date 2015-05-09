using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.Customers
{
    public sealed partial class CustomerList : ViewBase
    {
        public CustomerList() : base(typeof(CustomerListFrameVM)) 
        {
            this.InitializeComponent();
        }
    }
}
