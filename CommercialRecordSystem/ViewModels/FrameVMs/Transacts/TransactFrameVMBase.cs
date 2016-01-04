using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.Models.Accounts;
using CommercialRecordSystem.Models.Transacts;
using CommercialRecordSystem.ViewModels.DataVMs.Accounts;
using CommercialRecordSystem.ViewModels.Transacts.Payment;
using CommercialRecordSystem.Views.Transacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels.Transacts
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
                if (null != currentAccount && !currentAccount.Equals(value))
                {
                    currectAccountChanged();
                }

                currentAccount = value;
                RaisePropertyChanged("CurrentAccount");
            }
        }

        private void currectAccountChanged()
        {
            TransactInfo.AccountId = CurrentAccount.Id;

            CurrentActor.LastTransactDate = DateTime.Now;
            CurrentAccount.LastTransactDate = CurrentActor.LastTransactDate;

            if (TransactInfo.Recorded)
            {
                TransactInfo.save();
                CurrentActor.save();
                CurrentAccount.save();
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
                currentActor.TotalPaid += EntryBuff.Cost;
                currentAccount.TotalCredit += EntryBuff.Cost;
            }
            else
            {
                currentActor.TotalCost += EntryBuff.Cost;
                currentAccount.TotalDept += EntryBuff.Cost;
                transactInfo.Cost += EntryBuff.Cost;
            }

            TransactInfo.EntryCount++;
            TransactInfo.ModifyDate = DateTime.Now;
            CurrentActor.LastTransactDate = TransactInfo.ModifyDate;
            CurrentAccount.LastTransactDate = TransactInfo.ModifyDate;

            if (CurrentActor.Registered)
                CurrentAccount.save();
            
            CurrentActor.save();
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
            if (0 == TransactInfo.EntryCount && typeof(Sales) != Navigation.Back.PageType/*back to sales from payment in current transact*/)
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
                MessageDialog messageDialog = new MessageDialog("Seçili " + checkedEntryCnt + " kayıt silinecek. Emin misiniz?", "Kayıt Silme");

                // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
                messageDialog.Commands.Add(new UICommand("Hayır", null));
                messageDialog.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(this.deleteSelectedEntries)));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 1;

                // Set the command to be invoked when escape is pressed
                messageDialog.CancelCommandIndex = 0;

                // Show the message dialog
                messageDialog.ShowAsync();
            }
        }
        private void deleteSelectedEntries(IUICommand command)
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
                        currentActor.TotalPaid -= EntryBuff.Cost;
                        currentAccount.TotalCredit -= EntryBuff.Cost;
                    }
                    else
                    {
                        currentActor.TotalCost -= EntryBuff.Cost;
                        currentAccount.TotalDept -= EntryBuff.Cost;
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
            currentActor.LastTransactDate = TransactInfo.ModifyDate;
            currentAccount.LastTransactDate = TransactInfo.ModifyDate;

            currentActor.save();
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
                CurrentActor.get(transactInfo.CustomerId);
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
