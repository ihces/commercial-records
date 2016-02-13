using System;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.ViewModels.DataVMs;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CommercialRecordSystem.ViewModels
{
    class ActorVM : InfoDataVMBase<Actor>
    {
        #region Properties
        private int type = Actor.TYPE_PERSON;
        public int Type 
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                if (type == Actor.TYPE_FIRM)
                    Surname = string.Empty;

                RaisePropertyChanged("Type");
            }
        }

        private string name = string.Empty;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = UpperCaseFirst(value);
                RaisePropertyChanged("Name");
            }
        }

        private string surname = string.Empty;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname =  UpperCaseFirst(value);
                RaisePropertyChanged("Surname");
            }
        }

        private bool registered = true;
        public bool Registered
        {
            get
            {
                return registered;
            }
            set
            {
                registered = value;
                RaisePropertyChanged("Registered");
            }
        }

        private int sincerity = 0;
        public int Sincerity
        {
            get
            {
                return sincerity;
            }
            set
            {
                sincerity = value;
                RaisePropertyChanged("Sincerity");
            }
        }

        private string address = string.Empty;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                RaisePropertyChanged("Address");
            }
        }

        private string detail = string.Empty;
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

        private string phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        private string mobileNumber = string.Empty;
        public string MobileNumber
        {
            get
            {
                return mobileNumber;
            }
            set
            {
                mobileNumber = value;
                RaisePropertyChanged("MobileNumber");
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
                RaisePropertyChanged("LastTransactDate");
            }
        }

        private double receivableAccTotal;
        public double ReceivableAccTotal
        {
            get
            {
                return receivableAccTotal;
            }
            set
            {
                receivableAccTotal = value;
                RaisePropertyChanged("ReceivableAccTotal");
            }
        }

        private double receivableAccPaid;
        public double ReceivableAccPaid
        {
            get
            {
                return receivableAccPaid;
            }
            set
            {
                receivableAccPaid = value;
                RaisePropertyChanged("ReceivableAccPaid");
            }
        }

        private double debtAcctTotal;
        public double DebtAcctTotal
        {
            get
            {
                return debtAcctTotal;
            }
            set
            {
                debtAcctTotal = value;
                RaisePropertyChanged("DebtAcctTotal");
            }
        }

        private double debtAcctPaid;
        public double DebtAcctPaid
        {
            get
            {
                return debtAcctPaid;
            }
            set
            {
                debtAcctPaid = value;
                RaisePropertyChanged("DebtAcctPaid");
            }
        }

        private int totalAccount;
        public int TotalAccount
        {
            get
            {
                return totalAccount;
            }
            set
            {
                totalAccount = value;
                RaisePropertyChanged("TotalAccount");
            }
        }

        private int activeAccNum;
        public int ActiveAccNum
        {
            get
            {
                return activeAccNum;
            }
            set
            {
                activeAccNum = value;
                RaisePropertyChanged("ActiveAccNum");
            }
        }

        public double RemainingCost
        {
            get
            {
                return (debtAcctTotal - debtAcctPaid) - (receivableAccTotal - receivableAccPaid);
            }
        }

        private double totalAcctForList;
        public double TotalAcctForList
        {
            get
            {
                return totalAcctForList;
            }
            set
            {
                totalAcctForList = value;
                RaisePropertyChanged("TotalAcctForList");
            }
        }

        private DateTime createdDate = DateTime.Now;
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

        private DateTime modifiedDate = DateTime.Now;
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

        #endregion

        public ActorVM(): base(App.ProfileImgFolder)
        { }

        public ActorVM(Actor actor)
            : base(actor, App.ProfileImgFolder)
        {
        }
    }
}
