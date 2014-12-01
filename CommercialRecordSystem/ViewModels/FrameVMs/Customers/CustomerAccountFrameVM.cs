using System;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Common;

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

        public CustomerAccountFrameVM(Frame frame, FrameNavigation navigation)
            : base(frame, navigation)
        {
            //CurrentCustomer.get(customerId);
            //CurrentCustomer.Name += " " + CurrentCustomer.Surname;
        }
    }
}
