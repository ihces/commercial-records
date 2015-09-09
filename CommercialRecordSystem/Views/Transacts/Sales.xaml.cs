using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace CommercialRecordSystem.Views.Transacts
{
    public sealed partial class Sales : ViewBase
    {
        public Sales() :base(typeof(SaleFrameVM))
        {
            this.InitializeComponent();
            test();
        }

        public async Task test() {
           // await this.popupTest.ShowAsync();
        }
    }
}
