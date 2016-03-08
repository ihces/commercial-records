using CommercialRecords.Models.Goods;
using System;

namespace CommercialRecords.ViewModels.DataVMs.Goods
{
    class GoodVM : InfoDataVMBase<Good>
    {
        #region Properties
        public int brandId = 0;
        public int BrandId
        {
            get
            {
                return brandId;
            }
            set
            {
                if (brandId != value)
                {
                    BrandVM brandBuff = new BrandVM();
                    brandBuff.get(value);
                    BrandName = brandBuff.Name;
                }

                brandId = value;
                RaisePropertyChanged("BrandId");

            }
        }

        public string brandName;
        public string BrandName
        {
            get
            {
                return brandName;
            }
            set
            {
                brandName = value;
                RaisePropertyChanged("BrandName");

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

        private string unit = "0";
        public string Unit
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

        private double vat = 18.0f;
        public double Vat
        {
            get
            {
                return vat;
            }
            set
            {
                vat = value;
                RaisePropertyChanged("Vat");

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
