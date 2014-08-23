using System;

namespace CommercialRecordSystem.ViewModels
{
    class FirmVM : VMBase
    {
        #region Properties
        private int id;
        private string name;
        private string authorizedReseller;
        private string address;
        private string phone;
        private string mobile;
        private string pictureFileName;
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
