using CommercialRecords.Common;
using CommercialRecords.Models;
using CommercialRecords.ViewModels.FrameVMs;
using CommercialRecords.ViewModels.DataVMs.Accounts;
using CommercialRecords.Models.Accounts;

namespace CommercialRecords.ViewModels
{
    class ActorInfoFrameVM : InfoFrameVMBase<ActorVM, Actor>
    {
        
        private int mainAccountType = 0;
        public int MainAccountType
        {
            get
            {
                return mainAccountType;
            }
            set
            {
                mainAccountType = value;
                RaisePropertyChanged("MainAccountType");
            }
        }

        private double initialAmount = 0;
        public double InitialAmount
        {
            get
            {
                return initialAmount;
            }
            set
            {
                initialAmount = value;
                RaisePropertyChanged("InitialAmount");
            }
        }

        public ActorInfoFrameVM(FrameNavigation navigation)
            : base(navigation, CrsDictionary.getInstance().lookup("infoPageTitles", "person_firm"), 1.25)
        {
            
        }

        protected override void saveInfoCmdHandler(object parameter)
        {
            bool newActor = !CurrentInfo.Recorded;
            base.saveInfoCmdHandler(parameter);
            
            /*if (newActor && CurrentInfo.Recorded)
            {
                CurrentAccountVM defaultAccount = new CurrentAccountVM();
                defaultAccount.ActorId = CurrentInfo.Id;
                defaultAccount.Type = MainAccountType;
                defaultAccount.Name = CrsDictionary.getInstance().lookup("instanceLabels", "mainAccount");

                if ((0 == MainAccountType && InitialAmount > 0) || (1 == MainAccountType && InitialAmount < 0))
                    defaultAccount.TotalDebt = InitialAmount;

                if ((0 == MainAccountType && InitialAmount > 0) || (0 == MainAccountType && InitialAmount < 0))
                    defaultAccount.TotalCredit = InitialAmount;

                defaultAccount.save();
            }*/
        }
    }
}
