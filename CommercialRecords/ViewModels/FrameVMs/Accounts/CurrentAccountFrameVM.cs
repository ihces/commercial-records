using System;
using CommercialRecords.Models;
using Windows.UI.Xaml.Controls;
using CommercialRecords.Common;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommercialRecords.Views.Transacts;
using CommercialRecords.ViewModels.DataVMs.Accounts;
using CommercialRecords.Models.Accounts;
using CommercialRecords.Views.Accounts;
using Windows.UI.Popups;

namespace CommercialRecords.ViewModels
{
    class CurrentAccountFrameVM : FrameVMBase
    {
        #region Properties
        private ActorVM currentActor = new ActorVM();
        public ActorVM CurrentActor
        { 
            get
            {
                return currentActor;
            }
            set
            {
                currentActor = value;
                RaisePropertyChanged("CurrentActor");
            }
        }

        /*private ObservableCollection<string> accountTypes = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("accountTypes"));
        public ObservableCollection<string> AccountTypes
        { 
            get
            {
                return accountTypes;
            }
        }*/

        private CurrentAccountVM newAccountBuff;

        private CurrentAccountVM selectedAccount;
        public CurrentAccountVM SelectedAccount
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
            if (SelectedAccount.Recorded){
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
            messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "yesCommand"), new UICommandInvokedHandler(this.DelInfoCommandInvokedHandler)));

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

        private ObservableCollection<CurrentAccountVM> accounts;
        public ObservableCollection<CurrentAccountVM> Accounts
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

        private TransactVM selectedTransact = new TransactVM();
        public TransactVM SelectedTransact
        { 
            get
            {
                return selectedTransact;
            }
            set
            {
                selectedTransact = value;
                RaisePropertyChanged("SelectedTransact");
            }
        }

        private ObservableCollection<TransactVM> transacts = new ObservableCollection<TransactVM>();
        public ObservableCollection<TransactVM> Transacts
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

        private readonly ICommand openTransactionCmd;
        public ICommand OpenTransactionCmd
        {
            get
            {
                return openTransactionCmd;
            }
        }

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

        private void createNewAccountCmd_execute(object obj)
        {
            if (Accounts[Accounts.Count - 1].Recorded)
            {
                newAccountBuff = new CurrentAccountVM();
                newAccountBuff.ActorId = CurrentActor.Id;
                newAccountBuff.Type = 0;
                Accounts.Add(newAccountBuff);
                SelectedAccount = newAccountBuff;
                EnableAccountField = true;
            }
        }

        private readonly ICommand doPaymentCmd;
        public ICommand DoPaymentCmd
        {
            get
            {
                return doPaymentCmd;
            }
        }

        

        private void doPaymentCmd_execute(object obj)
        {
            Navigation.Navigate(typeof(Payments), CurrentActor.Id);
        }

        private void openTransactionCmd_execute(object obj)
        {
            if (Transact.TYPE_PAYMENT.Equals(SelectedTransact.Type))
                Navigation.Navigate(typeof(Payments), SelectedTransact);
            else
                Navigation.Navigate(typeof(Sales), SelectedTransact);
        }
        

        #endregion

        public CurrentAccountFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            createNewAccountCmd = new ICommandImp(createNewAccountCmd_execute);
            editCurrentAccountCmd = new ICommandImp(editCurrentAccountCmd_execute);
            doPaymentCmd = new ICommandImp(doPaymentCmd_execute); 
            openTransactionCmd = new ICommandImp(openTransactionCmd_execute);

            if (navigation.Back.Is<CurrentAccountList>()) {
                CurrentActor = (ActorVM)navigation.Message;
                CurrentActor.Name += " " + CurrentActor.Surname;
                setAccounts();
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

        private async Task setTransacts()
        {
            List<Expression<Func<Transact, object>>> orderByClauses = null;
            orderByClauses = new List<Expression<Func<Transact, object>>>();
                orderByClauses.Add(c => c.Date);
            
            Transacts = new ObservableCollection<TransactVM>(
                await TransactVM.getList<TransactVM>(c => c.AccountId == selectedAccount.Id, orderByClauses));
        }

        private async Task setAccounts()
        {
            List<Expression<Func<CurrentAccount, object>>> orderByClauses = null;
            orderByClauses = new List<Expression<Func<CurrentAccount, object>>>();
            orderByClauses.Add(c => c.Name);

            Accounts = new ObservableCollection<CurrentAccountVM>(
                await CurrentAccountVM.getList<CurrentAccountVM>(c => c.ActorId == CurrentActor.Id, orderByClauses));

            SelectedAccount = Accounts[0];
        }
    }
}
