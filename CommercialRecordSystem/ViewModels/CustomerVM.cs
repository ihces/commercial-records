using System;
using System.Linq;
using CommercialRecordSystem.Models;
using System.IO;

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

        #region Database Transactions
        public static CustomerVM getCustomer(int customerId)
        {
            CustomerVM customer = new CustomerVM();
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var customerBuff = (db.Table<Customer>().Where(
                    c => c.Id == customerId)).Single();

                customer.Id = customerBuff.Id;
                customer.Name = customerBuff.Name;
                customer.Surname = customerBuff.Surname;
                customer.Address = customerBuff.Address;
                customer.PhoneNumber = customerBuff.PhoneNumber;
                customer.MobileNumber = customerBuff.MobileNumber;
                customer.ProfilePhotoFileName = customerBuff.ProfilePhotoFileName;
                customer.ProfileImgSource = new Uri(Path.Combine(App.ProfileImgFolder.Path, customerBuff.ProfilePhotoFileName));
            }
            return customer;
        }

        public static string saveCustomer(Customer customer)
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                Customer existingCustomer = (db.Table<Customer>().Where(
                        c => c.Id == customer.Id)).SingleOrDefault();

                if (existingCustomer != null)
                {
                    db.Update(customer);
                }
                else
                {
                    db.Insert(customer);
                }
            }
            return "success";
        }

        public static string deleteCustomer(int customerId)
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
