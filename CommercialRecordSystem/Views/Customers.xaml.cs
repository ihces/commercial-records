using CommercialRecordSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecordSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Costumers : Page
    {
        public Costumers()
        {
            this.InitializeComponent();
            this.DataContext = new CustomersVM();
        }

        private void backButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
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
                int selectedCustomerId = dataContextBuff.SelectedCustomer.Id;
                this.Frame.Navigate(typeof(CustomerInfo), selectedCustomerId);
            }
        }

        private void CustomerListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if ( null != CustomerListView.SelectedItem)
            {
                CustomersVM dataContextBuff = (CustomersVM)this.DataContext;
                int selectedCustomerId = dataContextBuff.SelectedCustomer.Id;
                this.Frame.Navigate(typeof(CustomerAccount), selectedCustomerId);
            }
        }
    }
}
