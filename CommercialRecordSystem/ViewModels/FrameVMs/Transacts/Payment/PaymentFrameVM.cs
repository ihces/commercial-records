using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
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
        #region Command Handlers
        protected override void goNextCmdHandler(object parameter)
        {

            Frame frameBuff = navigation.PageFrame;
            navigation = new FrameNavigation(typeof(MainPage));
            navigation.PageFrame = frameBuff;
           
            Navigation.Navigate(typeof(TransactTypeSelector), transactInfo);
        }
        #endregion

        public PaymentFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
        }
    }
}
