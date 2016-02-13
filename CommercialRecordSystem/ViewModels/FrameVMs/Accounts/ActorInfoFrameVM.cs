using System;
using System.Linq;
using System.Windows.Input;
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using System.IO;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels.FrameVMs;
using CommercialRecordSystem.ViewModels.DataVMs.Accounts;
using CommercialRecordSystem.Models.Accounts;

namespace CommercialRecordSystem.ViewModels
{
    class ActorInfoFrameVM : InfoFrameVMBase<ActorVM, Actor>
    {
        public ActorInfoFrameVM(FrameNavigation navigation)
            : base(navigation, CrsDictionary.getInstance().lookup("infoPageTitles", "person_firm"), 1.25)
        {
            
        }

        protected override void saveInfoCmdHandler(object parameter)
        {
            bool newActor = !CurrentInfo.Recorded;
            base.saveInfoCmdHandler(parameter);

            if (newActor && CurrentInfo.Recorded)
            {
                CurrentAccountVM defaultAccount = new CurrentAccountVM();
                defaultAccount.ActorId = CurrentInfo.Id;
                defaultAccount.Type = CurrentAccount.TYPE_DEBT;
                defaultAccount.Name = CrsDictionary.getInstance().lookup("instanceLabels", "currentAccount1");
                defaultAccount.save();
            }
        }
    }
}
