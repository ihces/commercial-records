using System.Linq;
using System.Threading.Tasks;
using CommercialRecordSystem.Models;
using System.Collections.ObjectModel;
using CommercialRecordSystem.ViewModels.Transacts;
using System;

namespace CommercialRecordSystem.ViewModels
{
    class SaleEntryVM : TransactEntryVMBase<SaleEntry>
    {
        #region Properties
        public DateTime modifyDate = DateTime.Now;
        public DateTime ModifyDate
        {
            get
            {
                return modifyDate;
            }
            set
            {
                modifyDate = value;
                RaisePropertyChanged("ModifyDate");
            }
        }

        public DateTime deliveryDate = DateTime.Now;
        public DateTime DeliveryDate
        {
            get
            {
                return deliveryDate;
            }
            set
            {
                deliveryDate = value;
                RaisePropertyChanged("DeliveryDate");
            }
        }

        public int goodId = 0;
        public int GoodId
        {
            get
            {
                return goodId;
            }
            set
            {
                goodId = value;
                RaisePropertyChanged("GoodId");
            }
        }
        
        public int orderState = 0;
        public int OrderState
        {
            get
            {
                return orderState;
            }
            set
            {
                orderState = value;
                RaisePropertyChanged("OrderState");
            }
        }

        private string moreDetail;
        public string MoreDetail
        {
            get
            {
                return moreDetail;
            }
            set
            {
                moreDetail = value;
                RaisePropertyChanged("MoreDetail");
            }
        }

        private double amount = 1;
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

        private string measure = "0";
        public string Measure
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
