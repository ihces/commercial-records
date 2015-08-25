using System;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Common;
using System.Windows.Input;
using CommercialRecordSystem.Views.Customers;

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

        private readonly ICommand editCustomerCmd;
        public ICommand EditCustomerCmd
        {
            get
            {
                return editCustomerCmd;
            }
        }

        private void editCustomer_execute(object obj)
        {
            Navigation.Navigate(typeof(CustomerInfo), CurrentCustomer.Id);
        }

        #endregion

        public CustomerAccountFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            editCustomerCmd = new ICommandImp(editCustomer_execute);

            if (navigation.Forward == null) {
                CurrentCustomer.get((int)navigation.Message);
                CurrentCustomer.Name += " " + CurrentCustomer.Surname;
            }
        }
    }
}
