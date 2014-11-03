using CommercialRecordSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        

        private void StartTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            TransactTypeVM dataContextBuff = (TransactTypeVM)this.DataContext;
            this.Frame.Navigate(typeof(Sales), dataContextBuff);
        }
    }
}
