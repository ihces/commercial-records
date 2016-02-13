using CommercialRecordSystem.Common;
using CommercialRecordSystem.Constants;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.Models.Accounts;
using CommercialRecordSystem.ViewModels.DataVMs;
using CommercialRecordSystem.Views.Accounts;
using CommercialRecordSystem.Views.Transacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CommercialRecordSystem.ViewModels
{
    class CurrentAccountListFrameVM : FrameVMBase
    {
        #region Properties
        private readonly ICommand findActorsCmd;
        public ICommand FindActorsCmd
        {
            get
            {
                return findActorsCmd;
            }
        }

        private ObservableCollection<string> accountTypesForList = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("accountTypesForList"));
        public ObservableCollection<string> AccountTypesForList
        {
            get
            {
                return accountTypesForList;
            }
            set
            {
                accountTypesForList = value;
                RaisePropertyChanged("AccountTypesForList");
            }
        }

        private int selectedAcctTypeBuff = -1;

        private string selectedAccountType;
        public string SelectedAccountType
        {
            get
            {
                return selectedAccountType;
            }
            set
            {
                if (null != value)
                {
                    if (value == AccountTypesForList[0])
                        selectedAcctTypeBuff = -1;
                    else if (value == AccountTypesForList[1])
                        selectedAcctTypeBuff = CurrentAccount.TYPE_DEBT;
                    else if (value == AccountTypesForList[2])
                        selectedAcctTypeBuff = CurrentAccount.TYPE_RECEIVABLE;
                }

                setActors();

                selectedAccountType = value;
                RaisePropertyChanged("SelectedAccountType");
            }
        }

        private readonly ICommand doper4SelectedActorCmd;
        public ICommand Doper4SelectedActorCmd
        {
            get
            {
                return doper4SelectedActorCmd;
            }
        }

        private readonly ICommand editCurrentActorCmd;
        public ICommand EditCurrentActorCmd
        {
            get
            {
                return editCurrentActorCmd;
            }
        }

        private readonly ICommand addActorCmd;
        public ICommand AddActorCmd
        {
            get
            {
                return addActorCmd;
            }
        }

        private string queryText = string.Empty;
        public string QueryText
        {
            get
            {
                return queryText;
            }
            set
            {
                queryText = value;
                RaisePropertyChanged("QueryText");
            }
        }

        /*
         *unused*
        private int orderByIndex = 0;
        public int OrderByIndex
        {
            get
            {
                return orderByIndex;
            }
            set
            {
                orderByIndex = value;
                if (value != orderByIndex)
                {
                    List<Expression<Func<Actor, object>>> orderByClauses =
                        new List<Expression<Func<Actor, object>>>();
                    switch (value)
                    {
                        case 0:
                            orderByClauses.Add(c => c.Name);
                            orderByClauses.Add(c => c.Surname);
                            setActors(null, orderByClauses);
                            break;
                        case 1:
                            orderByClauses.Add(c => c.Surname);
                            orderByClauses.Add(c => c.Name);
                            setActors(null, orderByClauses);
                            break;
                        case 2:
                            orderByClauses.Add(c => c.LastTransactDate);
                            orderByClauses.Add(c => c.Name);
                            orderByClauses.Add(c => c.Surname);
                            setActors(null, orderByClauses);
                            break;
                        case 3:
                            //orderByClauses.Add(c => c.);
                            orderByClauses.Add(c => c.Name);
                            orderByClauses.Add(c => c.Surname);
                            setActors(null, orderByClauses);
                            break;
                    }
                    RaisePropertyChanged("OrderByIndex");
                }
            }
        }*/



        private ObservableCollection<ActorVM> actors;
        public ObservableCollection<ActorVM> Actors
        {
            get
            {
                return actors;
            }
            set
            {
                actors = value;
                RaisePropertyChanged("Actors");
            }
        }

        private ActorVM selectedActor;
        public ActorVM SelectedActor
        {
            get
            {
                return selectedActor;
            }
            set
            {
                selectedActor = value;
                RaisePropertyChanged("SelectedActor");
            }
        }

        private Int32 rowCount;
        public Int32 RowCount
        {
            get
            {
                return rowCount;
            }
            set
            {
                rowCount = value;
                RaisePropertyChanged("RowCount");
            }
        }

        private double totalAccount;
        public double TotalAccount
        {
            get
            {
                return totalAccount;
            }
            set
            {
                totalAccount = value;
                RaisePropertyChanged("TotalAccount");
            }
        }

        #endregion "Properties"

        public CurrentAccountListFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            findActorsCmd = new ICommandImp(findActors_execute);
            doper4SelectedActorCmd = new ICommandImp(doOper4SelectedActor_execute);
            addActorCmd = new ICommandImp(addActor_execute);
            editCurrentActorCmd = new ICommandImp(editCurrentActorCmd_execute);

            if (null != navigation.Back && navigation.Back.Is<Sales>())
            {
                PageReadOnly = true;

                if (Transact.TYPE_PURCHASE == (int)navigation.Message)
                {
                    SelectedAccountType = AccountTypesForList[2];
                }
                else
                {
                    SelectedAccountType = AccountTypesForList[1];
                }
            }
            else
            {
                PageReadOnly = false;
                SelectedAccountType = AccountTypesForList[0];
            }
        }

        private void editCurrentActorCmd_execute(object obj)
        {
            if (null != SelectedActor)
                Navigation.Navigate(typeof(ActorInfo), SelectedActor.Id);
        }

        #region Command Method
        public void findActors_execute(object parameter)
        {
            setActors();
        }

        private void doOper4SelectedActor_execute(object obj)
        {
            if (null != SelectedActor)
            {
                if (Navigation.Back.Is<Sales>())
                {
                    Navigation.GoBack(SelectedActor.Id);
                }
                else
                {
                    Navigation.Navigate<CurrentAccountView>(SelectedActor);
                }
            }
        }

        private void addActor_execute(object obj)
        {
            Navigation.Navigate(typeof(ActorInfo));
        }
        #endregion

        private async Task setActors()
        {
            Actors = new ObservableCollection<ActorVM>(await ActorVM.getList<ActorVM>(ScriptConsts.QUERY_ACTORS_FOR_LIST,
                selectedAcctTypeBuff, QueryText));

            RowCount = Actors.Count;

            double totalAccountBuff = 0.0;
            foreach (ActorVM actorBuff in Actors)
            {
                switch (selectedAcctTypeBuff)
                {
                    case -1:
                        actorBuff.TotalAcctForList = actorBuff.RemainingCost;
                        break;
                    case 0:
                        actorBuff.TotalAcctForList = actorBuff.DebtAcctTotal - actorBuff.DebtAcctPaid;
                        break;
                    case 1:
                        actorBuff.TotalAcctForList = -(actorBuff.ReceivableAccTotal - actorBuff.ReceivableAccPaid);
                        break;
                }

                totalAccountBuff += actorBuff.TotalAcctForList;
            }
            TotalAccount = totalAccountBuff;
        }
    }
}
