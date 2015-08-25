using CommercialRecordSystem.Models.Goods;
using CommercialRecordSystem.ViewModels.DataVMs;
using System;

namespace CommercialRecordSystem.ViewModels.DataVMs.Goods
{
    class GoodVM : InfoDataVMBase<Good>
    {
        #region Properties
        public int firmId;
        public int FirmId
        {
            get
            {
                return firmId;
            }
            set
            {
                if (firmId != value)
                {
                    FirmVM firmBuff = new FirmVM();
                    firmBuff.get(value);
                    FirmName = firmBuff.Name;
                }

                firmId = value;
                RaisePropertyChanged("FirmId");

            }
        }

        public string firmName;
        public string FirmName
        {
            get
            {
                return firmName;
            }
            set
            {
                firmName = value;
                RaisePropertyChanged("FirmName");

            }
        }

        public int categoryId = 0;
        public int CategoryId
        {
            get
            {
                return categoryId;
            }

            set
            {
                if (categoryId != value)
                {
                    CategoryVM categoryBuff = new CategoryVM();
                    categoryBuff.get(value);
                    CategoryName = categoryBuff.Name;
                }

                categoryId = value;
                RaisePropertyChanged("CategoryId");
            }
        }

        public string categoryName;
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                RaisePropertyChanged("CategoryName");
            }
        }

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

        private int stockAmount;
        public int StockAmount
        {
            get
            {
                return stockAmount;
            }
            set
            {
                stockAmount = value;
                RaisePropertyChanged("StockAmount");
            }
        }

        private int unit;
        public int Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
                RaisePropertyChanged("Unit");
            }
        }

        private string detail;
        public string Detail
        {
            get
            {
                return detail;
            }
            set
            {
                detail = value;
                RaisePropertyChanged("Detail");

            }
        }

        private double cost;
        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
                RaisePropertyChanged("Cost");

            }
        }

        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                RaisePropertyChanged("Price");

            }
        }

        private string barcode;
        public string Barcode
        {
            get
            {
                return barcode;
            }
            set
            {
                barcode = value;
                RaisePropertyChanged("Barcode");

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
                createdDate = value;
                RaisePropertyChanged("CreatedDate");

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
                modifiedDate = value;
                RaisePropertyChanged("ModifiedDate");

            }
        }

        public GoodVM()
            : base(App.GoodImgFolder)
        { }

        public GoodVM(Good good)
            : base(good, App.GoodImgFolder)
        {
        }
        #endregion
    }
}
