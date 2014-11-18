using CommercialRecordSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CommercialRecordSystem.ViewModels
{
    class SaleEntryVM : VMBase
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

        private int transactId = 0;
        public int TransactId
        {
            get
            {
                return transactId;
            }
            set
            {
                transactId = value;
                RaisePropertyChanged("TransactId");
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

        private double amount = 0.0f;
        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        private int measure = 0;
        public int Measure
        {
            get
            {
                return measure;
            }
            set
            {
                measure = value;
                RaisePropertyChanged("Measure");
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

        private double unitCost = 0.0f;
        public double UnitCost
        {
            get
            {
                return unitCost;
            }
            set
            {
                unitCost = value;
                RaisePropertyChanged("UnitCost");
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

        public SaleEntryVM()
        {
        }

        public void Refresh()
        {
            RaisePropertyChanged("Id"); 
            RaisePropertyChanged("CustomerId");
            RaisePropertyChanged("TransactId");
            RaisePropertyChanged("Date");
            RaisePropertyChanged("Amount");
            RaisePropertyChanged("Measure");
            RaisePropertyChanged("Detail");
            RaisePropertyChanged("UnitCost");
            RaisePropertyChanged("Cost");
        }

        public SaleEntryVM(SaleEntry model)
        {
            Id = model.Id;
            CustomerId = model.CustomerId;
            TransactId = model.TransactId;
            Date = model.Date;
            Amount = model.Amount;
            Measure = model.Measure;
            Detail = model.Detail;
            UnitCost = model.UnitCost;
            Cost = model.Cost;
        }
        
        #region Database Transactions
        public static SaleEntryVM get(int entryId)
        {
            SaleEntryVM entry = null;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var entryBuff = (db.Table<SaleEntry>().Where(
                    c => c.Id == entryId)).Single();

                if (null != entryBuff)
                {
                    entry = new SaleEntryVM(entryBuff);
                }
            }
            return entry;
        }

        public static string save(SaleEntryVM entry)
        {
            SaleEntry entryBuff =new SaleEntry();
            if (entry.Id > 0)
                entryBuff.Id = entry.Id;
            entryBuff.Amount = entry.Amount;
            entryBuff.Measure = entry.Measure;
            entryBuff.Date = entry.Date;
            entryBuff.Detail = entry.Detail;
            entryBuff.UnitCost = entry.UnitCost;
            entryBuff.Cost = entry.UnitCost * entry.Amount;

            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                SaleEntry existingEntry = (db.Table<SaleEntry>().Where(
                        c => c.Id == entry.Id)).SingleOrDefault();

                if (existingEntry != null)
                {
                    db.Update(entryBuff);
                }
                else
                {
                    db.Insert(entryBuff);
                }
            }
            return "success";
        }

        public static string delete(int entryId)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var existingEntry = (db.Table<SaleEntry>().Where(
                    c => c.Id == entryId)).Single();

                db.Delete(existingEntry);
            }

            return "success";
        }

        public async static Task<ObservableCollection<SaleEntryVM>> getSaleEntries(int customerId, int transactId)
        {
            ObservableCollection<SaleEntryVM> Entries = new ObservableCollection<SaleEntryVM>();
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var saleEntryList = await db.Table<SaleEntry>()
                                .Where(e => customerId == e.CustomerId && transactId == e.TransactId)
                                .OrderBy(e => e.Id).ToListAsync();
            
            foreach (SaleEntry entry in saleEntryList)
            {
                SaleEntryVM entryBuff = new SaleEntryVM(entry);
                Entries.Add(entryBuff);
            }

            return Entries;
        }
        #endregion
    }
}
