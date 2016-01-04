using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.Accounts
{
    public sealed partial class ActorInfo : ViewBase
    {
        public ActorInfo() : base(typeof(ActorInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
