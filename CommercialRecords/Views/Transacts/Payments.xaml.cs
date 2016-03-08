using CommercialRecords.Views;
using CommercialRecords.ViewModels;

namespace CommercialRecords.Views.Transacts
{
    public sealed partial class Payments : ViewBase
    {
        public Payments()
            : base(typeof(PaymentFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
