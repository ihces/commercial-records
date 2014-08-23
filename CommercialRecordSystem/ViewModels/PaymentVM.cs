using CommercialRecordSystem.Models;

namespace CommercialRecordSystem.ViewModels
{
    class PaymentVM : VMBase
    {
        #region Properties
        private int id;
        private int accountId;
        private Payment.PaymentType type;
        private string detail;
        private double cost;

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

        public Payment.PaymentType Type
        {
            get
            {
                return type;
            }
            set
            {
                if (type != value)
                {
                    type = value;
                    RaisePropertyChanged("Type");
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

        #endregion
    }
}
