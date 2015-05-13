using CommercialRecordSystem.Models.Transacts;
using CommercialRecordSystem.ViewModels.DataVMs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
namespace CommercialRecordSystem.ViewModels.Transacts
{
    abstract class TransactEntryVMBase<E> : DataVMBase<E> where E : TransactEntry, new()
    {
        #region Properties
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

        private bool isChecked = false;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }
        #endregion

        public TransactEntryVMBase()
        {
        }

        public TransactEntryVMBase(E model) : base(model)
        {
        }

        public static async Task<ObservableCollection<T>> getEntries<T>(int transactId) where T : TransactEntryVMBase<E>, new()
        {
            ObservableCollection<T> Entries = new ObservableCollection<T>();
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var saleEntryList = await db.Table<E>()
                                .Where(e => transactId == e.TransactId)
                                .OrderBy(e => e.Id).ToListAsync();

            foreach (E entry in saleEntryList)
            {
                T entryBuff = new T();
                entryBuff.initWithModel(entry);
                Entries.Add(entryBuff);
            }

            return Entries;
        }
    }
}
