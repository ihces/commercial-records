using System;
using System.Linq;
using CommercialRecordSystem.Models;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommercialRecordSystem.ViewModels.DataVMs;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerVM : InfoDataVMBase<Customer>
    {
        #region Properties
        private Customer.TYPE type = Customer.TYPE.REGISTERED;
        public Customer.TYPE Type 
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                RaisePropertyChanged("Type");
            }
        }

        private string name = string.Empty;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string surname = string.Empty;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                RaisePropertyChanged("Surname");
            }
        }

        private int sincerity = 0;
        public int Sincerity
        {
            get
            {
                return sincerity;
            }
            set
            {
                sincerity = value;
                RaisePropertyChanged("Sincerity");
            }
        }

        private string address = string.Empty;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                RaisePropertyChanged("Address");
            }
        }

        private string phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        private string mobileNumber = string.Empty;
        public string MobileNumber
        {
            get
            {
                return mobileNumber;
            }
            set
            {
                mobileNumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }

        private DateTime lastTransactDate = DateTime.Now;
        public DateTime LastTransactDate
        {
            get
            {
                return lastTransactDate;
            }
            set
            {
                lastTransactDate = value;
                RaisePropertyChanged("LastTransactDate");
            }
        }

        private double accountCost = 0.0f;
        public double AccountCost
        {
            get
            {
                return accountCost;
            }
            set
            {
                accountCost = value;
                RaisePropertyChanged("AccountCost");
            }
        }

        private DateTime createdDate = DateTime.Now;
        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
                RaisePropertyChanged("CreatedDate");
            }
        }

        private DateTime modifiedDate = DateTime.Now;
        public DateTime ModifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                modifiedDate = value;
                RaisePropertyChanged("ModifiedDate");
            }
        }

        #endregion

        public CustomerVM(): base(App.ProfileImgFolder)
        { }

        public CustomerVM(Customer customer)
            : base(customer, App.ProfileImgFolder)
        {
        }

        #region Database Transactions
        public async static Task<ObservableCollection<CustomerVM>> getCustomers(string keyword)
        {
            ObservableCollection<CustomerVM> CustomerList = new ObservableCollection<CustomerVM>();
            List<Customer> customerModelList = null;
            if (0 == keyword.Trim().Length)
            {
                var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
                customerModelList = await db.Table<Customer>().Where(c => c.Type == Customer.TYPE.REGISTERED).OrderBy(c => c.Name).OrderBy(c => c.Surname).ToListAsync();
            }
            else
            {
                keyword = "%" + keyword + "%";
                var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
                customerModelList = await db.Table<Customer>().Where(c => c.Type == Customer.TYPE.REGISTERED && (c.Name.Contains(keyword) || c.Surname.Contains(keyword))).ToListAsync();
            }

            foreach (Customer customer in customerModelList)
            {
                CustomerVM customerBuff = new CustomerVM(customer);
                CustomerList.Add(customerBuff);
            }
            return CustomerList;
        }
        #endregion
    }
}
