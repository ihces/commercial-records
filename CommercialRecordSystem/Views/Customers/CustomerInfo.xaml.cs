using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CommercialRecordSystem.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecordSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerInfo : Page
    {
        public CustomerInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
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
        }
    }
}
