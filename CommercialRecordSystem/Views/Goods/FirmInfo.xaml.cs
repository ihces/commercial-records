﻿using CommercialRecordSystem.ViewModels.FrameVMs.Goods;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecordSystem.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FirmInfo : ViewBase
    {
        public FirmInfo()
            : base(typeof(FirmInfoFrameVM))
        {
            this.InitializeComponent();
        }
    }
}
