using System;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerAccountVM : FrameVMBase
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

        public CustomerAccountVM(Frame frame, int customerId) : base(frame)
        {
            CurrentCustomer = CustomerVM.get(customerId);
            CurrentCustomer.Name += " " + CurrentCustomer.Surname;
        }
    }
}
