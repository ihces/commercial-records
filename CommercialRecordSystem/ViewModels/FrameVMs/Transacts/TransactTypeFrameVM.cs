using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.Views.Transacts;
using CommercialRecordSystem.Views.Accounts;

namespace CommercialRecordSystem.ViewModels
{
    class TransactTypeFrameVM : FrameVMBase
    {
        #region Properties
        private ActorVM unregisteredActor = new ActorVM() { Type = Actor.TYPE_PERSON, Registered = false };
        private ActorVM registeredActor = new ActorVM() { Type = Actor.TYPE_PERSON, Registered = false };
        private TransactVM transactInfo = new TransactVM();

        public const int SALE_TRANSACT = 0;
        public const int ORDER_TRANSACT = 1;
        public const int PAYMENT_TRANSACT = 2;

        private int selectedTransactTypeIndex = 0;
        public int SelectedTransactTypeIndex
        {
            get
            {
                return selectedTransactTypeIndex;
            }
            set
            {
                selectedTransactTypeIndex = value;
                RaisePropertyChanged("SelectedTransactTypeIndex");
            }
        }

        private int selectedCustomerType = 0;
        public int SelectedCustomerType
        {
            get
            {
                return selectedCustomerType;
            }
            set
            {
                if (value != selectedCustomerType)
                {
                    selectedCustomerType = value;
                    switch (value)
                    {
                        case 0:
                            IsRegisteredActor = false;
                            if (CurrentActor.Registered)
                                registeredActor = CurrentActor;
                            CurrentActor = unregisteredActor;
                            CurrentActor.Refresh();
                            break;
                        case 1:
                            if (!CurrentActor.Registered)
                                unregisteredActor = CurrentActor;
                            CurrentActor = registeredActor;
                            CurrentActor.Refresh();
                            IsRegisteredActor = true;
                            break;
                    }
                }

                RaisePropertyChanged("SelectedCustomerType");
            }
        }

        private ActorVM currentActor = new ActorVM();
        public ActorVM CurrentActor
        {
            get
            {
                return currentActor;
            }
            set
            {
                currentActor = value;
                RaisePropertyChanged("CurrentActor");
            }
        }

        private DateTime selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }

        private bool isRegisteredActor = false;
        public bool IsRegisteredActor
        {
            get
            {
                return isRegisteredActor;
            }
            set
            {
                isRegisteredActor = value;
                RaisePropertyChanged("IsRegisteredActor");
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

        private readonly ICommand startTransactionCmd;
        public ICommand StartTransactionCmd
        {
            get
            {
                return startTransactionCmd;
            }
        }

        #endregion
        #region Command Handlers
        private void selectRecordedActorCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(CurrentAccountList));
        }

        protected void GoBack()
        {
            Navigation.Navigate(typeof(MainPage));
        }

        private void startTransactionCmdHandler(object parameter)
        {
            if (!CurrentActor.Registered)
            {
                CurrentActor.save();
            }

            transactInfo.Date = selectedDate;
            transactInfo.CustomerId = CurrentActor.Id;

            switch (SelectedTransactTypeIndex)
            {
                case SALE_TRANSACT:
                    transactInfo.Type = Transact.TYPE_SALE;
                    transactInfo.save();
                    Navigation.Navigate(typeof(Sales), transactInfo);
                    break;
                case ORDER_TRANSACT:
                    transactInfo.Type = Transact.TYPE_ORDER;
                    transactInfo.save();
                    Navigation.Navigate(typeof(Sales), transactInfo);
                    break;
                case PAYMENT_TRANSACT:
                    transactInfo.Type = Transact.TYPE_PAYMENT;
                    transactInfo.save();
                    Navigation.Navigate(typeof(Payments), transactInfo);
                    break;
            }
        }
        #endregion

        public TransactTypeFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            selectRecordedActorCmd = new ICommandImp(selectRecordedActorCmdHandler);
            startTransactionCmd = new ICommandImp(startTransactionCmdHandler);

            if (navigation.CanGoForward)
            {
                if (navigation.Forward.Is<Sales>() ||
                    navigation.Forward.Is<Payments>())
                {
                    this.transactInfo = (TransactVM)navigation.Message;
                    if (this.transactInfo != null)
                    {
                        SelectedTransactTypeIndex = (int)transactInfo.Type - 1;
                        if (0 != transactInfo.CustomerId)
                        {
                            currentActor.get(transactInfo.CustomerId);
                            if (currentActor.Registered)
                            {
                                registeredActor = CurrentActor;
                                IsRegisteredActor = true;
                            }
                            else
                            {
                                unregisteredActor = CurrentActor;
                                isRegisteredActor = false;
                            }
                        }
                    }
                }
                else if (navigation.Forward.Is<CurrentAccountList>())
                {
                    if (null != navigation.Message)
                    {
                        registeredActor.get((int)navigation.Message);
                        CurrentActor = registeredActor;
                        CurrentActor.Refresh();
                    }
                }
            }
            else
                transactInfo = new TransactVM();
        }
    }
}
