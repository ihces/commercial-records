using System;
using System.Linq;
using SQLite;
using CommercialRecordSystem.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CommercialRecordSystem.ViewModels
{
    class TransactVM : VMBase
    {
        #region Properties
        private int id = 0;
        public int Id
        { 
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
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

        private Transact.TRANSACT_TYPE type = Transact.TRANSACT_TYPE.SALE;
        public Transact.TRANSACT_TYPE Type
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
        public static TransactVM get(int transactId)
        {
            TransactVM transact = null;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var entryBuff = (db.Table<Transact>().Where(
                    c => c.Id == transactId)).Single();

                if (null != entryBuff)
                {
                    transact = new TransactVM();
                }
            }
            return transact;
        }

        public static string save(Transact transact)
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                SaleEntry existingTransact = (db.Table<SaleEntry>().Where(
                        c => c.Id == transact.Id)).SingleOrDefault();

                if (existingTransact != null)
                {
                    db.Update(transact);
                }
                else
                {
                    db.Insert(transact);
                }
            }
            return "success";
        }

        public static string delete(int transactId)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var existingTransact = (db.Table<Transact>().Where(
                    c => c.Id == transactId)).Single();

                db.Delete(existingTransact);
            }

            return "success";
        }

        public async Task<List<Transact>> getTransacts(int customerId)
        {
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var transactList = db.Table<Transact>()
                                .Where(t => customerId == t.CustomerId)
                                .OrderBy(t => t.Date).ToListAsync();
            return await transactList;
        }
        #endregion
    }
}
