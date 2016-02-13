using CommercialRecordSystem.Common;
using CommercialRecordSystem.Views;
using CommercialRecordSystem.Views.Accounts;
using CommercialRecordSystem.Views.Goods;
using CommercialRecordSystem.Views.Settings;
using CommercialRecordSystem.Views.Transacts;
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
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
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

        private void DashboardsGrid_tabbed(object sender, TappedRoutedEventArgs e)
        {
           // GoTo<DashboardView>();
        }

        private void EnterpriseAccounts_tapped(object sender, TappedRoutedEventArgs e)
        {
            GoTo<EnterpriseAccounts>();
        }

        private void IncomeNExpenses_tapped(object sender, TappedRoutedEventArgs e)
        {
            GoTo<IncomingNExpenses>();
        }

        private void Settings_tapped(object sender, TappedRoutedEventArgs e)
        {
            GoTo<Settings>();
        }
    }
}
