using CommercialRecordSystem.Models.Accounts.EnterpriseAccounts;
using System;

namespace CommercialRecordSystem.ViewModels.DataVMs.Accounts.EnterpriseAccounts
{
    class EnterpriseAccTransactVM : DataVMBase<EnterpriseAccTransact>
    {
        public int type;
        public int Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                RaisePropertyChanged("Type");
            }
        }

        public int transactId;
        public int TransactId
        {
            get
            {
                return transactId;
            }
            set
            {
                transactId = value;
                RaisePropertyChanged("TransactId");
            }
        }

        public int transactEntryId;
        public int TransactEntryId
        {
            get
            {
                return transactEntryId;
            }
            set
            {
                transactEntryId = value;
                RaisePropertyChanged("TransactEntryId");
            }
        }

        public int accountId;
        public int AccountId
        {
            get
            {
                return accountId;
            }
            set
            {
                accountId = value;
                RaisePropertyChanged("AccountId");
            }
        }

        public string detail;
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
        
        public double amount;
        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                RaisePropertyChanged("Date");
            }
        }
    }
}
