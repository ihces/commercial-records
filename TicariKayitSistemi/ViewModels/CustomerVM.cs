using System;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerVM : VMBase
    {
        #region Properties
        private int id;
        private string name;
        private string surname;
        private int sincerity;
        private string address;
        private string phoneNumber;
        private string mobileNumber;
        private string profilePhotoFileName;
        private DateTime lastTransactDate;
        private double accountCost;
        private DateTime createdDate;
        private DateTime modifiedDate;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname != value)
                {
                    surname = value;
                    RaisePropertyChanged("Surname");
                }
            }
        }

        public int Sincerity
        {
            get
            {
                return sincerity;
            }
            set
            {
                if (sincerity != value)
                {
                    sincerity = value;
                    RaisePropertyChanged("Sincerity");
                }
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (address != value)
                {
                    address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (phoneNumber != value)
                {
                    phoneNumber = value;
                    RaisePropertyChanged("PhoneNumber");
                }
            }
        }

        public string MobileNumber
        {
            get
            {
                return mobileNumber;
            }
            set
            {
                if (mobileNumber != value)
                {
                    mobileNumber = value;
                    RaisePropertyChanged("MobileNumber");
                }
            }
        }

        public string ProfilePhotoFileName
        {
            get
            {
                return profilePhotoFileName;
            }
            set
            {
                if (profilePhotoFileName != value)
                {
                    profilePhotoFileName = value;
                    RaisePropertyChanged("ProfilePhotoFileName");
                }
            }
        }

        public DateTime LastTransactDate
        {
            get
            {
                return lastTransactDate;
            }
            set
            {
                if (lastTransactDate != value)
                {
                    lastTransactDate = value;
                    RaisePropertyChanged("LastTransactDate");
                }
            }
        }

        public double AccountCost
        {
            get
            {
                return accountCost;
            }
            set
            {
                if (accountCost != value)
                {
                    accountCost = value;
                    RaisePropertyChanged("AccountCost");
                }
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                if (createdDate != value)
                {
                    createdDate = value;
                    RaisePropertyChanged("CreatedDate");
                }
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                if (modifiedDate != value)
                {
                    modifiedDate = value;
                    RaisePropertyChanged("ModifiedDate");
                }
            }
        }

        #endregion

        public CustomerVM getCustomer(int customerId) 
        {
            return null;

        }

        public string saveCustomer(CustomerVM customer)
        {
            return null;
        }

        public string deleteCustomer(int customerId)
        {
            return null;
        }
    }
}
