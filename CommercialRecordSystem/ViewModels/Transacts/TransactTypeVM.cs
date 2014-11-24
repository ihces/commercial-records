using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;

namespace CommercialRecordSystem.ViewModels
{
    class TransactTypeVM : FrameVMBase
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
            Navigate(typeof(Customers), Customers.OPEN_PURPOSE.ADD_TRANSACTION);
        }

        public override void GoBackFrame()
        {
            Navigate(typeof(MainPage));
        }

        private void startTransactionCmdHandler(object parameter)
        {
            transactInfo.Date = selectedDate;
            if (CurrentCustomer.Type.Equals(Customer.TYPE.UNREGISTERED) && 0 == transactInfo.CustomerId)
            {
                transactInfo.CustomerId = CustomerVM.save(CurrentCustomer);
            }

            switch (SelectedTransactTypeIndex)
            { 
                case SALE_TRANSACT:
                    transactInfo.Type = Transact.TYPE.SALE;
                    this.Navigate(typeof(Sales), transactInfo);
                    break;
                case ORDER_TRANSACT:
                    transactInfo.Type = Transact.TYPE.ORDER;
                    this.Navigate(typeof(Sales), transactInfo);
                    break;
                case PAYMENT_TRANSACT:
                    transactInfo.Type = Transact.TYPE.PAYMENT;
                    this.Navigate(typeof(Payments), transactInfo);
                    break;
            }
        }
        #endregion

        public TransactTypeVM(Frame frame, TransactVM transact):base(frame)
        {
            customerTypeSelectCmd = new ICommandImp(customerTypeSelectCmdHandler);
            selectRecordedCustomerCmd = new ICommandImp(selectRecordedCustomerCmdHandler);
            startTransactionCmd = new ICommandImp(startTransactionCmdHandler);

            this.transactInfo = transact;
            SelectedTransactTypeIndex = (int)transactInfo.Type -1;
            if (0 != transact.CustomerId)
            {
                currentCustomer = CustomerVM.get(transact.CustomerId);
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
}
