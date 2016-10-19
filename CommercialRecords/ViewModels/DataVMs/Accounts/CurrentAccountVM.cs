using CommercialRecords.Common;
using CommercialRecords.Models.Accounts;
using System;
using System.Linq;

namespace CommercialRecords.ViewModels.DataVMs.Accounts
{
    class CurrentAccountVM : AccountBaseVM<CurrentAccount>
    {
        public const int TYPE_RECEIVABLE= 0, TYPE_DEBT=1;
        private int actorId = 0;
        public int ActorId {
            get
            {
                return actorId;
            }
            set 
            {
                actorId = value;
                RaisePropertyChanged("ActorId", false);
            }
        }

        private int initialAmountType = 0;
        public int InitialAmountType
        {
            get
            {
                return initialAmountType;
            }
            set
            {
                initialAmountType = value;
                RaisePropertyChanged("InitialAmountType", false);
            }
        }

        private double initialAmount = 0;
        public double InitialAmount
        {
            get
            {
                return initialAmount;
            }
            set
            {
                initialAmount = value;
                RaisePropertyChanged("InitialAmount", false);
            }
        }

        public CurrentAccountVM() 
        {
            Type = TYPE_DEBT;
            Dirty = false;
        }
    }
}
