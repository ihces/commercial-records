using CommercialRecordSystem.Models;
using CommercialRecordSystem.ViewModels.DataVMs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommercialRecordSystem.ViewModels
{
    class TransactVM : DataVMBase<Transact>
    {
        #region Properties
        private int customerId = 0;
        public int CustomerId
        {
            get
            {
                return customerId;
            }
            set
            {
                if (customerId != value)
                {
                    customerId = value;
                    ActorVM customerBuff = new ActorVM();
                    customerBuff.get(value);
                    CustomerName = customerBuff.Name + " " + customerBuff.Surname;
                }
                RaisePropertyChanged("CustomerId");
            }
        }

        private string customerName;
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
                RaisePropertyChanged("CustomerName");
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
