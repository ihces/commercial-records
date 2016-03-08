using CommercialRecords.ViewModels.FrameVMs.Goods;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecords.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrandInfo : ViewBase
    {
        public BrandInfo()
            : base(typeof(BrandInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
