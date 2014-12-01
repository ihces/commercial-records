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

        /*protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CustomerInfoFrameVM VModel = null;
            if (null == e.Parameter)
            {
                VModel = new CustomerInfoFrameVM(this.Frame);
            }  
            else
            {
                int customerId = (int)e.Parameter;
                VModel = new CustomerInfoFrameVM(this.Frame, customerId);
            }
            this.DataContext = VModel;
        }*/
    }
}
