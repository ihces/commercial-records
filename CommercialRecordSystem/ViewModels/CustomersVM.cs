using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

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
            Customers = new ObservableCollection<CustomerVM>();
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var customerList = db.Table<Customer>().OrderBy(c => c.Name).OrderBy(c=>c.Surname).ToListAsync();

            foreach (var cust in await customerList)
            {
                CustomerVM ct = new CustomerVM();
                ct.Name = cust.Name;
                Customers.Add(ct);
            }
        }

        public async Task retrieveCustomers(string keyword)
        {
            keyword = "%" + keyword + "%";
            Customers.Clear();
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var customerList = db.Table<Customer>().Where(c => c.Name.Contains(keyword) || c.Surname.Contains(keyword)).ToListAsync();
            
            foreach (var cust in await customerList)
            {
                CustomerVM ct = new CustomerVM();
                ct.Name = cust.Name;
                Customers.Add(ct);
            }

            //Customers = new ObservableCollection<CustomerVM>(await customerList);
        }




    }
}
