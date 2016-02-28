using CommercialRecords.ViewModels;
namespace CommercialRecords.Views.Settings
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
