using CommercialRecords.Common;
using CommercialRecords.Models.Goods;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommercialRecords.ViewModels.Transacts;
using CommercialRecords.Views.Transacts;
using System;
using System.Linq.Expressions;
using CommercialRecords.ViewModels.DataVMs.Goods;
using CommercialRecords.Models;
using CommercialRecords.Models.Accounts;
using CommercialRecords.ViewModels.DataVMs.Accounts;
using CommercialRecords.Views.Accounts;

namespace CommercialRecords.ViewModels
{
    class SaleFrameVM : TransactFrameVMBase<SaleEntryVM, SaleEntry>
    {
        #region Properties
        private ActorVM unregisteredActor = new ActorVM() { Type = Actor.TYPE_PERSON, Registered = false };
        private ActorVM registeredActor = new ActorVM() { Type = Actor.TYPE_PERSON, Registered = true };

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

        private bool transactTypeAssigned = false;
        public bool TransactTypeAssigned
        {
            get
            {
                return transactTypeAssigned;
            }
            set
            {
                transactTypeAssigned = value;
                RaisePropertyChanged("TransactTypeAssigned");
            }
        }

        private bool currentActorInfoEditable = true;
        public bool CurrentActorInfoEditable
        {
            get
            {
                return currentActorInfoEditable;
            }
            set
            {
                currentActorInfoEditable = value;
                assignActorInfoReadOnly();

                RaisePropertyChanged("CurrentActorInfoEditable");
            }
        }

        private bool currentActorTypeEditable = true;
        public bool CurrentActorTypeEditable
        {
            get
            {
                return currentActorTypeEditable;
            }
            set
            {
                currentActorTypeEditable = value;
                RaisePropertyChanged("CurrentActorTypeEditable");
            }
        }

        private void assignActorInfoReadOnly()
        {
            if (CurrentActorInfoEditable && NON_REGISTERED == ActorRegistedIndex)
            {
                ActorInfoReadyOnly = false;
                CurrentActorTypeEditable = true;
            }
            else
            {
                ActorInfoReadyOnly = true;
                CurrentActorTypeEditable = false;
            }
        }

        private int transactTypeIndex = 0;
        public int TransactTypeIndex
        {
            get
            {
                return transactTypeIndex;
            }
            set
            {
                transactTypeIndex = value;
                TransactInfo.Type = value;
                ActorRegistedIndex = NON_REGISTERED;
                RaisePropertyChanged("TransactTypeIndex");
            }
        }

        private int actorRegistedIndex = NON_REGISTERED;
        public int ActorRegistedIndex
        {
            get
            {
                return actorRegistedIndex;
            }
            set
            {
                switch (value)
                {
                    case NON_REGISTERED:
                        if (CurrentActor.Registered)
                            registeredActor = CurrentActor;
                        CurrentActor = unregisteredActor;
                        CurrentActor.Refresh();
                        CurrentAccount = new CurrentAccountVM();
                        break;
                    case REGISTERED:
                        if (!CurrentActor.Registered)
                            unregisteredActor = CurrentActor;
                        CurrentActor = registeredActor;
                        CurrentActor.Refresh();
                        setAccounts();
                        break;
                }

                actorRegistedIndex = value;

                assignActorInfoReadOnly();

                RaisePropertyChanged("ActorRegistedIndex");
            }
        }

        private bool actorInfoReadyOnly = false;
        public bool ActorInfoReadyOnly
        {
            get
            {
                return actorInfoReadyOnly;
            }
            set
            {
                actorInfoReadyOnly = value;
                RaisePropertyChanged("ActorInfoReadyOnly");
            }
        }

        private string searchGoodInput = string.Empty;
        public string SearchGoodInput
        {
            get
            {
                return searchGoodInput;
            }
            set
            {
                searchGoodInput = value;
                RaisePropertyChanged("SearchGoodInput");
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

        private ObservableCollection<string> measures = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("measures"));
        public ObservableCollection<string> Measures
        {
            get
            {
                return measures;
            }
        }

        private ObservableCollection<string> transactTypes = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("transactTypes"));
        public ObservableCollection<string> TransactTypes
        {
            get
            {
                return transactTypes;
            }
        }

        private GoodVM selectedGood = null;
        public GoodVM SelectedGood
        {
            get
            {
                return selectedGood;
            }
            set
            {
                if (null == value)
                {
                    EntryBuff.Measure = "0";
                }
                else
                {
                    EntryBuff.Detail = value.Name;
                    EntryBuff.UnitCost = value.Price;
                    EntryBuff.Measure = value.Unit;
                }
                selectedGood = value;
                RaisePropertyChanged("SelectedGood");
            }
        }
        #endregion

        #region Commands
        private readonly ICommand selectRecordedActorCmd;
        public ICommand SelectRecordedActorCmd
        {
            get
            {
                return selectRecordedActorCmd;
            }
        }

        private readonly ICommand editActorAccountInfoCmd;
        public ICommand EditActorAccountInfoCmd
        {
            get
            {
                return editActorAccountInfoCmd;
            }
        }

        private readonly ICommand goodSearchBoxTextChangedCmd;
        public ICommand GoodSearchBoxTextChangedCmd
        {
            get
            {
                return goodSearchBoxTextChangedCmd;
            }
        }

        private readonly ICommand orderPreviousStateCmd;
        public ICommand OrderPreviousStateCmd
        {
            get
            {
                return orderPreviousStateCmd;
            }
        }

        private readonly ICommand orderNextStateCmd;
        public ICommand OrderNextStateCmd
        {
            get
            {
                return orderNextStateCmd;
            }
        }

        private bool foundGoodsVisible = false;
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

        private const int
            NON_REGISTERED = 0,
            REGISTERED = 1;
        #endregion

        #region Command Handlers
        private void selectRecordedActorCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(CurrentAccountList), TransactInfo.Type);
        }

        private void goodSearchBoxTextChangedCmdHandler(object parameter)
        {
            if (!string.IsNullOrWhiteSpace((string)parameter))
                findGoods(parameter.ToString());
            else
                FoundGoods = new ObservableCollection<GoodVM>();
        }

        private void editActorAccountInfoCmdHandler(object parameter)
        {
            if (CurrentActorInfoEditable)
            {
                if (CurrentActor.Registered && (!CurrentActor.Recorded || null == CurrentAccount))
                    ;
                else
                {
                    TransactTypeAssigned = true;

                    if (!CurrentActor.Registered)
                        CurrentActor.save();

                    if (TransactInfo.Recorded && CurrentAccount.Id != TransactInfo.AccountId)
                    {
                        CurrentAccountVM oldAcctInfo = new CurrentAccountVM();
                        oldAcctInfo.get(TransactInfo.AccountId);
                        if (oldAcctInfo.Recorded)
                        {
                            oldAcctInfo.TotalCredit -= TransactInfo.Paid;
                            oldAcctInfo.TotalDebt -= TransactInfo.Cost;
                            oldAcctInfo.save();
                        }

                        if (CurrentAccount.Recorded)
                        {
                            CurrentAccount.TotalCredit += TransactInfo.Paid;
                            CurrentAccount.TotalDebt += TransactInfo.Cost;

                            CurrentAccount.save();
                        }
                    }

                    TransactInfo.AccountId = CurrentAccount.Id;
                    TransactInfo.ActorId = CurrentActor.Id;

                    TransactInfo.save();
                    TransactInfo.Refresh();

                    CurrentActorInfoEditable = false;
                }
            }
            else
                CurrentActorInfoEditable = true;
        }

        protected override void goNextCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(Payments), TransactInfo);
        }

        private void orderNextStateCmdHandler(object obj)
        {
            int checkedCount = 0;
            int minOrderState = SelectedEntry.OrderState;
            foreach (SaleEntryVM entry in Entries)
            {
                if (entry.IsChecked)
                {
                    checkedCount++;
                    if (minOrderState > entry.OrderState)
                        minOrderState = entry.OrderState;
                }
            }

            if (minOrderState < 5)
            {
                minOrderState++;
                if (checkedCount > 1)
                {
                    foreach (SaleEntryVM entry in Entries)
                    {
                        entry.OrderState = minOrderState;
                        entry.ModifyDate = DateTime.Now;
                        if (minOrderState == 5)
                            entry.DeliveryDate = entry.ModifyDate;
                        entry.save();
                    }
                }
                else
                {
                    SelectedEntry.OrderState = minOrderState;
                    SelectedEntry.ModifyDate = DateTime.Now;
                    if (minOrderState == 5)
                        SelectedEntry.DeliveryDate = SelectedEntry.ModifyDate;
                    SelectedEntry.save();
                }
            }
        }

        private void orderPreviousStateCmdHandler(object obj)
        {
            int checkedCount = 0;
            int minOrderState = SelectedEntry.OrderState;
            foreach (SaleEntryVM entry in Entries)
            {
                if (entry.IsChecked)
                {
                    checkedCount++;
                    if (minOrderState > entry.OrderState)
                        minOrderState = entry.OrderState;
                }
            }

            if (minOrderState > 0)
            {
                minOrderState--;
                if (checkedCount > 1)
                {
                    foreach (SaleEntryVM entry in Entries)
                    {
                        entry.OrderState = minOrderState;
                        entry.ModifyDate = DateTime.Now;
                        entry.save();
                    }
                }
                else
                {
                    SelectedEntry.OrderState = minOrderState;
                    SelectedEntry.ModifyDate = DateTime.Now;
                    SelectedEntry.save();
                }
            }
        }
        #endregion

        public SaleFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            transactTypes.RemoveAt(3);
            RaisePropertyChanged("TransactTypes");

            if (TransactInfo.Recorded)
            {
                CurrentActorInfoEditable = false;
                TransactTypeAssigned = true;
            }

            //registered customer selected
            if (null != navigation.Forward && navigation.Forward.Is<CurrentAccountList>())
            {
                if (null != navigation.Message)
                {
                    CurrentActorInfoEditable = true;
                    CurrentActor.get((int)navigation.Message);
                    setAccounts();
                }
            }

            if (CurrentActor.Registered)
            {
                registeredActor = CurrentActor;
                ActorRegistedIndex = 1;
            }
            else
            {
                unregisteredActor = CurrentActor;
                ActorRegistedIndex = 0;
            }

            orderPreviousStateCmd = new ICommandImp(orderPreviousStateCmdHandler);
            orderNextStateCmd = new ICommandImp(orderNextStateCmdHandler);
            selectRecordedActorCmd = new ICommandImp(selectRecordedActorCmdHandler);
            editActorAccountInfoCmd = new ICommandImp(editActorAccountInfoCmdHandler);
            goodSearchBoxTextChangedCmd = new ICommandImp(goodSearchBoxTextChangedCmdHandler);
        }

        protected override void addEntryToListCmdHandler(object parameter)
        {
            EntryBuff.Cost = EntryBuff.Amount * EntryBuff.UnitCost;
            base.addEntryToListCmdHandler(parameter);
            SearchGoodInput = string.Empty;
        }

        //need review
        private async Task findGoods(string searchText)
        {
            List<Expression<Func<Good, object>>> orderByClauses = new List<Expression<Func<Good, object>>>();
            orderByClauses.Add(c => c.Name);
            string findBuff = '%' + searchText + '%';
            FoundGoods = new ObservableCollection<GoodVM>(
                await GoodVM.getList<GoodVM>(c => c.Name.ToLower().Contains(findBuff.ToLower()) || c.Barcode.Contains(findBuff),
                orderByClauses));

            if (FoundGoods.Count > 0)
                SelectedGood = FoundGoods[0];
            else
                SelectedGood = null;
        }

        protected async Task setAccounts()
        {
            List<Expression<Func<CurrentAccount, object>>> orderByClauses = null;
            orderByClauses = new List<Expression<Func<CurrentAccount, object>>>();
            orderByClauses.Add(c => c.Name);

            int typeBuff = TransactInfo.Type == 2?1:0;

            Accounts = new ObservableCollection<CurrentAccountVM>(
                await CurrentAccountVM.getList<CurrentAccountVM>(c => c.ActorId == registeredActor.Id && c.Type == typeBuff, orderByClauses));

            if (0 == TransactInfo.AccountId)
                CurrentAccount = Accounts[0];
            else if (0 < TransactInfo.AccountId)
            {
                foreach (CurrentAccountVM curAcc in Accounts)
                    if (curAcc.Id == TransactInfo.AccountId)
                    {
                        CurrentAccount = curAcc;
                        break;
                    }
            }

            CurrentAccount.Refresh();
        }
    }
}