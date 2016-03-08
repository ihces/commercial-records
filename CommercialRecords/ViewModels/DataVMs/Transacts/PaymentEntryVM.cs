using CommercialRecords.Models;

namespace CommercialRecords.ViewModels.Transacts
{
    class PaymentEntryVM : TransactEntryVMBase<PaymentEntry>
    {
        #region Properties
        private int type = 0;
        public int Type
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

        public PaymentEntryVM(PaymentEntry model) : base(model)
        {
            IsChecked = false;
        }
    }
}
