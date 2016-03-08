using System;
using CommercialRecords.Common;
using CommercialRecords.Models.Goods;

namespace CommercialRecords.ViewModels.DataVMs.Goods
{
    class BrandVM : InfoDataVMBase<Brand>
    {
        #region Properties
        private string name;
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
                    name = UpperCaseFirst(value);
                    RaisePropertyChanged("Name");
                }
            }
        }

        private string authorizedReseller;
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

        private string address;
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

        private string phone;
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

        private string mobile;
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

        private DateTime createdDate;
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

        private DateTime modifiedDate;
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
        
        private FrameNavigation navigation = null;
        public FrameNavigation Navigation
        {
            get
            {
                return navigation;
            }
            set
            {
                navigation = value;
            }
        }

        public BrandVM(): base(App.FirmImgFolder)
        {
        }

        public BrandVM(Brand brand)
            : base(brand, App.FirmImgFolder)
        {
            
        }
        #endregion
    }
}
