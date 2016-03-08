using CommercialRecords.Views;
using CommercialRecords.ViewModels;
using Windows.UI.Xaml;

namespace CommercialRecords.Views.Accounts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrentAccountView : ViewBase
    {
        public CurrentAccountView()
            : base(typeof(CurrentAccountFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
