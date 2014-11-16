using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CommercialRecordSystem.ViewModels;
using System.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecordSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Customers : Page
    {
        public enum OPEN_PURPOSE { BASE, ADD_TRANSACTION }
        private OPEN_PURPOSE openPurpose = OPEN_PURPOSE.BASE;

        public Customers()
        {

            this.InitializeComponent();
            setTotalCustomerCount();
            setTotalCustomerBalance();
        }

        private void setTotalCustomerCount()
        {
            int totalCount = CustomerListView != null ? CustomerListView.Items.Count : 0;
            DisplayTextWhenListEmpty.Text = totalCount == 0 ? "Kayıtlı Müşteri Bulunamadı" : "";
            totalCustomerCount.Text = totalCount.ToString() + " Kişi";

        }
        private void setTotalCustomerBalance()
        {

            int totalCount = CustomerListView != null ? CustomerListView.Items.Count : 0;
            double balance = 0;

            IEnumerable items = this.CustomerListView.Items;
            foreach (CustomerVM customer in items)
            {
                balance = balance + customer.AccountCost;
            }

            totalCustomerBalance.Text = balance.ToString() + "₺";

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (null != e.Parameter)
            {
                openPurpose = (OPEN_PURPOSE)e.Parameter;
            }

            this.DataContext = new CustomersVM(this.Frame);
        }

        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerInfo));
        }

        private void editCustButton_Click(object sender, RoutedEventArgs e)
        {
            if (null != CustomerListView.SelectedItem)
            {
                CustomersVM dataContextBuff = (CustomersVM)this.DataContext;
                this.Frame.Navigate(typeof(CustomerInfo), dataContextBuff.SelectedCustomer.Id);
            }
        }

        private void CustomerListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (null != CustomerListView.SelectedItem)
            {
                CustomersVM dataContextBuff = (CustomersVM)this.DataContext;

                switch (openPurpose)
                {
                    case OPEN_PURPOSE.BASE:
                        this.Frame.Navigate(typeof(CustomerAccount), dataContextBuff.SelectedCustomer.Id);
                        break;
                    case OPEN_PURPOSE.ADD_TRANSACTION:
                        this.Frame.Navigate(typeof(TransactTypeSelector), dataContextBuff.SelectedCustomer.Id);
                        break;
                }
            }
        }

        private void CustomerListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            setTotalCustomerCount();
            setTotalCustomerBalance();
        }


    }
}
