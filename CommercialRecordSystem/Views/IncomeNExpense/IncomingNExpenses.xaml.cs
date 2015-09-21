using CommercialRecordSystem.ViewModels.FrameVMs.IncomeNExpenses;

namespace CommercialRecordSystem.Views
{
    public sealed partial class IncomingNExpenses : ViewBase
    {
        public IncomingNExpenses()
            : base(typeof(IncomeNExpenseFrameVM))
        {
            this.InitializeComponent();
        }
    }
}