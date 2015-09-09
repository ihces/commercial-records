using CommercialRecordSystem.ViewModels.FrameVMs.Transacts;

namespace CommercialRecordSystem.Views
{
    public sealed partial class IncomingNExpenses : ViewBase
    {
        public IncomingNExpenses()
            : base(typeof(TransactsFrameVM))
        {
            this.InitializeComponent();
        }
    }
}