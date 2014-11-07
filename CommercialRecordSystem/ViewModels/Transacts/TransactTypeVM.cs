using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Common;

namespace CommercialRecordSystem.ViewModels
{
    class TransactTypeVM : FrameVMBase
    {
        #region Properties
        private CustomerVM normalCustomer = new CustomerVM();
        private CustomerVM recordedCustomer = new CustomerVM();

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
                value.CopyTo(currentCustomer);
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

        private bool isRecordedCustomer = false;
        public bool IsRecordedCustomer
        {
            get
            {
                return isRecordedCustomer;
            }
            set
            {
                isRecordedCustomer = value;
                RaisePropertyChanged("IsRecordedCustomer");
            }
        }

        /*private readonly ICommand transactTypeSelectCmd;
        public ICommand TransactTypeSelectCmd
        {
            get
            {
                return transactTypeSelectCmd;
            }
        }*/

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

        public TransactTypeVM(Frame frame, int selectedCustId = 0):base(frame)
        {
            //transactTypeSelectCmd = new ICommandImp(transactTypeSelect_execute);
            customerTypeSelectCmd = new ICommandImp(customerTypeSelect_execute);
            selectRecordedCustomerCmd = new ICommandImp(selectRecordedCustomer_execute);
            startTransactionCmd = new ICommandImp(startTransaction_execute);

            if (0 != selectedCustId)
            {
                CustomerVM.get(selectedCustId).CopyTo(recordedCustomer);
                IsRecordedCustomer = true;
            }
        }

        private void transactTypeSelect_execute(object parameter)
        {
            switch((string)parameter)
            {
                case "sale":
                    break;
                case "order":
                    break;
                case "payment":
                    break;
            }
        }

        private void customerTypeSelect_execute(object parameter)
        {
            switch((string)parameter)
            {
                case "normal":
                    IsRecordedCustomer = false;
                    CurrentCustomer.CopyTo(recordedCustomer);
                    normalCustomer.CopyTo(CurrentCustomer);
                    break;
                case "recorded":
                    CurrentCustomer.CopyTo(normalCustomer);
                    recordedCustomer.CopyTo(CurrentCustomer);
                    IsRecordedCustomer = true;
                    break;
            }
        }

        private void selectRecordedCustomer_execute(object parameter)
        {
            Navigate(typeof(Customers), Customers.OPEN_PURPOSE.ADD_TRANSACTION);
        }

        private void startTransaction_execute(object parameter)
        {
            if (SelectedTransactTypeIndex == 2)
                this.Navigate(typeof(Payments), this);
            else
                this.Navigate(typeof(Sales), this);
        }    
    }
}
