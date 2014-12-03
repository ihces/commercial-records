using System.Linq;
using System.Threading.Tasks;
using CommercialRecordSystem.Models;
using System.Collections.ObjectModel;

namespace CommercialRecordSystem.ViewModels.Transacts.Payment
{
    class PaymentEntryVM : TransactEntryVMBase<PaymentEntry>
    {
        #region Properties
        private PaymentEntry.TYPE type = PaymentEntry.TYPE.CASH;
        public PaymentEntry.TYPE Type
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
        #endregion

        public PaymentEntryVM()
        {
        }

        public override void Refresh()
        {
            RaisePropertyChanged("Id"); 
            RaisePropertyChanged("TransactId");
            RaisePropertyChanged("Detail");
            RaisePropertyChanged("Cost");
            RaisePropertyChanged("IsChecked");
        }

        public PaymentEntryVM(PaymentEntry model)
        {
            Id = model.Id;
            TransactId = model.TransactId;
            Type = model.Type;
            Detail = model.Detail;
            Cost = model.Cost;
            IsChecked = false;
        }
        
        public override void initWithModel(PaymentEntry model)
        {
            Id = model.Id;
            TransactId = model.TransactId;
            Type = model.Type;
            Detail = model.Detail;
            Cost = model.Cost;
        }

        public override PaymentEntry convert2Model()
        {
            PaymentEntry model = new PaymentEntry();
            model.Id = Id;
            model.TransactId = TransactId;
            model.Type = Type;
            model.Detail = Detail;
            model.Cost = Cost;

            return model;
        }
    }
}
