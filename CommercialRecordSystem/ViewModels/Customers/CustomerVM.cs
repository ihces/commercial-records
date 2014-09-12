using System;
using System.Linq;
using CommercialRecordSystem.Models;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerVM : VMBase
    {
        #region Properties
        private int id = 0;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
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

        private string profilePhotoFileName = string.Empty;
        public string ProfilePhotoFileName
        {
            get
            {
                return profilePhotoFileName;
            }
            set
            {
                profilePhotoFileName = value;
                RaisePropertyChanged("ProfilePhotoFileName");
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

        private Uri profileImgSource;
        public Uri ProfileImgSource
        {
            get
            {
                return profileImgSource;
            }
            set
            {
                profileImgSource = value;
                RaisePropertyChanged("ProfileImgSource");
            }
        }
        #endregion

        public CustomerVM()
        { }

        public CustomerVM(Customer model)
        {
            Id = model.Id;
            Name = model.Name;
            Surname = model.Surname;
            Address = model.Address;
            PhoneNumber = model.PhoneNumber;
            MobileNumber = model.MobileNumber;
            ProfilePhotoFileName = model.ProfilePhotoFileName;
            ProfileImgSource = new Uri(Path.Combine(App.ProfileImgFolder.Path, model.ProfilePhotoFileName));
        }

        #region Database Transactions
        public static CustomerVM get(int customerId)
        {
            CustomerVM customer = null;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var customerBuff = (db.Table<Customer>().Where(
                    c => c.Id == customerId)).Single();

                if (null != customerBuff)
                {
                    customer = new CustomerVM(customerBuff);
                }
            }
            return customer;
        }

        public async static Task<ObservableCollection<CustomerVM>> getCustomers(string keyword)
        {
            ObservableCollection<CustomerVM> CustomerList = new ObservableCollection<CustomerVM>();
            List<Customer> customerModelList = null;
            if (0 == keyword.Trim().Length) 
            {
                var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
                customerModelList = await db.Table<Customer>().OrderBy(c => c.Name).OrderBy(c => c.Surname).ToListAsync();
            }
            else
            {
                keyword = "%" + keyword + "%";
                var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
                customerModelList = await db.Table<Customer>().Where(c => c.Name.Contains(keyword) || c.Surname.Contains(keyword)).ToListAsync();
            }

            foreach (Customer customer in customerModelList)
            {
                CustomerVM customerBuff = new CustomerVM(customer);
                CustomerList.Add(customerBuff);
            }
            return CustomerList;
        }

        public static string save(CustomerVM customer)
        {
            Customer custModel = new Customer();
            //for updating customer via id
            if (customer.Id > 0)
            {
                custModel.Id = customer.Id;
            }
            custModel.Name = customer.Name;
            custModel.Surname = customer.Surname;
            custModel.Sincerity = customer.Sincerity;
            custModel.Address = customer.Address;
            custModel.PhoneNumber = customer.PhoneNumber;
            custModel.MobileNumber = customer.MobileNumber;
            custModel.ProfilePhotoFileName = customer.ProfilePhotoFileName;
            custModel.LastTransactDate = customer.LastTransactDate;
            custModel.AccountCost = customer.AccountCost;
            custModel.CreatedDate = customer.CreatedDate;
            custModel.ModifiedDate = customer.ModifiedDate;

            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                Customer existingCustomer = (db.Table<Customer>().Where(
                        c => c.Id == customer.Id)).SingleOrDefault();

                if (existingCustomer != null)
                {
                    db.Update(custModel);
                }
                else
                {
                    db.Insert(custModel);
                }
            }
            return "success";
        }

        public static string delete(int customerId)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var existingCustomer = (db.Table<Customer>().Where(
                    c => c.Id == customerId)).Single();

                db.Delete(existingCustomer);
            }

            return "success";
        }
        #endregion
    }
}
