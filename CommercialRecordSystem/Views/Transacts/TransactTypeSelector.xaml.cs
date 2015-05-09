using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.Transacts
{
    public sealed partial class TransactTypeSelector : ViewBase
    {
        public TransactTypeSelector() : base(typeof(TransactTypeFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
