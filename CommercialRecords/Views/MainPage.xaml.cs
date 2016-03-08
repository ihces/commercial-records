using CommercialRecords.Common;
using CommercialRecords.Views.Accounts;
using CommercialRecords.Views.Goods;
using CommercialRecords.Views.TransactionReports;
using CommercialRecords.Views.Transacts;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
namespace CommercialRecords.Views
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            App.CRInitializer initializer = new App.CRInitializer();
            initializer.startInit();
        }

        private void CostumersGrid_tabbed(object sender, TappedRoutedEventArgs e)
        {
            GoTo<CurrentAccountList>();
        }

        private void GoodsGrid_tabbed(object sender, TappedRoutedEventArgs e)
        {
            GoTo<GoodsView>();
        }

        private void SalesGrid_tabbed(object sender, TappedRoutedEventArgs e)
        {
            GoTo<TransactList>();
            
        }

        private void GoTo<Destination>()
        {
            Type destinationPageType = typeof(Destination);
            FrameNavigation navigation = new FrameNavigation(destinationPageType);
            navigation.Back = new FrameNavigation(this.GetType());
            this.Frame.Navigate(destinationPageType, navigation);
        }

        private void EnterpriseAccounts_tapped(object sender, TappedRoutedEventArgs e)
        {
            GoTo<EnterpriseAccounts>();
        }

        private void TransactionReports_tapped(object sender, TappedRoutedEventArgs e)
        {
            GoTo<TransactionReportsView>();
        }

        private void Settings_tapped(object sender, TappedRoutedEventArgs e)
        {
            GoTo<Settings.Users>();
        }
    }
}
