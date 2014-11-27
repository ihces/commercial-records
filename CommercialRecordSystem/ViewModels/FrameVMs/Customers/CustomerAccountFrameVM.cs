using System;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerAccountFrameVM : FrameVMBase
    {
        #region Properties
        private CustomerVM currentCustomer = new CustomerVM();
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

        public CustomerAccountFrameVM(Frame frame, int customerId)
            : base(frame)
        {
            CurrentCustomer.get(customerId);
            CurrentCustomer.Name += " " + CurrentCustomer.Surname;
        }
    }
}
