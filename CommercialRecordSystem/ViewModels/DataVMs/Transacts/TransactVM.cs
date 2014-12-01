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
        private int parentId = 0;
        public int ParentId
        {
            get
            {
                return parentId;
            }
            set
            {
                parentId = value;
                RaisePropertyChanged("ParentId");
            }
        }

        private int customerId = 0;
        public int CustomerId
        { 
            get
            {
                return customerId;
            }
            set
            {
                customerId = value;
                RaisePropertyChanged("CustomerId");
            }
        }

        private Transact.TYPE type = Transact.TYPE.SALE;
        public Transact.TYPE Type
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
            }
        }
        #endregion

        #region Database Transactions

        public async Task<List<Transact>> getTransacts(int customerId)
        {
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var transactList = db.Table<Transact>()
                                .Where(t => customerId == t.CustomerId)
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

        public override void Refresh()
        {
            RaisePropertyChanged("Id");
            RaisePropertyChanged("ParentId");
            RaisePropertyChanged("CustomerId");
            RaisePropertyChanged("Date");
            RaisePropertyChanged("Cost");
        }

        public override void initWithModel(Transact model)
        {
            Id = model.Id;
            ParentId = model.ParentId;
            CustomerId = model.CustomerId;
            Date = model.Date;
            Cost = model.Cost;
            Dirty = false;
        }

        public override Transact convert2Model()
        {
            Transact transact = new Transact();
            transact.Id = Id;
            transact.ParentId = ParentId;
            transact.CustomerId = CustomerId;
            transact.Date = Date;
            transact.Cost = Cost;

            return transact;
        }
    }
}
