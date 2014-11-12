using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private readonly ICommand goNextCmd;
        public ICommand GoNextCmd
        {
            get
            {
                return goNextCmd;
            }
        }
        #endregion

        public SaleVM(Frame frame, TransactTypeVM transactObj)
            : base(frame)
        {
            addEntryToListCmd = new ICommandImp(addEntryToListCmdHandler);
            goNextCmd = new ICommandImp(goNextCmdHandler);

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

        #region Command Handlers
        private void addEntryToListCmdHandler(object parameter)
        {
            SaleEntryVM.save(EntryBuff);
            Entries.Add(EntryBuff);
            EntryBuff = new SaleEntryVM();
        }

        private void goNextCmdHandler(object parameter)
        {
            this.Navigate(typeof(Payments));
        }
        #endregion

        private async Task setEntries()
        {
            Entries = await SaleEntryVM.getSaleEntries(selectedCustomer.Id, 1);
        }
    }
}
