using CommercialRecords.Common;
using CommercialRecords.Models.Accounts;
using CommercialRecords.Models.Settings;
using CommercialRecords.ViewModels.DataVMs.Accounts.EnterpriseAccounts;
using CommercialRecords.ViewModels.DataVMs.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommercialRecords.ViewModels.FrameVMs.Settings
{
    class UserInfoFrameVM : InfoFrameVMBase<UserVM, User>
    {
        private ObservableCollection<EnterpriseAccountVM> cashRegisters;
        public ObservableCollection<EnterpriseAccountVM> CashRegisters
        {
            get
            {
                return cashRegisters;
            }
            set
            {
                cashRegisters = value;
                RaisePropertyChanged("CashRegisters");
            }
        }

        private EnterpriseAccountVM selectedCashRegister;
        public EnterpriseAccountVM SelectedCashRegister
        {
            get
            {
                return selectedCashRegister;
            }
            set
            {
                selectedCashRegister = value;
                if (null != selectedCashRegister)
                    CurrentInfo.CashRegisterId = selectedCashRegister.Id;

                RaisePropertyChanged("SelectedCashRegister");
            }
        }

        private ObservableCollection<string> roles = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("roles"));
        public ObservableCollection<string> Roles
        {
            get
            {
                return roles;
            }
        }

        protected override void createNewOneCmdHandler(object parameter)
        {
            base.createNewOneCmdHandler(parameter);
            if (Roles.Count > 0)
                CurrentInfo.Role = Roles[0];
        }

        public UserInfoFrameVM(FrameNavigation navigation)
            : base(navigation, CrsDictionary.getInstance().lookup("infoPageTitles", "user_info"), 1.25)
        {
            setCashRegisters();

            if (Roles.Count > 0 && !CurrentInfo.Recorded)
                CurrentInfo.Role = Roles[0];
        }

        protected async Task setCashRegisters()
        {
            CashRegisters = new ObservableCollection<EnterpriseAccountVM>(
                await EnterpriseAccountVM.getList<EnterpriseAccountVM>(c => c.Type == 0, c => c.CreateDate));

            if (CurrentInfo.CashRegisterId > 0)
            {
                foreach(EnterpriseAccountVM cr in CashRegisters)
                    if (cr.Id.Equals(CurrentInfo.CashRegisterId))
                    {
                        SelectedCashRegister = cr;
                        break;
                    }
            }
            else
                SelectedCashRegister = CashRegisters[0];

            SelectedCashRegister.Refresh();
        }
    }
}
