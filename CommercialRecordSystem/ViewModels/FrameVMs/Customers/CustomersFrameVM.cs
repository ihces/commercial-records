using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
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
    class CustomersFrameVM : FrameVMBase
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
        #endregion "Properties"

        public CustomersFrameVM(Frame frame)
            : base(frame)
        {
            findCustomersCmd = new ICommandImp(findCustomers_execute);
            setCustomers();
        }

        #region Command Method
        public void findCustomers_execute(object parameter)
        {
            setCustomers();
        }
        #endregion

        private async Task setCustomers()
        {
            Customers = await CustomerVM.getCustomers(QueryText);
        }
    }
}
