using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommercialRecordSystem.ViewModels
{
    class SaleVM : VMBase
    {
        #region Properties
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

        private readonly ICommand addEntryToListCmd;
        public ICommand AddEntryToListCmd
        {
            get
            {
                return addEntryToListCmd;
            }
        }

        private void addEntryToListCmdHandler(object parameter)
        {
            SaleEntryVM.save(EntryBuff);
            Entries.Add(EntryBuff);
            EntryBuff = new SaleEntryVM();
        }

        public SaleVM(string transactDateStr, int customerId, int transactId = 0)
        {
            addEntryToListCmd = new ICommandImp(addEntryToListCmdHandler);
            saleDateStr = transactDateStr;
            selectedCustomer = CustomerVM.get(customerId);
            selectedCustomer.Name += " " + selectedCustomer.Surname;

        }

        private async Task setEntries()
        {
            Entries = await SaleEntryVM.getSaleEntries(selectedCustomer.Id, 1);
        }
    }
}
