using CommercialRecordSystem.Models.Accounts;
using System;

namespace CommercialRecordSystem.ViewModels.DataVMs.Accounts
{
    abstract class AccountBaseVM<E> : DataVMBase<E> where E: AccountBase, new()
    {
        private string type;
        public string Type
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

        private double totalDept;
        public double TotalDept
        {
            get
            {
                return totalDept;
            }
            set
            {
                totalDept = value;
                RaisePropertyChanged("TotalDept", false);
                RaisePropertyChanged("RemainingDept", false);
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
                RaisePropertyChanged("RemainingDept", false);
            }
        }

        public double RemainingDept
        {
            get
            {
                double remainingDept = TotalDept - TotalCredit;
                if (double.Epsilon > Math.Abs(remainingDept))
                    Active = false;
                else
                    Active = true;

                return remainingDept;
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
