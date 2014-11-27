
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
namespace CommercialRecordSystem.ViewModels.Transacts
{
    abstract class TransactFrameVMBase<E> : FrameVMBase
    {
        #region Properties
        private readonly string transactDateStr = string.Empty;
        public string TransactDateStr
        {
            get
            {
                return transactDateStr;
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

        private E entryBuff = (E)new object();
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

        private TransactVM transactInfo = new TransactVM();
        #endregion

        public TransactFrameVMBase(Frame frame, TransactVM transactInfo)
            : base(frame)
        {
            addEntryToListCmd = new ICommandImp(addEntryToListCmdHandler);
            goNextCmd = new ICommandImp(goNextCmdHandler);
            deleteEntryCmd = new ICommandImp(deleteEntryCmdHandler);

            System.DateTime transactDateBuff = transactObj.Date;
            saleDateStr = transactDateBuff.ToString("dd.MM.yyyy");
            selectedCustomer = CustomerVM.get(transactObj.CustomerId);

            selectedCustomer.Name = UpperCaseFirst(selectedCustomer.Name) + " " + selectedCustomer.Surname.ToUpper();
        }
    }
}
