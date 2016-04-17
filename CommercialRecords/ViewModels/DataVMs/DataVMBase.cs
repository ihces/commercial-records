using System.Linq;
using System.Reflection;
using CommercialRecords.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using SQLite;
using CommercialRecords.DataLayer;

namespace CommercialRecords.ViewModels.DataVMs
{
    public abstract class DataVMBase<E> : VMBase, DataVMIntf<E> where E : ModelBase, new()
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
                RaisePropertyChanged("Id", false);
                RaisePropertyChanged("Recorded", false);
            }
        }
        #endregion

        public bool Recorded
        {
            get
            {
                if (Id > 0)
                    return true;
                else
                    return false;
            }
        }

        public void Refresh()
        {
            IEnumerable<PropertyInfo> properties = this.GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                RaisePropertyChanged(property.Name);
            }
        }

        public virtual void initWithModel(E model)
        {
            IEnumerable<PropertyInfo> modelProperties = model.GetType().GetRuntimeProperties();
            IEnumerable<PropertyInfo> VMproperties = this.GetType().GetRuntimeProperties();

            foreach (PropertyInfo modelProperty in modelProperties)
            {
                PropertyInfo property = VMproperties.Where(p => p.Name == modelProperty.Name).Single();
                if (null != property)
                    property.SetValue(this, modelProperty.GetValue(model));
            }

            Dirty = false;
        }

        public virtual E convert2Model()
        {
            E model = new E();
            IEnumerable<PropertyInfo> modelProperties = model.GetType().GetRuntimeProperties();
            IEnumerable<PropertyInfo> VMproperties = this.GetType().GetRuntimeProperties();

            foreach (PropertyInfo modelProperty in modelProperties)
            {
                PropertyInfo property = VMproperties.Where(p => p.Name == modelProperty.Name).Single();
                if (null != property)
                    modelProperty.SetValue(model, property.GetValue(this));
            }

            return model;
        }

        public virtual int save()
        {
            E entryBuff = convert2Model();
            if (Id > 0)
                entryBuff.Id = Id;

            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                db.BeginTransaction();

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

                db.Commit();

                Dirty = false;
            }

            return Id;
        }

        public virtual DataVMIntf<E> get(int id)
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var entryBuff = (db.Table<E>().Where(
                    c => c.Id == id)).SingleOrDefault();

                if (null != entryBuff)
                {
                    initWithModel(entryBuff);
                }
            }
            return this;
        }

        public virtual DataVMIntf<E> delete()
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                db.BeginTransaction();
                var existingEntry = (db.Table<E>().Where(
                    c => c.Id == Id)).Single();

                db.Delete(existingEntry);

                db.Commit();
            }

            return this;
        }

        public DataVMBase(E model)
        {
            initWithModel(model);
        }

        public DataVMBase()
        {

        }

        public static async Task<List<T>> getList<T>(
            Expression<Func<E, bool>> whereClause = null, List<Expression<Func<E, object>>> orderByClauses = null) where T : DataVMIntf<E>, new()
        {
            List<E> resultList = null;

            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);

            AsyncTableQuery<E> resultBuff = db.Table<E>();

            if (null != whereClause)
            {
                resultBuff = resultBuff.Where(whereClause);
            }

            if (null != orderByClauses)
            {
                foreach (Expression<Func<E, object>> orderBy in orderByClauses)
                {
                    resultBuff = resultBuff.OrderBy(orderBy);
                }
            }

            try
            {
                resultList = await resultBuff.ToListAsync();
            }
            catch (Exception )
            {

            }

            return converToVM<T>(resultList);
        }

        public static async Task<List<T>> getList<T>(string query, params object [] args) where T : DataVMIntf<E>, new()
        {
            List<E> resultList = null;

            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);

            string scriptBuff = string.Empty;
            if (!string.IsNullOrEmpty(query))
                resultList = await db.QueryAsync<E>(string.Format((await ScriptLoader.getInstance()).getScript(query, args)));

            return converToVM<T>(resultList);
        }

        private static List<T> converToVM<T>(List<E> resultList) where T : DataVMIntf<E>, new()
        {
            List<T> RecordList = new List<T>();

            foreach (E record in resultList)
            {
                T recordBuff = new T();
                recordBuff.initWithModel(record);
                RecordList.Add(recordBuff);
            }
            return RecordList;
        }
    }
}
