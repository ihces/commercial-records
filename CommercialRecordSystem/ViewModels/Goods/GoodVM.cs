using System;

namespace CommercialRecordSystem.ViewModels
{
    class GoodVM : VMBase
    {
        #region Properties
        public int id;
        public string name;
        public int stockAmount;
        public int unit;
        public string detail;
        public double cost;
        public double price;
        public string pictureFileName;
        public string barcode;
        public DateTime createdDate;
        public DateTime modifiedDate;

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

        public int StockAmount
        {
            get
            {
                return stockAmount;
            }
            set
            {
                if (stockAmount != value)
                {
                    stockAmount = value;
                    RaisePropertyChanged("StockAmount");
                }
            }
        }

        public int Unit
        {
            get
            {
                return unit;
            }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    RaisePropertyChanged("Unit");
                }
            }
        }

        public string Detail
        {
            get
            {
                return detail;
            }
            set
            {
                if (detail != value)
                {
                    detail = value;
                    RaisePropertyChanged("Detail");
                }
            }
        }

        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                if (cost != value)
                {
                    cost = value;
                    RaisePropertyChanged("Cost");
                }
            }
        }

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (price != value)
                {
                    price = value;
                    RaisePropertyChanged("Price");
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

        public string Barcode
        {
            get
            {
                return barcode;
            }
            set
            {
                if (barcode != value)
                {
                    barcode = value;
                    RaisePropertyChanged("Barcode");
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
