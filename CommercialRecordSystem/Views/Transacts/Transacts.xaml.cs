using CommercialRecordSystem.ViewModels.FrameVMs.Transacts;

namespace CommercialRecordSystem.Views.Transacts
{
    public sealed partial class Transacts : ViewBase
    {
        public Transacts()
            : base(typeof(TransactsFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
