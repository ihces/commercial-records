using CommercialRecordSystem.Models;
using CommercialRecordSystem.ViewModels.DataVMs;
using System;

namespace CommercialRecordSystem.ViewModels
{
    class GoodVM : InfoDataVMBase<Good>
    {
        #region Properties
        public int firmId;
        public int FirmId {
            get
            {
                return firmId;
            }
            set
            {
                if (firmId != value)
                {
                    firmId = value;
                    RaisePropertyChanged("FirmId");
                }
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
                    name = value;
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
                if (stockAmount != value)
                {
                    stockAmount = value;
                    RaisePropertyChanged("StockAmount");
                }
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
                if (unit != value)
                {
                    unit = value;
                    RaisePropertyChanged("Unit");
                }
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
                if (detail != value)
                {
                    detail = value;
                    RaisePropertyChanged("Detail");
                }
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
                if (cost != value)
                {
                    cost = value;
                    RaisePropertyChanged("Cost");
                }
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
                if (price != value)
                {
                    price = value;
                    RaisePropertyChanged("Price");
                }
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
                if (barcode != value)
                {
                    barcode = value;
                    RaisePropertyChanged("Barcode");
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

        public GoodVM(): base(App.GoodImgFolder)
        { }

        public GoodVM(Good good)
            : base(good, App.GoodImgFolder)
        {
        }
        #endregion
    }
}
