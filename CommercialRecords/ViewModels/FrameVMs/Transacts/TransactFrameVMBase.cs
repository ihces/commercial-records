using CommercialRecords.Common;
using CommercialRecords.Models;
using CommercialRecords.Models.Transacts;
using CommercialRecords.ViewModels.DataVMs.Accounts;
using CommercialRecords.Views.Transacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace CommercialRecords.ViewModels.Transacts
{
    abstract class TransactFrameVMBase<E, T> : FrameVMBase
        where E : TransactEntryVMBase<T>, new()
        where T : TransactEntry, new()
    {
        #region Properties
        private TransactVM transactInfo = new TransactVM();
        public TransactVM TransactInfo
        {
            get
            {
                return transactInfo;
            }
            set
            {
                transactInfo = value;
                RaisePropertyChanged("TransactInfo");
            }
        }

        private ActorVM currentActor = new ActorVM() {Type = Actor.TYPE_PERSON, Registered=false };
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

        private CurrentAccountVM currentAccount = new CurrentAccountVM();
        public CurrentAccountVM CurrentAccount
        {
            get
            {
                return currentAccount;
            }
            set
            {
                currentAccount = value;
                RaisePropertyChanged("CurrentAccount");
            }
        }

        private ObservableCollection<E> entries = new ObservableCollection<E>();
        public ObservableCollection<E> Entries
        {
            get
            {
                return entries;
            }
            set
            {
                entries = value;
                RaisePropertyChanged("Entries");
            }
        }

        private E entryBuff = new E();
        public E EntryBuff
        {
            get
            {
                return entryBuff;
            }
            set
            {
                entryBuff = value;
                RaisePropertyChanged("EntryBuff");
            }
        }

        private E selectedEntry;
        public E SelectedEntry
        {
            get
            {
                return selectedEntry;
            }
            set
            {
                if (value != selectedEntry && null != selectedEntry)
                    selectedEntry.IsSelected = false;

                selectedEntry = value;
                if (null != selectedEntry)
                    selectedEntry.IsSelected = true;

                RaisePropertyChanged("SelectedEntry");
            }
        }

        private bool isAllChecked = false;
        public bool IsAllChecked
        {
            get
            {
                return isAllChecked;
            }
            set
            {
                isAllChecked = value;
                foreach (var entry in this.entries)
                    entry.IsChecked = isAllChecked;

                RaisePropertyChanged("IsAllChecked");
            }
        }
        #endregion
        #region Commands
        private readonly ICommand addEntryToListCmd;
        public ICommand AddEntryToListCmd
        {
            get
            {
                return addEntryToListCmd;
            }
        }

        private readonly ICommand deleteEntryCmd;
        public ICommand DeleteEntryCmd
        {
            get
            {
                return deleteEntryCmd;
            }
        }

        private readonly ICommand goNextCmd;
        public ICommand GoNextCmd
        {
            get
            {
                return goNextCmd;
            }
        }
        #endregion

        #region Command Handlers
        protected virtual void addEntryToListCmdHandler(object parameter)
        {
            if (EntryBuff is PaymentEntryVM)
            {
                transactInfo.Paid += EntryBuff.Cost;
                currentAccount.TotalCredit += EntryBuff.Cost;
            }
            else
            {
                currentAccount.TotalDebt += EntryBuff.Cost;
                transactInfo.Cost += EntryBuff.Cost;
            }

            TransactInfo.EntryCount++;
            TransactInfo.ModifyDate = DateTime.Now;
            CurrentAccount.LastTransactDate = TransactInfo.ModifyDate;

            if (CurrentActor.Registered)
                CurrentAccount.save();
            
            TransactInfo.save();
            
            EntryBuff.TransactId = TransactInfo.Id;
            EntryBuff.save();

            Entries.Add(EntryBuff);
            SelectedEntry = EntryBuff;
            
            EntryBuff = new E();
            EntryBuff.Refresh();
            
            IsAllChecked = false;
        }

        protected abstract void goNextCmdHandler(object parameter);
        protected override void goBackCmdHandler(object parameter)
        {
            if (0 == TransactInfo.EntryCount && !Navigation.Back.Is<Sales>()/*back to sales from payment in current transact*/)
            {
                if (!CurrentActor.Registered && CurrentActor.Recorded)
                    CurrentActor.delete();

                if (TransactInfo.Recorded)
                    TransactInfo.delete();
            }

            Navigation.GoBack(transactInfo);
        }
        private void deleteEntryCmdHandler(object parameter)
        {
            int checkedEntryCnt = 0;
            for (int i = 0; i < entries.Count; ++i)
            {
                if (entries[i].IsChecked)
                {
                    checkedEntryCnt++;
                }
            }

            if (0 < checkedEntryCnt)
            {
                MessageDialog messageDialog = new MessageDialog(
                    CrsDictionary.getInstance().lookup("notifications","delRecordMessage", checkedEntryCnt),
                    CrsDictionary.getInstance().lookup("notifications", "delRecordHeader"));

                // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
                messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "noCommand"), null));
                messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "yesCommand"), new UICommandInvokedHandler(this.deleteSelectedEntries)));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 1;

                // Set the command to be invoked when escape is pressed
                messageDialog.CancelCommandIndex = 0;

                // Show the message dialog
                messageDialog.ShowAsync();
            }
        }
        protected virtual void deleteSelectedEntries(IUICommand command)
        {
            ObservableCollection<E> entriesBuff = new ObservableCollection<E>();
            foreach (E entry in Entries)
            {
                if (entry.IsChecked)
                {
                    entry.delete();

                    if (entry is PaymentEntryVM)
                    {
                        TransactInfo.Paid -= entry.Cost;
                        currentAccount.TotalCredit -= entry.Cost;
                    }
                    else
                    {
                        currentAccount.TotalDebt -= entry.Cost;
                        TransactInfo.Cost -= entry.Cost;
                    }

                    TransactInfo.EntryCount--;
                }
                else
                {
                    entriesBuff.Add(entry);
                }
            }

            TransactInfo.ModifyDate = DateTime.Now;
            currentAccount.LastTransactDate = TransactInfo.ModifyDate;

            currentAccount.save();
            TransactInfo.save();

            Entries = entriesBuff;
            IsAllChecked = false;
        }
        #endregion

        public TransactFrameVMBase(FrameNavigation navigation)
            : base(navigation)
        {
            addEntryToListCmd = new ICommandImp(addEntryToListCmdHandler);
            goNextCmd = new ICommandImp(goNextCmdHandler);
            deleteEntryCmd = new ICommandImp(deleteEntryCmdHandler);

            if (navigation.Message is TransactVM)
            {
                TransactInfo = (TransactVM)navigation.Message;
            }
            
            if (TransactInfo.Recorded)
            {
                CurrentActor.get(transactInfo.ActorId);
                CurrentAccount.get(transactInfo.AccountId);
                CurrentActor.Refresh();
                CurrentAccount.Refresh();
                setEntries();
            }
        }

        protected async Task setEntries()
        {
            List<Expression<Func<T, object>>> orderByList = new List<Expression<Func<T, object>>>();
            orderByList.Add(e => e.Id);

            Entries = new ObservableCollection<E>(await TransactEntryVMBase<T>.getList<E>(e => transactInfo.Id == e.TransactId, orderByList));
        }
    }
}
