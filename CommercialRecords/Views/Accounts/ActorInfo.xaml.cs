using CommercialRecords.Views;
using CommercialRecords.ViewModels;

namespace CommercialRecords.Views.Accounts
{
    public sealed partial class ActorInfo : ViewBase
    {
        public ActorInfo() : base(typeof(ActorInfoFrameVM))
        {
            InitializeComponent();
        }
    }
}
