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
        /*
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (null != e.Parameter)
            {
                this.DataContext = new SaleFrameVM(this.Frame, (TransactVM)e.Parameter);
            }
        }*/
    }
}
