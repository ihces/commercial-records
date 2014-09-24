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
    public sealed partial class Customers : Page
    {
        public enum OPEN_PURPOSE { BASE, ADD_TRANSACTION }
        private OPEN_PURPOSE openPurpose = OPEN_PURPOSE.BASE;

        public Customers()
        {
            this.InitializeComponent();
            this.DataContext = new CustomersVM(this.Frame);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (null != e.Parameter)
            {
                openPurpose = (OPEN_PURPOSE) e.Parameter;
            }
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
                this.Frame.Navigate(typeof(CustomerInfo), dataContextBuff.SelectedCustomer.Id);
            }
        }

        private void CustomerListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if ( null != CustomerListView.SelectedItem)
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
    }
}
