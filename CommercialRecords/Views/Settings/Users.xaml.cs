using CommercialRecords.ViewModels.FrameVMs.Settings;

namespace CommercialRecords.Views.Settings
{
    public sealed partial class Users : ViewBase
    {
        public Users()
            : base(typeof(UsersFrameVM))
        {
            this.InitializeComponent();
        }
    }
}

