using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.ViewModels.DataVMs.Accounts;
using CommercialRecordSystem.ViewModels.Transacts;
using CommercialRecordSystem.ViewModels.Transacts.Payment;
using CommercialRecordSystem.Views.Transacts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels
{
    class PaymentFrameVM : TransactFrameVMBase<PaymentEntryVM, PaymentEntry>
    {
        private ObservableCollection<string> paymentTypes = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("paymentTypes"));
        public ObservableCollection<string> PaymentTypes
        {
            get
            {
                return paymentTypes;
            }
        }

        #region Command Handlers
        private ObservableCollection<CurrentAccountVM> accounts;
        public ObservableCollection<CurrentAccountVM> Accounts
        {
            get
            {
                return accounts;
            }
            set
            {
                accounts = value;
                RaisePropertyChanged("Accounts");
            }
        }

        protected override void goNextCmdHandler(object parameter)
        {

            Frame frameBuff = navigation.PageFrame;
            navigation = new FrameNavigation(typeof(MainPage));
            navigation.PageFrame = frameBuff;
           
            Navigation.Navigate(typeof(TransactTypeSelector), TransactInfo);
        }
        #endregion

        public PaymentFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            if (CurrentActor.Registered)
            {
                Accounts = new ObservableCollection<CurrentAccountVM>();
                Accounts.Add(CurrentAccount);
            }
        }
    }
}
