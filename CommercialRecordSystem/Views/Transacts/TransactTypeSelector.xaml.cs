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
    public sealed partial class TransactTypeSelector : Page
    {
        public TransactTypeSelector()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (null != e.Parameter)
            {
                int selectedCustomerId = (int)e.Parameter;
                this.DataContext = new TransactTypeVM(this.Frame, selectedCustomerId);
            }
            else
            {
                this.DataContext = new TransactTypeVM(this.Frame);
            }
        }
        
        private void StartTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            TransactTypeVM dataContextBuff = (TransactTypeVM)this.DataContext;
            this.Frame.Navigate(typeof(Sales), dataContextBuff);
        }
    }
}
