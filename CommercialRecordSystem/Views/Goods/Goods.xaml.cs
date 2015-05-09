using CommercialRecordSystem.ViewModels.FrameVMs.Goods;
using CommercialRecordSystem.Views;

namespace CommercialRecordSystem
{
    public sealed partial class Goods : ViewBase
    {
        public Goods() : base(typeof(GoodsFrameVM))
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
