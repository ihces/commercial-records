
namespace CommercialRecordSystem.ViewModels
{
    class SaleVM : VMBase
    {
        #region Properties
        private int id;
        private int accountId;
        private int quantity;
        private int unitType;
        private string detail;
        private double unitCost;

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

        public int AccountId
        {
            get
            {
                return accountId;
            }
            set
            {
                if (accountId != value)
                {
                    accountId = value;
                    RaisePropertyChanged("AccountId");
                }
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    RaisePropertyChanged("Quantity");
                }
            }
        }

        public int UnitType
        {
            get
            {
                return unitType;
            }
            set
            {
                if (unitType != value)
                {
                    unitType = value;
                    RaisePropertyChanged("UnitType");
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

        public double UnitCost
        {
            get
            {
                return unitCost;
            }
            set
            {
                if (unitCost != value)
                {
                    unitCost = value;
                    RaisePropertyChanged("UnitCost");
                }
            }
        }

        #endregion
    }
}
