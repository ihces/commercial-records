
using CommercialRecordSystem.Common;
using System;
using System.Windows.Input;
namespace CommercialRecordSystem.ViewModels
{
    class TransactTypeVM : VMBase
    {
        #region Properties
        private CustomerVM selectedCustomer = new CustomerVM();
        public CustomerVM SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                RaisePropertyChanged("SelectedCustomer");
            }
        }

        private DateTime selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }
        #endregion

        public TransactTypeVM(int selectedCustId = 0)
        {
            if (0 != selectedCustId)
            {
                SelectedCustomer = CustomerVM.get(selectedCustId);
            }
        }
    }
}
