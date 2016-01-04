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
using CommercialRecordSystem.Models.Accounts;
using CommercialRecordSystem.ViewModels.DataVMs.Accounts;
using CommercialRecordSystem.Views.Accounts;

namespace CommercialRecordSystem.ViewModels
{
    class SaleFrameVM : TransactFrameVMBase<SaleEntryVM, SaleEntry>
    {
        #region Properties
        private ActorVM unregisteredActor = new ActorVM() { Type = Actor.TYPE_PERSON, Registered = false };
        private ActorVM registeredActor = new ActorVM() { Type = Actor.TYPE_PERSON, Registered = false };

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
                //assign actor info fields should be readonly or not.
                ActorRegistedIndex = ActorRegistedIndex;

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

        private int actorRegistedIndex = NON_REGISTERED;
        public int ActorRegistedIndex
        {
            get
            {
                return actorRegistedIndex;
            }
            set
            {
                if (CurrentActorInfoEditable && NON_REGISTERED == value)
                {
                    ActorInfoReadyOnly = false;
                    CurrentActorTypeEditable = true;
                }
                else
                {
                    ActorInfoReadyOnly = true;
                    CurrentActorTypeEditable = false;
                }

                if (value != actorRegistedIndex)
                {
                    switch (value)
                    {
                        case NON_REGISTERED:
                            if (CurrentActor.Registered)
                                registeredActor = CurrentActor;
                            CurrentActor = unregisteredActor;
                            CurrentActor.Refresh();
                            break;
                        case REGISTERED:
                            if (!CurrentActor.Registered)
                                unregisteredActor = CurrentActor;
                            CurrentActor = registeredActor;
                            CurrentActor.Refresh();
                            setAccounts();
                            break;
                    }
                }

                actorRegistedIndex = value;
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
                else{
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

        private readonly ICommand selectGoodCmd;
        public ICommand SelectGoodCmd
        {
            get
            {
                return selectGoodCmd;
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
            Navigation.Navigate(typeof(CurrentAccountList));
        }

        private void goodSearchBoxTextChangedCmdHandler(object parameter)
        {

            if (null != parameter)
            {
                string searchBoxInputBuff = parameter.ToString();
                if (!string.IsNullOrWhiteSpace(searchBoxInputBuff))
                    findGoods(searchBoxInputBuff);
                else
                    FoundGoods = new ObservableCollection<GoodVM>();
            }
        }

        private void editActorAccountInfoCmdHandler(object parameter)
        {
            if (CurrentActorInfoEditable)
            {
                if (CurrentActor.Registered && !CurrentActor.Recorded)
                    ;
                else
                {
                    TransactTypeAssigned = true;
                    if (!CurrentActor.Registered)
                        CurrentActor.save();
                    else
                    {
                        CurrentAccount.get(CurrentAccount.Id);
                        TransactInfo.AccountId = CurrentAccount.Id;
                    }

                    TransactInfo.CustomerId = CurrentActor.Id;

                    TransactInfo.save();


                    CurrentActor.Refresh();
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
                        entry.save();
                    }
                }
                else
                {
                    SelectedEntry.OrderState = minOrderState;
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
                        entry.save();
                    }
                }
                else
                {
                    SelectedEntry.OrderState = minOrderState;
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

            Accounts = new ObservableCollection<CurrentAccountVM>(
                await CurrentAccountVM.getList<CurrentAccountVM>(c => c.ActorId == registeredActor.Id, orderByClauses));

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