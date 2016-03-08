using CommercialRecords.Models;
using CommercialRecords.ViewModels.DataVMs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommercialRecords.ViewModels
{
    class TransactVM : DataVMBase<Transact>
    {
        #region Properties
        private int actorId = 0;
        public int ActorId
        {
            get
            {
                return actorId;
            }
            set
            {
                if (actorId != value)
                {
                    actorId = value;
                    ActorVM actorBuff = new ActorVM();
                    actorBuff.get(value);
                    ActorName = actorBuff.Name + " " + actorBuff.Surname;
                }
                RaisePropertyChanged("ActorId");
            }
        }

        private string actorName;
        public string ActorName
        {
            get
            {
                return actorName;
            }
            set
            {
                actorName = value;
                RaisePropertyChanged("ActorName");
            }
        }

        private int accountId = 0;
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

        private int enterpriseAccId = 0;
        public int EnterpriseAccId
        { 
            get
            {
                return enterpriseAccId;
            }
            set
            {
                enterpriseAccId = value;
                RaisePropertyChanged("EnterpriseAccId");
            }
        }

        private int type = Transact.TYPE_SALE;
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

        private DateTime date = DateTime.Now;
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

        private DateTime modifyDate = DateTime.Now;
        public DateTime ModifyDate
        {
            get
            {
                return modifyDate;
            }
            set
            {
                modifyDate = value;
                RaisePropertyChanged("ModifyDate");
            }
        }

        private double cost = 0.0f;
        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
                RaisePropertyChanged("Cost");
                RaisePropertyChanged("RemainingCost");
            }
        }

        private double paid = 0.0f;
        public double Paid
        {
            get
            {
                return paid;
            }
            set
            {
                paid = value;
                RaisePropertyChanged("Paid");
                RaisePropertyChanged("RemainingCost");
            }
        }

        private int entryCount = 0;
        public int EntryCount
        {
            get
            {
                return entryCount;
            }
            set
            {
                entryCount = value;
                RaisePropertyChanged("EntryCount");
            }
        }

        public double RemainingCost
        {
            get
            {
                return Cost - Paid;
            }
        }
        #endregion

        #region Database Transactions

        public async Task<List<Transact>> getTransacts(int customerId)
        {
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var transactList = db.Table<Transact>()
                                .Where(t => accountId == t.AccountId)
                                .OrderBy(t => t.Date).ToListAsync();
            return await transactList;
        }
        #endregion

        public TransactVM()
        { }
        
        public TransactVM(Transact transact)
        {
            initWithModel(transact);
        }
    }
}
