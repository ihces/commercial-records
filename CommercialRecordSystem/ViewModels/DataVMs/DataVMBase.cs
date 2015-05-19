using System.Linq;
using System.Reflection;
using CommercialRecordSystem.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using SQLite;

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

        public DataVMBase(E model)
        {
            initWithModel(model);
        }

        public DataVMBase() 
        {
        
        }

        public static async Task<ObservableCollection<T>> getList<T>(
            Expression<Func<E, bool>> whereClause, List<Expression<Func<E, object>>> orderByClauses) where T: DataVMIntf<E>, new()
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

            resultList = await resultBuff.ToListAsync();

            /*if (0 == keyword.Trim().Length)
            {
                var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
                customerModelList = await db.Table<E>().Where(c => c.Type == Customer.TYPE.REGISTERED).OrderBy(c => c.Name).OrderBy(c => c.Surname).ToListAsync();
            }
            else
            {
                keyword = "%" + keyword + "%";
                var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
                customerModelList = await db.Table<E>().Where(c => c.Type == Customer.TYPE.REGISTERED && (c.Name.Contains(keyword) || c.Surname.Contains(keyword))).ToListAsync();
            }*/

            ObservableCollection<T> RecordList = new ObservableCollection<T>();
            
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
