using CommercialRecords.Common;
using CommercialRecords.Models.Accounts;
using CommercialRecords.Models.Accounts.EnterpriseAccounts;
using CommercialRecords.ViewModels.DataVMs.Accounts.EnterpriseAccounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace CommercialRecords.ViewModels.FrameVMs
{
    class EnterpriseAccountsFrameVM : FrameVMBase
    {
        #region Properties
        private ObservableCollection<EnterpriseAccountVM> accounts;
        public ObservableCollection<EnterpriseAccountVM> Accounts
        {
            get
            {
                return accounts;
            }
            set
            {
                accounts = value;
                RaisePropertyChanged("Accounts");
            }
        }

        private ObservableCollection<EnterpriseAccTransactVM> transacts = new ObservableCollection<EnterpriseAccTransactVM>();
        public ObservableCollection<EnterpriseAccTransactVM> Transacts
        {
            get
            {
                return transacts;
            }
            set
            {
                transacts = value;
                RaisePropertyChanged("Transacts");
            }
        }

        private ObservableCollection<string> accountTypes = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("enterpriseAccountTypes"));
        public ObservableCollection<string> AccountTypes
        {
            get
            {
                return accountTypes;
            }
        }

        private EnterpriseAccountVM newAccountBuff;
        private EnterpriseAccountVM selectedAccount;
        public EnterpriseAccountVM SelectedAccount
        {
            get
            {
                return selectedAccount;
            }
            set
            {
                if (null != value && !value.Equals(selectedAccount))
                {
                    if (null != selectedAccount && selectedAccount.Dirty)
                    {
                        needSaveAccountInfo();
                    }

                    if (!EnableAccountField)
                    {
                        selectedAccount = value;
                        setTransacts();
                    }
                }
                RaisePropertyChanged("SelectedAccount");
            }
        }

        private void needSaveAccountInfo()
        {
            string message = "";
            string header = "";
            if (SelectedAccount.Recorded)
            {
                header = CrsDictionary.getInstance().lookup("notifications", "saveNewAccountHeader");
                message = CrsDictionary.getInstance().lookup("notifications", "saveAccountMessage");
            }
            else
            {
                message = CrsDictionary.getInstance().lookup("notifications", "saveChangesHeader");
                CrsDictionary.getInstance().lookup("notifications", "saveAccountMessage");
            }
            var messageDialog = new MessageDialog(message, header);

            messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "noCommand"), null));
            messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "yesCommand"), 
                new UICommandInvokedHandler(this.DelInfoCommandInvokedHandler)));

            messageDialog.DefaultCommandIndex = 1;
            messageDialog.CancelCommandIndex = 0;
            messageDialog.ShowAsync();
        }

        private void DelInfoCommandInvokedHandler(IUICommand command)
        {
            EnableAccountField = false;

            if (null != newAccountBuff && !newAccountBuff.Recorded)
                Accounts.Remove(newAccountBuff);
        }

        private IconElement accountConfButtonIcon = new SymbolIcon(Symbol.Edit);
        public IconElement AccountConfButtonIcon
        {
            get
            {
                return accountConfButtonIcon;
            }
            set
            {

                accountConfButtonIcon = value;
                RaisePropertyChanged("AccountConfButtonIcon");
            }
        }

        private double totalAmount = 0;
        public double TotalAmount
        {
            get
            {
                return totalAmount;
            }
            set
            {
                totalAmount = value;
                RaisePropertyChanged("TotalAmount");
            }
        }
        
        private bool enableAccountField = false;
        public bool EnableAccountField
        {
            get
            {
                return enableAccountField;
            }
            set
            {

                enableAccountField = value;
                if (enableAccountField)
                    AccountConfButtonIcon = new SymbolIcon(Symbol.Save);
                else
                    AccountConfButtonIcon = new SymbolIcon(Symbol.Edit);

                RaisePropertyChanged("EnableAccountField");
            }
        }
        #endregion

        #region Commands
        private readonly ICommand editCurrentAccountCmd;
        public ICommand EditCurrentAccountCmd
        {
            get
            {
                return editCurrentAccountCmd;
            }
        }

        private readonly ICommand createNewAccountCmd;
        public ICommand CreateNewAccountCmd
        {
            get
            {
                return createNewAccountCmd;
            }
        }
        #endregion

        #region CommandHandlers
        private void createNewAccountCmd_execute(object obj)
        {
            if (Accounts[Accounts.Count - 1].Recorded)
            {
                newAccountBuff = new EnterpriseAccountVM();
                newAccountBuff.Type = 0;
                Accounts.Add(newAccountBuff);
                SelectedAccount = newAccountBuff;
                EnableAccountField = true;
            }
        }

        private void editCurrentAccountCmd_execute(object obj)
        {
            if (EnableAccountField && SelectedAccount.Dirty)
            {
                string message = "";
                if (!SelectedAccount.Recorded)
                    message = CrsDictionary.getInstance().lookup("notifications", "newRecordSavedMessage");
                else
                    message = CrsDictionary.getInstance().lookup("notifications", "accInfoUpdatedMessage");
                var messageDialog = new MessageDialog(message, CrsDictionary.getInstance().lookup("notifications", "accountInfoHeader"));

                messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "okCommand"), null));

                messageDialog.DefaultCommandIndex = 1;
                messageDialog.CancelCommandIndex = 0;
                messageDialog.ShowAsync();

                SelectedAccount.save();
            }

            EnableAccountField = !EnableAccountField;
        }
        #endregion

        public EnterpriseAccountsFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            createNewAccountCmd = new ICommandImp(createNewAccountCmd_execute);
            editCurrentAccountCmd = new ICommandImp(editCurrentAccountCmd_execute);
            setAccounts();
        }

        private async Task setAccounts()
        {
            List<Expression<Func<EnterpriseAccount, object>>> orderByClauses = new List<Expression<Func<EnterpriseAccount, object>>>();
            orderByClauses.Add(c => c.Name);

            Accounts = new ObservableCollection<EnterpriseAccountVM>(await EnterpriseAccountVM.getList<EnterpriseAccountVM>(null, orderByClauses));

            foreach (EnterpriseAccountVM acct in Accounts)
                TotalAmount += acct.Balance;

            SelectedAccount = Accounts[0];
        }

        private async Task setTransacts()
        {
            List<Expression<Func<EnterpriseAccTransact, object>>> orderByClauses = null;
            orderByClauses = new List<Expression<Func<EnterpriseAccTransact, object>>>();
            orderByClauses.Add(c => c.Date);

            Transacts = new ObservableCollection<EnterpriseAccTransactVM>(
                await EnterpriseAccTransactVM.getList<EnterpriseAccTransactVM>(c => c.AccountId == selectedAccount.Id, orderByClauses));
        }
    }
}
