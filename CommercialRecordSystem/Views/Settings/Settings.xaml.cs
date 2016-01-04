using CommercialRecordSystem.ViewModels;
namespace CommercialRecordSystem.Views.Settings
{
    public sealed partial class Settings : ViewBase
    {
        public Settings()
            : base(typeof(ActorInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
