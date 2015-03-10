using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.Customers
{
    public sealed partial class CustomerInfo : ViewBase
    {
        public CustomerInfo() : base(typeof(CustomerInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
