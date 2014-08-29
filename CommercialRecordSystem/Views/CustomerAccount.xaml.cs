using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecordSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerAccount : Page
    {
        public CustomerAccount()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        public class FinancialStuff
        {
            public string Name { get; set; }
            public int Amount { get; set; }
        }
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }
        private void backButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void LoadChartContents()
        {
            Random rand = new Random();
            List<FinancialStuff> financialStuffList = new List<FinancialStuff>();
            financialStuffList.Add(new FinancialStuff() { Name = "Ağustos", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Eylül", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Ekim", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Kasım", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Aralık", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Ocak", Amount = rand.Next(0, 200) });
            (ColumnChart.Series[0] as ColumnSeries).ItemsSource = financialStuffList;
            (ColumnChart.Series[1] as ColumnSeries).ItemsSource = financialStuffList;
            (ColumnChart.Series[2] as LineSeries).ItemsSource = financialStuffList;
        }

        private void actionList_Tabbed(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Sales));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int customerId = (int)e.Parameter;
        }
    }
}
