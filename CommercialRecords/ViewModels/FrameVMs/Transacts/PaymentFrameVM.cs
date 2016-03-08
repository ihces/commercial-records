using CommercialRecords.Common;
using CommercialRecords.Models;
using CommercialRecords.Models.Accounts;
using CommercialRecords.ViewModels.DataVMs.Accounts;
using CommercialRecords.ViewModels.DataVMs.Accounts.EnterpriseAccounts;
using CommercialRecords.ViewModels.Transacts;
using CommercialRecords.Views;
using CommercialRecords.Views.Transacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace CommercialRecords.ViewModels
{
    class PaymentFrameVM : TransactFrameVMBase<PaymentEntryVM, PaymentEntry>
    {
        private ObservableCollection<string> paymentTypes = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("paymentTypes"));
        public ObservableCollection<string> PaymentTypes
        {
            get
            {
                return paymentTypes;
            }
        }

        #region Command Handlers
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

        private ObservableCollection<EnterpriseAccountVM> enterpriseAccounts;
        public ObservableCollection<EnterpriseAccountVM> EnterpriseAccounts
        {
            get
            {
                return enterpriseAccounts;
            }
            set
            {
                enterpriseAccounts = value;
                RaisePropertyChanged("EnterpriseAccounts");
            }
        }

        private EnterpriseAccountVM selectedEnterpriseAccount = new EnterpriseAccountVM();
        public EnterpriseAccountVM SelectedEnterpriseAccount
        {
            get
            {
                return selectedEnterpriseAccount;
            }
            set
            {
                selectedEnterpriseAccount = value;
                RaisePropertyChanged("SelectedEnterpriseAccount");
            }
        }

        protected override void goNextCmdHandler(object parameter)
        {

            Frame frameBuff = navigation.PageFrame;
            navigation = new FrameNavigation(typeof(MainPage));
            navigation.PageFrame = frameBuff;

            Navigation.Navigate(typeof(TransactList), TransactInfo);
        }

        protected override void addEntryToListCmdHandler(object parameter)
        {
            if (EntryBuff.Cost != 0 && SelectedEnterpriseAccount != null)
            {
                PaymentEntryVM newEntry = EntryBuff;

                base.addEntryToListCmdHandler(parameter);

                EnterpriseAccTransactVM enterpriseAccTransact = new EnterpriseAccTransactVM();
                enterpriseAccTransact.TransactId = newEntry.TransactId;
                enterpriseAccTransact.Date = newEntry.Date;
                enterpriseAccTransact.AccountId = SelectedEnterpriseAccount.Id;
                enterpriseAccTransact.TransactEntryId = newEntry.Id;

                if (TransactInfo.Type == 2)
                {
                    enterpriseAccTransact.Type = 1;
                    enterpriseAccTransact.Amount = -newEntry.Cost;
                }
                else
                {
                    enterpriseAccTransact.Type = 0;
                    enterpriseAccTransact.Amount = newEntry.Cost;
                }

                SelectedEnterpriseAccount.Balance += enterpriseAccTransact.Amount;
                SelectedEnterpriseAccount.LastTransactDate = enterpriseAccTransact.Date;
                enterpriseAccTransact.save();
                SelectedEnterpriseAccount.save();
            }
        }

        protected override async void deleteSelectedEntries(IUICommand command)
        {
            ObservableCollection<PaymentEntryVM> oldEntries = Entries;

            base.deleteSelectedEntries(command);

            ObservableCollection<PaymentEntryVM> newEntries = Entries;

            foreach (PaymentEntryVM entry in oldEntries)
            {
                if (!newEntries.Contains(entry))
                {
                    List<EnterpriseAccTransactVM> entAccTransacts = await EnterpriseAccTransactVM.getList<EnterpriseAccTransactVM>(
                        c => c.TransactId == TransactInfo.Id && c.TransactEntryId == entry.Id, null);
                    SelectedEnterpriseAccount.Balance -= entAccTransacts[0].Amount;

                    entAccTransacts[0].delete();
                }
            }

            SelectedEnterpriseAccount.save();
        }

        #endregion

        public PaymentFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            if (CurrentActor.Registered)
            {
                Accounts = new ObservableCollection<CurrentAccountVM>();
                Accounts.Add(CurrentAccount);
            }

            setEnterpriseAccounts();
        }

        protected async Task setEnterpriseAccounts()
        {
            List<Expression<Func<EnterpriseAccount, object>>> orderByClauses = null;
            orderByClauses = new List<Expression<Func<EnterpriseAccount, object>>>();
            orderByClauses.Add(c => c.CreateDate);

            EnterpriseAccounts = new ObservableCollection<EnterpriseAccountVM>(
                await EnterpriseAccountVM.getList<EnterpriseAccountVM>(c=>c.Type == 0, orderByClauses));

            SelectedEnterpriseAccount = EnterpriseAccounts[0];
            SelectedEnterpriseAccount.Refresh();
        }
    }
}
