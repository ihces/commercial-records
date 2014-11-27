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
        public static TransactVM get(int transactId)
        {
            TransactVM transact = null;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var entryBuff = (db.Table<Transact>().Where(
                    c => c.Id == transactId)).Single();

                if (null != entryBuff)
                {
                    transact = new TransactVM(entryBuff);
                }
            }
            return transact;
        }

        public static int save(Transact transact)
        {
            int id = transact.Id;
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
                    id = db.Insert(transact);
                }
            }
            return id;
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

        public TransactVM(Transact transact)
        {
            Id = transact.Id;
            Type = transact.Type;
            CustomerId = transact.CustomerId;
            Date = transact.Date;
            Cost = transact.Cost;
        }

        public TransactVM()
        {

        }
    }
}
