using System;
using CommercialRecordSystem.Models;

namespace CommercialRecordSystem.ViewModels
{
    class CurrentAccountVM : VMBase
    {
        #region Properties
        private int id;
        private int custumerId;
        private DateTime transactDate;
        private CurrentAccount.AccountType type;
        private double amount;

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

        public int CustumerId
        {
            get
            {
                return custumerId;
            }
            set
            {
                if (custumerId != value)
                {
                    custumerId = value;
                    RaisePropertyChanged("CustumerId");
                }
            }
        }

        public DateTime TransactDate
        {
            get
            {
                return transactDate;
            }
            set
            {
                if (transactDate != value)
                {
                    transactDate = value;
                    RaisePropertyChanged("TransactDate");
                }
            }
        }

        public CurrentAccount.AccountType Type
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

        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    RaisePropertyChanged("Amount");
                }
            }
        }
        #endregion
    }
}
