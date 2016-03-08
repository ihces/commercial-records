using CommercialRecords.ViewModels.FrameVMs.Transacts;

namespace CommercialRecords.Views.TransactionReports
{
    public sealed partial class TransactionReportsView : ViewBase
    {
        public TransactionReportsView()
            : base(typeof(TransactListFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
