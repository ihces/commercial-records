using CommercialRecordSystem.Models;
using CommercialRecordSystem.ViewModels.DataVMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommercialRecordSystem.ViewModels
{
    class FirmVM : InfoDataVMBase<Firm>
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
                    name = value;
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

        private ObservableCollection<GoodVM> goods;
        public ObservableCollection<GoodVM> Goods
        {
            get
            {
                return goods;
            }
            set
            {
                goods = value;
                RaisePropertyChanged("Goods");
            }
        }

        public FirmVM(): base(App.FirmImgFolder)
        { }

        public FirmVM(Firm firm)
            : base(firm, App.FirmImgFolder)
        {
        }

        public async Task loadGoods()
        {
            List<Expression<Func<Good, object>>> orderByClauses = new List<Expression<Func<Good, object>>>();
            orderByClauses.Add(c => c.Name);
            orderByClauses.Add(c => c.CreatedDate);
            Goods = await DataVMBase<Good>.getList<GoodVM>(g => g.FirmId == Id, orderByClauses);
        }
        #endregion
    }
}
