using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.ViewModels.Transacts;
using CommercialRecordSystem.Views.Transacts;

namespace CommercialRecordSystem.ViewModels
{
    class SaleFrameVM : TransactFrameVMBase<SaleEntryVM, SaleEntry>
    {
        #region Properties
        private readonly string header = "Satış";
        public string Header
        {
            get
            {
                return header;
            }
        }
        #endregion

        #region Command Handlers
        protected override void goNextCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(Payments), transactInfo);
        }
        #endregion

        public SaleFrameVM(Frame frame, FrameNavigation navigation)
            : base(frame, navigation)
        {
            if (transactInfo.Type.Equals(Transact.TYPE.ORDER))
            {
                header = "Sipariş";
            }
        }

        protected override void addEntryToListCmdHandler(object parameter)
        {
            EntryBuff.Cost = EntryBuff.Amount * EntryBuff.UnitCost;
            base.addEntryToListCmdHandler(parameter);
        }
    }
}