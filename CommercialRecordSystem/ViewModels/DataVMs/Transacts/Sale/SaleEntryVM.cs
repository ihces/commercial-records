using System.Linq;
using System.Threading.Tasks;
using CommercialRecordSystem.Models;
using System.Collections.ObjectModel;
using CommercialRecordSystem.ViewModels.Transacts;

namespace CommercialRecordSystem.ViewModels
{
    class SaleEntryVM : TransactEntryVMBase<SaleEntry>
    {
        #region Properties
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
        #endregion

        public SaleEntryVM()
        {
        }

        public SaleEntryVM(SaleEntry model)
        {
            initWithModel(model);

        }

        public async static Task<ObservableCollection<SaleEntryVM>> getSaleEntries(int customerId, int transactId)
        {
            ObservableCollection<SaleEntryVM> Entries = new ObservableCollection<SaleEntryVM>();
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);
            var saleEntryList = await db.Table<SaleEntry>()
                                .Where(e => transactId == e.TransactId)
                                .OrderBy(e => e.Id).ToListAsync();
            
            foreach (SaleEntry entry in saleEntryList)
            {
                SaleEntryVM entryBuff = new SaleEntryVM(entry);
                Entries.Add(entryBuff);
            }

            return Entries;
        }

        public override void initWithModel(SaleEntry model)
        {
            Id = model.Id;
            TransactId = model.TransactId;
            Amount = model.Amount;
            Measure = model.Measure;
            Detail = model.Detail;
            UnitCost = model.UnitCost;
            Cost = model.Cost;
            IsChecked = false;
        }

        public override SaleEntry convert2Model()
        {
            SaleEntry model = new SaleEntry();
            model.Amount = Amount;
            model.Measure = Measure;
            model.Detail = Detail;
            model.UnitCost = UnitCost;
            model.Cost = Cost;

            return model;
        }

        public override void Refresh()
        {
            RaisePropertyChanged("Id"); 
            RaisePropertyChanged("TransactId");
            RaisePropertyChanged("Amount");
            RaisePropertyChanged("Measure");
            RaisePropertyChanged("Detail");
            RaisePropertyChanged("UnitCost");
            RaisePropertyChanged("Cost");
            RaisePropertyChanged("IsChecked");
        }
    }
}
