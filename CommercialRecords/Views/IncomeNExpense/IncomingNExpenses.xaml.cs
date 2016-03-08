using CommercialRecords.ViewModels;
using CommercialRecords.ViewModels.FrameVMs.IncomeNExpenses;

namespace CommercialRecords.Views
{
    public sealed partial class IncomingNExpenses : ViewBase
    {
        public IncomingNExpenses()
            : base(typeof(ActorInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}