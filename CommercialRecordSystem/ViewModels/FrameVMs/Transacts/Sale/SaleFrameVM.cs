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
using System;
using System.Linq.Expressions;

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

        private ObservableCollection<GoodVM> foundGoods = new ObservableCollection<GoodVM>();
        public ObservableCollection<GoodVM> FoundGoods
        {
            get
            {
                return foundGoods;
            }
            set
            {
                foundGoods = value;
                RaisePropertyChanged("FoundGoods");
            }
        }

        private String queryText = "";
        public String QueryText
        {
            get
            {
                return queryText;
            }
            set
            {
                queryText = value;
                findGoods();
                RaisePropertyChanged("QueryText");
            }
        }

        private readonly ICommand closeGoodsViewCmd;
        public ICommand CloseGoodsViewCmd
        {
            get
            {
                return closeGoodsViewCmd;
            }
        }

        private bool showGoodsView = false;
        public bool ShowGoodsView
        {
            get
            {
                return showGoodsView;
            }
            set
            {
                showGoodsView = value;
                RaisePropertyChanged("ShowGoodsView");
            }
        }
        #endregion

        #region Command Handlers
        protected override void goNextCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(Payments), transactInfo);
        }
        #endregion

        public SaleFrameVM(FrameNavigation navigation)
            : base(navigation)
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

        private async Task findGoods()
        {
            List<Expression<Func<Good, object>>> orderByClauses = new List<Expression<Func<Good, object>>>();
            orderByClauses.Add(c => c.Name);
            string findBuff = '%' + QueryText + '%';
            FoundGoods = await GoodVM.getList<GoodVM>(c => c.Name.ToLower().Contains(findBuff.ToLower()), orderByClauses);
        }
    }
}