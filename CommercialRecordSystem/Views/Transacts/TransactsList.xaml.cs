using CommercialRecordSystem.ViewModels.FrameVMs.Transacts;

namespace CommercialRecordSystem.Views.Transacts
{
    public sealed partial class TransactList : ViewBase
    {
        public TransactList()
            : base(typeof(TransactListFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
