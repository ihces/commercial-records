using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.Transacts
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
