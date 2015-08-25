using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
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
using CommercialRecordSystem.ViewModels.DataVMs.Goods;
using CommercialRecordSystem.Models;

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
                FoundGoodsVisible = 0 < foundGoods.Count ? true : false;
                RaisePropertyChanged("FoundGoods");
            }
        }

        private GoodVM selectedGood = new GoodVM();
        public GoodVM SelectedGood
        {
            get
            {
                return selectedGood;
            }
            set
            {
                selectedGood = value;
                RaisePropertyChanged("SelectedGood");
            }
        }

        /*private string saleEntityDetail;
        public string SaleEntityDetail
        {
            get
            {
                return saleEntityDetail;
            }
            set
            {
                saleEntityDetail = value;
                RaisePropertyChanged("SaleEntityDetail");
            }
        }*/

        private readonly ICommand detailTextChangedCmd;
        public ICommand DetailTextChangedCmd
        {
            get
            {
                return detailTextChangedCmd;
            }
        }

        private readonly ICommand selectGoodCmd;
        public ICommand SelectGoodCmd
        {
            get
            {
                return selectGoodCmd;
            }
        }
        
        private bool foundGoodsVisible = false;
        private bool searchGood = true;
        public bool FoundGoodsVisible
        {
            get
            {
                return foundGoodsVisible;
            }
            set
            {
                foundGoodsVisible = value;
                RaisePropertyChanged("FoundGoodsVisible");
            }
        }
        #endregion

        #region Command Handlers
        protected override void goNextCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(Payments), transactInfo);
        }

        private void detailTextChangedCmdHandler(object parameter)
        {
            string searchText = (string)parameter;
            if (searchGood)
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                    findGoods(searchText);
                else
                    FoundGoods = new ObservableCollection<GoodVM>();
            }
            else
            {
                searchGood = true;
            }
            
        }

        private void selectGoodCmdHandler(object parameter)
        {
            if (null != SelectedGood) {
                searchGood = false;
                EntryBuff.Detail = SelectedGood.Name;
                EntryBuff.UnitCost = SelectedGood.Price;
                FoundGoods = new ObservableCollection<GoodVM>();
            }
        }
        #endregion

        public SaleFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            if (transactInfo.Type.Equals(Transact.TYPE.ORDER))
            {
                header = "Sipariş";
            }

            selectGoodCmd = new ICommandImp(selectGoodCmdHandler);
            detailTextChangedCmd = new ICommandImp(detailTextChangedCmdHandler);
        }

        protected override void addEntryToListCmdHandler(object parameter)
        {
            EntryBuff.Cost = EntryBuff.Amount * EntryBuff.UnitCost;
            base.addEntryToListCmdHandler(parameter);
        }

        private async Task findGoods(string searchText)
        {
            List<Expression<Func<Good, object>>> orderByClauses = new List<Expression<Func<Good, object>>>();
            orderByClauses.Add(c => c.Name);
            string findBuff = '%' + searchText + '%';
            //FoundGoods = await GoodVM.getList<GoodVM>(c => c.Name.ToLower().Contains(findBuff.ToLower()), orderByClauses);
        }
    }
}