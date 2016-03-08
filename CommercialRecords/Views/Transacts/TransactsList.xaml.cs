using CommercialRecords.ViewModels.FrameVMs.Transacts;

namespace CommercialRecords.Views.Transacts
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
