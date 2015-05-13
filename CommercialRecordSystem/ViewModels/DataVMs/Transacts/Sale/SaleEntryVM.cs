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

        public SaleEntryVM(SaleEntry model) : base(model)
        {
        }
    }
}
