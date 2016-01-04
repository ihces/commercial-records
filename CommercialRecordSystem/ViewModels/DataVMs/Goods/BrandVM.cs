using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
using CommercialRecordSystem.ViewModels.DataVMs;
using CommercialRecordSystem.Views;

namespace CommercialRecordSystem.ViewModels.DataVMs.Goods
{
    class BrandVM : InfoDataVMBase<Firm>
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
        /*
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
        */
        
        
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
        /*
        private ICommand addGoodCmd = null;
        public ICommand AddGoodCmd
        {
            get
            {
                return addGoodCmd;
            }
        }

        private ICommand editGoodCmd = null;
        public ICommand EditGoodCmd
        {
            get
            {
                return editGoodCmd;
            }
        }

        private Boolean showGoodList = false;
        public Boolean ShowGoodList
        {
            get
            {
                return showGoodList;
            }
            set
            {
                showGoodList = value;
                ColumnSpan = showGoodList ? 5 : 2;
                RaisePropertyChanged("ShowGoodList");
            }
        }

        private int columnSpan = 2;
        public int ColumnSpan
        {
            get
            {
                return columnSpan;
            }
            set
            {
                columnSpan = value;
                RaisePropertyChanged("ColumnSpan");
            }
        }*/

        public BrandVM(): base(App.FirmImgFolder)
        {
            /*addGoodCmd = new ICommandImp(addGoodCmdHandler);
            editGoodCmd = new ICommandImp(editGoodCmdHandler);*/
        }

        public BrandVM(Firm firm)
            : base(firm, App.FirmImgFolder)
        {
            
        }
        /*
        public async Task loadGoods()
        {
            List<Expression<Func<Good, object>>> orderByClauses = new List<Expression<Func<Good, object>>>();
            orderByClauses.Add(c => c.Name);
            orderByClauses.Add(c => c.CreatedDate);

            Goods = await DataVMBase<Good>.getList<GoodVM>(g => g.FirmId == Id, orderByClauses);
        }

        private void addGoodCmdHandler(object parameter)
        {
            Int32 [] Ids = new Int32[2];
            Ids[0] = Id;
            Ids[1] = 0;
            navigation.Navigate<GoodInfo>(Ids);
        }

        private void editGoodCmdHandler(object parameter)
        {
            Int32[] Ids = new Int32[2];
            Ids[0] = Id;
            Ids[1] = 0;
            navigation.Navigate<GoodInfo>(Ids);
        }*/
        #endregion
    }
}
