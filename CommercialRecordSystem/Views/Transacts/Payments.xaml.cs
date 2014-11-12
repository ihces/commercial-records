using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecordSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Payments : Page
    {
        public Payments()
        {
            this.InitializeComponent();
        }

        private void backButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void printButtonClicked(object sender, RoutedEventArgs e)
        {
            //await Windows.Graphics.Printing.PrintManager.ShowPrintUIAsync();
        }
    }
}
