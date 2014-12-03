using System.Linq;
using CommercialRecordSystem.Models;

namespace CommercialRecordSystem.ViewModels.DataVMs
{
    abstract class DataVMBase<E> : VMBase, DataVMIntf<E> where E : ModelBase, new()
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
        #endregion

        public abstract void Refresh();

        public abstract void initWithModel(E model);

        public abstract E convert2Model();

        public virtual int save()
        {
            E entryBuff = convert2Model();
            if (Id > 0)
                entryBuff.Id = Id;

            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                E existingEntry = (db.Table<E>().Where(
                        c => c.Id == Id)).SingleOrDefault();

                if (existingEntry != null)
                {
                    db.Update(entryBuff);
                }
                else
                {
                    db.Insert(entryBuff);
                    Id = entryBuff.Id;
                }
            }

            return Id;
        }

        public virtual DataVMIntf<E> get(int id)
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var entryBuff = (db.Table<E>().Where(
                    c => c.Id == id)).Single();

                if (null != entryBuff)
                {
                    initWithModel(entryBuff);
                }
            }
            return this;
        }

        public virtual DataVMIntf<E> delete()
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var existingEntry = (db.Table<E>().Where(
                    c => c.Id == Id)).Single();

                db.Delete(existingEntry);
            }

            return this;
        }
    }
}
