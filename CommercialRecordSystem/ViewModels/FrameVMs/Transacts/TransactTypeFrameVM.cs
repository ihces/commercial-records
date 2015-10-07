﻿using System;
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
        private CustomerVM unregisteredCustomer = new CustomerVM() { Type = Customer.TYPE.UNREGISTERED };
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

        private int selectedCustomerType = 0;
        public int SelectedCustomerType
        {
            get
            {
                return selectedCustomerType;
            }
            set
            {
                if (value != selectedCustomerType)
                {
                    selectedCustomerType = value;
                    switch (value)
                    {
                        case 0:
                            IsRegisteredCustomer = false;
                            if (CurrentCustomer.Type.Equals(Customer.TYPE.REGISTERED))
                                registeredCustomer = CurrentCustomer;
                            CurrentCustomer = unregisteredCustomer;
                            CurrentCustomer.Refresh();
                            break;
                        case 1:
                            if (CurrentCustomer.Type.Equals(Customer.TYPE.UNREGISTERED))
                                unregisteredCustomer = CurrentCustomer;
                            CurrentCustomer = registeredCustomer;
                            CurrentCustomer.Refresh();
                            IsRegisteredCustomer = true;
                            break;
                    }
                }

                RaisePropertyChanged("SelectedCustomerType");
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
        private void selectRecordedCustomerCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(CustomerList));
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

        public TransactTypeFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            selectRecordedCustomerCmd = new ICommandImp(selectRecordedCustomerCmdHandler);
            startTransactionCmd = new ICommandImp(startTransactionCmdHandler);

            if (navigation.CanGoForward)
            {
                if (navigation.Forward.Is<Sales>() ||
                    navigation.Forward.Is<Payments>())
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
                        }
                    }
                }
                else if (navigation.Forward.Is<CustomerList>())
                {
                    if (null != navigation.Message)
                    {
                        registeredCustomer.get((int)navigation.Message);
                        CurrentCustomer = registeredCustomer;
                        CurrentCustomer.Refresh();
                    }
                }
            }
            else
                transactInfo = new TransactVM();
        }
    }
}
