using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.Transacts
{
    public sealed partial class Sales : ViewBase
    {
        public Sales() :base(typeof(SaleFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
