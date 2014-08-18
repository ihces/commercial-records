using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerVM
    {
        #region Properties
        private int id { get; set; }
        private string name { get; set; }
        private string surname { get; set; }
        private int sincerity { get; set; }
        private string address { get; set; }
        private string phoneNumber { get; set; }
        private string mobileNumber { get; set; }
        private string profilePhotoFileName { get; set; }
        private DateTime lastTransactDate { get; set; }
        private double accountCost { get; set; }
        private DateTime createdDate { get; set; }
        private DateTime modifiedDate { get; set; }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
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
                name = value;
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
                surname = value;
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
                sincerity = value;
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
                address = value;
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
                phoneNumber = value;
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
                mobileNumber = value;
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
                profilePhotoFileName = value;
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
                lastTransactDate = value;
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
                accountCost = value;
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
                createdDate = value;
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
                address = modifiedDate;
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
