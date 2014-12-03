using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.Views.Customers;
using CommercialRecordSystem.Views.Transacts;

namespace CommercialRecordSystem.ViewModels
{
    class TransactTypeFrameVM : FrameVMBase
    {
        #region Properties
        private CustomerVM unregisteredCustomer = new CustomerVM() { Type=Customer.TYPE.UNREGISTERED};
        private CustomerVM registeredCustomer = new CustomerVM() { Type = Customer.TYPE.REGISTERED };
        private TransactVM transactInfo = new TransactVM();

        public const int SALE_TRANSACT = 0;
        public const int ORDER_TRANSACT = 1;
        public const int PAYMENT_TRANSACT = 2;

        private int selectedTransactTypeIndex = 0;
        public int SelectedTransactTypeIndex
        {
            get
            {
                return selectedTransactTypeIndex;
            }
            set
            {
                selectedTransactTypeIndex = value;
                RaisePropertyChanged("SelectedTransactTypeIndex");
            }
        }

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

        private bool isRegisteredCustomer = false;
        public bool IsRegisteredCustomer
        {
            get
            {
                return isRegisteredCustomer;
            }
            set
            {
                isRegisteredCustomer = value;
                RaisePropertyChanged("IsRegisteredCustomer");
            }
        }
        #endregion
        #region Commands
        private readonly ICommand customerTypeSelectCmd;
        public ICommand CustomerTypeSelectCmd
        {
            get
            {
                return customerTypeSelectCmd;
            }
        }

        private readonly ICommand selectRecordedCustomerCmd;
        public ICommand SelectRecordedCustomerCmd
        {
            get
            {
                return selectRecordedCustomerCmd;
            }
        }

        private readonly ICommand startTransactionCmd;
        public ICommand StartTransactionCmd
        {
            get
            {
                return startTransactionCmd;
            }
        }
        
        #endregion
        #region Command Handlers
        private void customerTypeSelectCmdHandler(object parameter)
        {
            switch ((string)parameter)
            {
                case "unregistered":
                    IsRegisteredCustomer = false;
                    if (CurrentCustomer.Type.Equals(Customer.TYPE.REGISTERED))
                        registeredCustomer = CurrentCustomer;
                    CurrentCustomer = unregisteredCustomer;
                    CurrentCustomer.Refresh();
                    break;
                case "registered":
                    if (CurrentCustomer.Type.Equals(Customer.TYPE.UNREGISTERED))
                        unregisteredCustomer = CurrentCustomer;
                    CurrentCustomer = registeredCustomer;
                    CurrentCustomer.Refresh();
                    IsRegisteredCustomer = true;
                    break;
            }
        }

        private void selectRecordedCustomerCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(CustomerList), "Customers.OPEN_PURPOSE.ADD_TRANSACTION");
        }

        protected void GoBack()
        {
            Navigation.Navigate(typeof(MainPage));
        }

        private void startTransactionCmdHandler(object parameter)
        {
            if (CurrentCustomer.Type.Equals(Customer.TYPE.UNREGISTERED))
            {
                CurrentCustomer.save();
            }

            transactInfo.Date = selectedDate;
            transactInfo.CustomerId = CurrentCustomer.Id;

            switch (SelectedTransactTypeIndex)
            { 
                case SALE_TRANSACT:
                    transactInfo.Type = Transact.TYPE.SALE;
                    transactInfo.save();
                    Navigation.Navigate(typeof(Sales), transactInfo);
                    break;
                case ORDER_TRANSACT:
                    transactInfo.Type = Transact.TYPE.ORDER;
                    transactInfo.save();
                    Navigation.Navigate(typeof(Sales), transactInfo);
                    break;
                case PAYMENT_TRANSACT:
                    transactInfo.Type = Transact.TYPE.PAYMENT;
                    transactInfo.save();
                    Navigation.Navigate(typeof(Payments), transactInfo);
                    break;
            }
        }
        #endregion

        public TransactTypeFrameVM(Frame frame, FrameNavigation navigation)
            : base(frame, navigation)
        {
            customerTypeSelectCmd = new ICommandImp(customerTypeSelectCmdHandler);
            selectRecordedCustomerCmd = new ICommandImp(selectRecordedCustomerCmdHandler);
            startTransactionCmd = new ICommandImp(startTransactionCmdHandler);

            if (navigation.CanGoForward && (navigation.Forward.Is<Sales>() ||
                navigation.Forward.Is<Payments>()))
            {
                this.transactInfo = (TransactVM)navigation.Message;
                if (this.transactInfo != null)
                {
                    SelectedTransactTypeIndex = (int)transactInfo.Type - 1;
                    if (0 != transactInfo.CustomerId)
                    {
                        currentCustomer.get(transactInfo.CustomerId);
                        if (currentCustomer.Type.Equals(Customer.TYPE.REGISTERED))
                        {
                            registeredCustomer = currentCustomer;
                            IsRegisteredCustomer = true;
                        }
                        else
                        {
                            unregisteredCustomer = currentCustomer;
                            isRegisteredCustomer = false;
                        }
                        CurrentCustomer.Refresh();
                    }
                }
            }
            else
                transactInfo = new TransactVM();
        }
    }
}
