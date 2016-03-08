using CommercialRecords.Views;
using CommercialRecords.ViewModels.FrameVMs.Goods;

namespace CommercialRecords.Views.Goods
{
    public sealed partial class GoodsView : ViewBase
    {
        public GoodsView()
            : base(typeof(GoodsFrameVM))
        {
            this.InitializeComponent();
        }

        /*private void FirmGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.FirmInfo));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.GoodInfo));
        }*/
    }
}
