using CommercialRecords.Common;
using CommercialRecords.Models;
using CommercialRecords.ViewModels.FrameVMs;
using CommercialRecords.ViewModels.DataVMs.Accounts;
using CommercialRecords.Models.Accounts;

namespace CommercialRecords.ViewModels
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
