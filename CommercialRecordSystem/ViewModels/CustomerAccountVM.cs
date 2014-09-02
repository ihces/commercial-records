using System;
using CommercialRecordSystem.Models;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerAccountVM : VMBase
    {
        #region Properties
        private CustomerVM currentCustomer = null;
        public CustomerVM CurrentCustomer
        { 
            get
            {
                return currentCustomer;
            }
            set
            {
                currentCustomer = value;
                RaisePropertyChanged("CurrentCustomer");
            }
        }

        #endregion

        public CustomerAccountVM(int customerId)
        {
            CurrentCustomer = CustomerVM.getCustomer(customerId);
            CurrentCustomer.Name += " " + CurrentCustomer.Surname;
        }
    }
}
