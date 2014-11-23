using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels
{
    class SaleVM : FrameVMBase
    {
        #region Properties

        private readonly string header = "Satış";
        public string Header
        {
            get
            {
                return header;
            }
        }

        private readonly string saleDateStr = string.Empty;
        public string SaleDateStr
        {
            get
            {
                return saleDateStr;
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

        private ObservableCollection<SaleEntryVM> entries = new ObservableCollection<SaleEntryVM>();
        public ObservableCollection<SaleEntryVM> Entries
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

        private SaleEntryVM entryBuff = new SaleEntryVM();
        public SaleEntryVM EntryBuff
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
        private void addEntryToListCmdHandler(object parameter)
        {
            EntryBuff.Cost = EntryBuff.Amount * EntryBuff.UnitCost;
            SaleEntryVM.save(EntryBuff);
            Entries.Add(EntryBuff);
            EntryBuff = new SaleEntryVM();
            EntryBuff.Refresh();
            IsAllChecked = false;
        }

        private void goNextCmdHandler(object parameter)
        {
            this.Navigate(typeof(Payments), this);
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
            ObservableCollection<SaleEntryVM> entriesBuff = new ObservableCollection<SaleEntryVM>();
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

        public SaleVM(Frame frame, TransactTypeVM transactObj)
            : base(frame)
        {
            addEntryToListCmd = new ICommandImp(addEntryToListCmdHandler);
            goNextCmd = new ICommandImp(goNextCmdHandler);
            deleteEntryCmd = new ICommandImp(deleteEntryCmdHandler);

            System.DateTime transactDateBuff =  transactObj.SelectedDate;
            saleDateStr = transactDateBuff.ToString("dd.MM.yyyy");
            selectedCustomer = CustomerVM.get(transactObj.CurrentCustomer.Id);

            if (null == selectedCustomer)
            {
                selectedCustomer = transactObj.CurrentCustomer;
            }

            selectedCustomer.Name = UpperCaseFirst(selectedCustomer.Name) + " " + selectedCustomer.Surname.ToUpper();

            if (transactObj.SelectedTransactTypeIndex.Equals(TransactTypeVM.ORDER_TRANSACT))
                header = "Sipariş";
        }

        private async Task setEntries()
        {
            Entries = await SaleEntryVM.getSaleEntries(selectedCustomer.Id, 1);
        }
    }
}
