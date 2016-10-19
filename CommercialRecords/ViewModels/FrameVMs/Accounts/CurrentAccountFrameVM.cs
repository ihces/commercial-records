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

        private ObservableCollection<string> accountTypes = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("accountTypes"));
        public ObservableCollection<string> AccountTypes
        {
            get
            {
                return accountTypes;
            }
        }

        private CurrentAccountVM newAccountBuff;

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
                        InitialAmount = 0;
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
                {
                    if (!SelectedAccount.Active)
                        EnableAccountTypeField = true;

                    AccountConfButtonIcon = new SymbolIcon(Symbol.Save);
                }
                else
                {
                    EnableAccountTypeField = false;
                    AccountConfButtonIcon = new SymbolIcon(Symbol.Edit);
                }

                RaisePropertyChanged("EnableAccountField");
            }
        }

        private bool canAccountBeDeleted = false;
        public bool CanAccountBeDeleted
        {
            get
            {
                return canAccountBeDeleted;
            }
            set
            {

                canAccountBeDeleted = value;
                RaisePropertyChanged("CanAccountBeDeleted");
            }
        }

        private bool enableAccountTypeField = false;
        public bool EnableAccountTypeField
        {
            get
            {
                return enableAccountTypeField;
            }
            set
            {

                enableAccountTypeField = value;
                RaisePropertyChanged("EnableAccountTypeField");
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

        private readonly ICommand deleteSelectedAccountCmd;
        public ICommand DeleteSelectedAccountCmd
        {
            get
            {
                return deleteSelectedAccountCmd;
            }
        }

        private readonly ICommand deleteSelectedTransactCmd;
        public ICommand DeleteSelectedTransactCmd
        {
            get
            {
                return deleteSelectedTransactCmd;
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
            if (Accounts.Count == 0 || Accounts[Accounts.Count - 1].Recorded)
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
            deleteSelectedAccountCmd = new ICommandImp(deleteSelectedAccountCmd_execute);
            doPaymentCmd = new ICommandImp(doPaymentCmd_execute);
            openTransactionCmd = new ICommandImp(openTransactionCmd_execute);
            deleteSelectedTransactCmd = new ICommandImp(deleteSelectedTransactCmd_execute);

            if (navigation.Back.Is<CurrentAccountList>())
            {
                CurrentActor = (ActorVM)navigation.Message;
                CurrentActor.Name += " " + CurrentActor.Surname;
                setAccounts();
            }
        }

        private void deleteSelectedAccountCmd_execute(object obj)
        {
            if (SelectedAccount.Active)
            {
                var messageDialog = new MessageDialog("Seçilen Hesap Aktif Olduğu İçin Silemiyoruz.", "Hesap Silme");
                messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "okCommand"), null));
                messageDialog.DefaultCommandIndex = 1;
                messageDialog.CancelCommandIndex = 0;
                messageDialog.ShowAsync();

            }
            else
            {
                var messageDialog = new MessageDialog("Seçili Hesabı Silmek Üzeresiniz. Emin misiniz?", "Hesap Silme");

                messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "noCommand"), null));
                messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "yesCommand"), new UICommandInvokedHandler(this.DelAccountCommandInvokedHandler)));

                messageDialog.DefaultCommandIndex = 1;
                messageDialog.CancelCommandIndex = 0;
                messageDialog.ShowAsync();
            }

        }

        private void DelAccountCommandInvokedHandler(IUICommand command)
        {
            SelectedAccount.delete();
        }

        private void deleteSelectedTransactCmd_execute(object obj)
        {
            var messageDialog = new MessageDialog("Seçili İşlemi Silmek Üzeresiniz. Emin misiniz?", "İşlem Silme");

            messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "noCommand"), null));
            messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "yesCommand"), new UICommandInvokedHandler(this.DelTransactCommandInvokedHandler)));

            messageDialog.DefaultCommandIndex = 1;
            messageDialog.CancelCommandIndex = 0;
            messageDialog.ShowAsync();
        }

        private void DelTransactCommandInvokedHandler(IUICommand command)
        {
            SelectedTransact.delete();
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


                if (!SelectedAccount.Recorded)
                {
                    switch (SelectedAccount.Type)
                    {
                        case CurrentAccountVM.TYPE_DEBT:
                            SelectedAccount.TotalDebt = SelectedAccount.InitialAmount;
                            break;
                        case CurrentAccountVM.TYPE_RECEIVABLE:
                            SelectedAccount.TotalCredit = SelectedAccount.InitialAmount;
                            break;
                    }
                }

                SelectedAccount.save();
                messageDialog.ShowAsync();
            }

            EnableAccountField = !EnableAccountField;
        }

        private async Task setTransacts()
        {
            Transacts = new ObservableCollection<TransactVM>(
                await TransactVM.getList<TransactVM>(c => c.AccountId == selectedAccount.Id, c => c.Date));
        }

        private async Task setAccounts()
        {
            Accounts = new ObservableCollection<CurrentAccountVM>(
                await CurrentAccountVM.getList<CurrentAccountVM>(c => c.ActorId == CurrentActor.Id, c => c.Name));

            SelectedAccount = Accounts[0];
        }
    }
}
