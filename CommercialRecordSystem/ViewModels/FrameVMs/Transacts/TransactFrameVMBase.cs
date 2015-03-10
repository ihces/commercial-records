using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.Models.Transacts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels.Transacts
{
    abstract class TransactFrameVMBase<E,T> : FrameVMBase where E : TransactEntryVMBase<T>, new() where T:TransactEntry, new()
    {
        #region Properties
        private readonly string dateStr = string.Empty;
        public string DateStr
        {
            get
            {
                return dateStr;
            }
        }

        private readonly CustomerVM selectedCustomer = new CustomerVM();
        public CustomerVM SelectedCustomer
        {
            get
            {
                return selectedCustomer;
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

        protected TransactVM transactInfo = new TransactVM();
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
            EntryBuff.TransactId = transactInfo.Id;
            EntryBuff.save();
            Entries.Add(EntryBuff);
            EntryBuff = new E();
            EntryBuff.Refresh();
            IsAllChecked = false;
        }
        protected abstract void goNextCmdHandler(object parameter);
        protected override void goBackCmdHandler(object parameter)
        {
            Navigation.GoBack(transactInfo);
        }
        private void deleteEntryCmdHandler(object parameter)
        {
            int checkedEntryCnt = 0;
            for (int i=0; i<entries.Count; ++i)
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
            for (int i = 0; i < entries.Count; ++i)
            {
                if (!entries[i].IsChecked)
                {
                    entriesBuff.Add(entries[i]);
                }
            }

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
                transactInfo = (TransactVM)navigation.Message;

                System.DateTime transactDateBuff = transactInfo.Date;
                dateStr = transactDateBuff.ToString("dd.MM.yyyy");
                selectedCustomer.get(transactInfo.CustomerId);

                selectedCustomer.Name = UpperCaseFirst(selectedCustomer.Name) + " " + selectedCustomer.Surname.ToUpper();
            }

            setEntries();
        }

        protected async Task setEntries()
        {
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var saleEntryList = await db.Table<T>()
                                .Where(e => transactInfo.Id == e.TransactId)
                                .OrderBy(e => e.Id).ToListAsync();

            foreach (T entry in saleEntryList)
            {
                E entryBuff = new E();
                entryBuff.initWithModel(entry);
                Entries.Add(entryBuff);
            }
        }
    }
}
