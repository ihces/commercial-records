using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
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
                    List<Expression<Func<Actor, object>>>  orderByClauses =
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
        }



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
            setActors();
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
                if (Navigation.Back.PageType.Equals(typeof(Sales)))
                {
                    Navigation.GoBack(SelectedActor.Id);
                }
                else
                {
                    Navigation.Navigate<CurrentAccountView>(SelectedActor.Id);
                }
            }
        }

        private void addActor_execute(object obj)
        {
            Navigation.Navigate(typeof(ActorInfo));
        }
        #endregion

        private async Task setActors(Expression<Func<Actor, bool>> whereClause = null, List<Expression<Func<Actor, object>>> orderByClauses = null)
        {
            if (null == orderByClauses)
            {
                orderByClauses = new List<Expression<Func<Actor, object>>>();
                orderByClauses.Add(c => c.Name);
                orderByClauses.Add(c => c.Surname);
            }

            if (string.IsNullOrWhiteSpace(QueryText))
                whereClause = c => c.Registered == true;
            else
                whereClause = c => c.Registered == true && (c.Name.Contains(QueryText) || c.Surname.Contains(QueryText));

            Actors = new ObservableCollection<ActorVM>(await ActorVM.getList<ActorVM>(whereClause, orderByClauses));
            RowCount = Actors.Count;

            double totalAccountBuff = 0.0;
            foreach (ActorVM actorBuff in Actors)
            {
                totalAccountBuff += actorBuff.RemainingCost;
            }
            TotalAccount = totalAccountBuff;
        }
    }
}
