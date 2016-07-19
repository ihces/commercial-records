using CommercialRecords.ViewModels.FrameVMs.Settings;

namespace CommercialRecords.Views.Settings
{
    public sealed partial class UserInfo : ViewBase
    {
        public UserInfo()
            : base(typeof(UserInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
