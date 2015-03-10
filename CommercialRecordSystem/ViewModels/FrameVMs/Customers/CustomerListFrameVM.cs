using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.Views.Customers;
using CommercialRecordSystem.Views.Transacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerListFrameVM : FrameVMBase
    {
        #region Properties
        private readonly ICommand findCustomersCmd;
        public ICommand FindCustomersCmd
        {
            get
            {
                return findCustomersCmd;
            }
        }

        private readonly ICommand doOper4SelectedCustomerCmd;
        public ICommand DoOper4SelectedCustomerCmd
        {
            get
            {
                return doOper4SelectedCustomerCmd;
            }
        }

        private readonly ICommand addCustomerCmd;
        public ICommand AddCustomerCmd
        {
            get
            {
                return addCustomerCmd;
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

        private string queryText = string.Empty;
        public string QueryText
        {
            get
            {
                return queryText;
            }
            set
            {
                queryText = value;
                RaisePropertyChanged("QueryText");
            }
        }

        private ObservableCollection<CustomerVM> customers;
        public ObservableCollection<CustomerVM> Customers 
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        private CustomerVM selectedCustomer;
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

        private Int32 rowCount;
        public Int32 RowCount
        {
            get
            {
                return rowCount;
            }
            set
            {
                rowCount = value;
                RaisePropertyChanged("RowCount");
            }
        }

        private double totalAccount;
        public double TotalAccount
        {
            get
            {
                return totalAccount;
            }
            set
            {
                totalAccount = value;
                RaisePropertyChanged("TotalAccount");
            }
        }
        
        private Boolean noCustomerFound;
        public Boolean NoCustomerFound
        {
            get
            {
                return noCustomerFound;
            }
            set
            {
                noCustomerFound = value;
                RaisePropertyChanged("NoCustomerFound");
            }
        }
        
        #endregion "Properties"

        public CustomerListFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            findCustomersCmd = new ICommandImp(findCustomers_execute);
            doOper4SelectedCustomerCmd = new ICommandImp(doOper4SelectedCustomer_execute);
            addCustomerCmd = new ICommandImp(addCustomer_execute);
            editCustomerCmd = new ICommandImp(editCustomer_execute);
            setCustomers();
        }

        #region Command Method
        public void findCustomers_execute(object parameter)
        {
            setCustomers();
        }

        private void doOper4SelectedCustomer_execute(object obj)
        {
            if (Navigation.Back.PageType.Equals(typeof(TransactTypeSelector)))
            {
                Navigation.Navigate<TransactTypeSelector>(SelectedCustomer.Id);
            }
            else
            {
                Navigation.Navigate<CustomerAccount>(SelectedCustomer.Id);
            }
        }

        private void editCustomer_execute(object obj)
        {
            Navigation.Navigate(typeof(CustomerInfo), SelectedCustomer.Id);
        }

        private void addCustomer_execute(object obj)
        {
            Navigation.Navigate(typeof(CustomerInfo));
        }
        #endregion

        private async Task setCustomers()
        {
            Customers = await CustomerVM.getCustomers(QueryText);
            NoCustomerFound = true;
            if (0 < Customers.Count) 
            {
                NoCustomerFound = false;
            }
            RowCount = Customers.Count;

            double totalAccountBuff = 0.0;
            foreach (CustomerVM customerBuff in Customers)
            {
                totalAccountBuff += customerBuff.AccountCost;
            }
            TotalAccount = totalAccountBuff;
        }
    }
}
