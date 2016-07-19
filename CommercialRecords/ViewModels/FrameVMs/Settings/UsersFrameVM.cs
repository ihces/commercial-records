using CommercialRecords.Common;
using CommercialRecords.ViewModels.DataVMs.Settings;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using CommercialRecords.Views.Settings;
using System.Threading.Tasks;
using CommercialRecords.ViewModels.DataVMs.Accounts.EnterpriseAccounts;
using System.Collections.Generic;
using System.Linq.Expressions;
using CommercialRecords.Models.Accounts;

namespace CommercialRecords.ViewModels.FrameVMs.Settings
{
    class UsersFrameVM : FrameVMBase
    {
        #region Properties
        private ObservableCollection<UserVM> users;
        public ObservableCollection<UserVM> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                RaisePropertyChanged("Users");
            }
        }

        private UserVM selectedUser;
        public UserVM SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;

                if (null != selectedUser && null != CashRegisters)
                {
                    foreach (EnterpriseAccountVM cr in CashRegisters)
                        if (cr.Id.Equals(selectedUser.CashRegisterId))
                        {
                            SelectedCashRegister = cr;
                            SelectedCashRegister.Refresh();
                            break;
                        }
                }
                RaisePropertyChanged("SelectedUser");
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
                RaisePropertyChanged("SelectedCashRegister");
            }
        }

        #endregion Properties

        private readonly ICommand addUserCmd;
        public ICommand AddUserCmd
        {
            get
            {
                return addUserCmd;
            }
        }

        private readonly ICommand editCurrentUserCmd;
        public ICommand EditCurrentUserCmd
        {
            get
            {
                return editCurrentUserCmd;
            }
        }

        public UsersFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            addUserCmd = new ICommandImp(addUser_execute);
            editCurrentUserCmd = new ICommandImp(editCurrentUserCmd_execute);
            setUsers();
            setCashRegisters();
        }

        private void editCurrentUserCmd_execute(object obj)
        {
            Navigation.Navigate(typeof(UserInfo), SelectedUser.Id);
        }

        private void addUser_execute(object obj)
        {
            Navigation.Navigate(typeof(UserInfo));
        }

        private async Task setUsers()
        {
            Users = new ObservableCollection<UserVM>(await UserVM.getList<UserVM>());
        }

        protected async Task setCashRegisters()
        {
            CashRegisters = new ObservableCollection<EnterpriseAccountVM>(
                await EnterpriseAccountVM.getList<EnterpriseAccountVM>(c => c.Type == 0, c => c.CreateDate));

        }
    }
}
