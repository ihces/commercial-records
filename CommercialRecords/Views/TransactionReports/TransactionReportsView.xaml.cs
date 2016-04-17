using CommercialRecords.ViewModels.FrameVMs;

namespace CommercialRecords.Views.TransactionReports
{
    public sealed partial class TransactionReportsView : ViewBase
    {
        public TransactionReportsView()
            : base(typeof(TransactionReportsFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
