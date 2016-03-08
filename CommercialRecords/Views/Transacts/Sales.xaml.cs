using CommercialRecords.Views;
using CommercialRecords.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace CommercialRecords.Views.Transacts
{
    public sealed partial class Sales : ViewBase
    {
        public Sales() :base(typeof(SaleFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
