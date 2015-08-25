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
                    CustomerVM customerBuff = new CustomerVM();
                    customerBuff.get(value);
                    CustomerName = customerBuff.Name + " " + customerBuff.Surname;
                }
                RaisePropertyChanged("CustomerId");
            }
        }

        private string customerName = string.Empty;
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
    }
}
