using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace CommercialRecordSystem.ViewModels
{
    class CustomersVM : VMBase
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

        public CustomersVM()
        {
            findCustomersCmd = new ICommandImp(findCustomers_execute);
            getAllCustomers();
        }

        #region Command Method
        public void findCustomers_execute(object parameter)
        {
            retrieveCustomers(queryText);
        }
        #endregion

        public async Task getAllCustomers()
        {
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var customerList = db.Table<Customer>().OrderBy(c => c.Name).OrderBy(c=>c.Surname).ToListAsync();

            setCustomers(await customerList);
        }

        public async Task retrieveCustomers(string keyword)
        {
            keyword = "%" + keyword + "%";
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var customerList = db.Table<Customer>().Where(c => c.Name.Contains(keyword) || c.Surname.Contains(keyword)).ToListAsync();

            setCustomers(await customerList);
        }

        private void setCustomers(List<Customer> customerList)
        {
            Customers = new ObservableCollection<CustomerVM>();
            foreach (var cust in customerList)
            {
                CustomerVM ct = new CustomerVM();
                ct.Id = cust.Id;
                ct.Name = cust.Name;
                ct.Surname = cust.Surname;
                ct.ProfileImgSource = new Uri(Path.Combine(App.ProfileImgFolder.Path, cust.ProfilePhotoFileName));
                Customers.Add(ct);
            }
        }
    }
}
