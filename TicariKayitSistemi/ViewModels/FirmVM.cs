using System;
using SQLiteDemo.ViewModels;

namespace CommercialRecordSystem.ViewModels
{
    class FirmVM : VMBase
    {
        #region Properties
        private int id { get; set; }
        private string name { get; set; }
        private string authorizedReseller { get; set; }
        private string address { get; set; }
        private string phone { get; set; }
        private string mobile { get; set; }
        private string pictureFileName { get; set; }
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
        public string AuthorizedReseller
        {
            get
            {
                return authorizedReseller;
            }
            set
            {
                if (authorizedReseller != value)
                {
                    authorizedReseller = value;
                    RaisePropertyChanged("AuthorizedReseller");
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
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if (phone != value)
                {
                    phone = value;
                    RaisePropertyChanged("Phone");
                }
            }
        }
        public string Mobile 
        {
            get
            {
                return mobile;
            }
            set
            {
                if (mobile != value)
                {
                    mobile = value;
                    RaisePropertyChanged("Mobile");
                }
            }
        }
        public string PictureFileName 
        {
            get
            {
                return pictureFileName;
            }
            set
            {
                if (pictureFileName != value)
                {
                    pictureFileName = value;
                    RaisePropertyChanged("PictureFileName");
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
    }
}
