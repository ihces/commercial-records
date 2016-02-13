using CommercialRecordSystem.ViewModels.FrameVMs.Dashboard;
using CommercialRecordSystem.ViewModels.FrameVMs.Dashboard;
using System;
using System.Collections.Generic;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecordSystem.Views.Dashboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardView : ViewBase
    {
        public DashboardView()
            : base(typeof(DashboardFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
