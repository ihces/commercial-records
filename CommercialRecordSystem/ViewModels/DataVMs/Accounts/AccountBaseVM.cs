using CommercialRecordSystem.Models.Accounts;
using System;

namespace CommercialRecordSystem.ViewModels.DataVMs.Accounts
{
    abstract class AccountBaseVM<E> : DataVMBase<E> where E: AccountBase, new()
    {
        private int type = 0;
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

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        private bool active = false;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                RaisePropertyChanged("Active", false);
            }
        }

        private DateTime createDate = DateTime.Now;
        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = value;
                RaisePropertyChanged("CreateDate", false);
            }
        }
        
        private DateTime lastTransactDate = DateTime.Now;
        public DateTime LastTransactDate
        {
            get
            {
                return lastTransactDate;
            }
            set
            {
                lastTransactDate = value;
                RaisePropertyChanged("LastTransactDate", false);
            }
        }

        private double totalDebt;
        public double TotalDebt
        {
            get
            {
                return totalDebt;
            }
            set
            {
                totalDebt = value;
                RaisePropertyChanged("TotalDebt", false);
                RaisePropertyChanged("RemainingDebt", false);
            }
        }

        private double totalCredit;
        public double TotalCredit
        {
            get
            {
                return totalCredit;
            }
            set
            {
                totalCredit = value;
                RaisePropertyChanged("TotalCredit", false);
                RaisePropertyChanged("RemainingDebt", false);
            }
        }

        public double RemainingDebt
        {
            get
            {
                double remainingDebt = TotalDebt - TotalCredit;
                if (double.Epsilon > Math.Abs(remainingDebt))
                    Active = false;
                else
                    Active = true;

                return remainingDebt;
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
    }
}
