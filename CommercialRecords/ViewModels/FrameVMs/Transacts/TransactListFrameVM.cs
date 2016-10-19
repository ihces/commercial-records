using CommercialRecords.Common;
using CommercialRecords.Models;
using CommercialRecords.ViewModels.DataVMs.Settings;
using CommercialRecords.Views.Transacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommercialRecords.ViewModels.FrameVMs.Transacts
{
    class TransactListFrameVM : FrameVMBase
    {
        #region Properties
        private TransactVM selectedTransact;
        public TransactVM SelectedTransact
        {
            get
            {
                return selectedTransact;
            }
            set
            {
                selectedTransact = value;
                RaisePropertyChanged("SelectedTransact");
            }
        }

        private ObservableCollection<string> transactions = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("transactTypes"));
        public ObservableCollection<string> Transactions
        {
            get
            {
                return transactions;
            }
        }

        private ObservableCollection<UserVM> users;
        public ObservableCollection<UserVM> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                RaisePropertyChanged("Users");
            }
        }

        private UserVM selectedUser;
        public UserVM SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                RaisePropertyChanged("SelectedUser");
                if (null != selectedUser)
                    setTransacts();
            }
        }

        private string selectedTransaction;
        public string SelectedTransaction
        {
            get
            {
                return selectedTransaction;
            }
            set
            {
                selectedTransaction = value;
                RaisePropertyChanged("SelectedTransaction");
                if (null != selectedTransaction)
                    setTransacts();
            }
        }

        private readonly ICommand openTransactionCmd;
        public ICommand OpenTransactionCmd
        {
            get
            {
                return openTransactionCmd;
            }
        }

        private readonly ICommand startNewTransactCmd;
        public ICommand StartNewTransactCmd
        {
            get
            {
                return startNewTransactCmd;
            }
        }

        private double totalAmount;
        public double TotalAmount
        {
            get
            {
                return totalAmount;
            }
            set
            {
                totalAmount = value;
                RaisePropertyChanged("TotalAmount");
            }
        }

        private ObservableCollection<TransactVM> transacts;
        public ObservableCollection<TransactVM> Transacts
        {
            get
            {
                return transacts;
            }
            set
            {
                transacts = value;
                RaisePropertyChanged("Transacts");
            }
        }

        private DateTime startDate = DateTime.Now.AddMonths(-1).AddSeconds(1);
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                DateTime startDateBuff = startDate;
                startDate = value;
                RaisePropertyChanged("StartDate");
                if (null != startDate && !startDate.Equals(startDateBuff))
                    setTransacts();
            }
        }

        private DateTime endDate = DateTime.Now;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                DateTime endDateBuff = endDate;
                endDate = value;
                RaisePropertyChanged("EndDate");
                if (null != endDate && !endDate.Equals(endDateBuff))
                    setTransacts();
            }
        }

        public DateTime EndDateBound
        {
            get
            {
                return DateTime.Now > StartDate ? DateTime.Now : StartDate;
            }
        }

        public DateTime StartDateBound
        {
            get
            {
                return EndDate.AddMonths(-1);
            }
        }
        #endregion

        public TransactListFrameVM(FrameNavigation navigation)
            : base(navigation, false)
        {
            openTransactionCmd = new ICommandImp(openTransactionCmd_execute);
            startNewTransactCmd = new ICommandImp(startNewTransact_execute);
            init();
        }

        private async Task init()
        {
            selectedTransaction = Transactions[0];
            RaisePropertyChanged("SelectedTransaction");

            await setUsers();
            await setTransacts();
        }

        private void openTransactionCmd_execute(object obj)
        {
            if (null != SelectedTransact)
                switch (SelectedTransact.Type)
                {
                    case Transact.TYPE_SALE:
                        Navigation.Navigate(typeof(Sales), selectedTransact);
                        break;
                    case Transact.TYPE_ORDER:
                        Navigation.Navigate(typeof(Sales), selectedTransact);
                        break;
                    case Transact.TYPE_PURCHASE:
                        Navigation.Navigate(typeof(Sales), selectedTransact);
                        break;
                    case Transact.TYPE_PAYMENT:
                        Navigation.Navigate(typeof(Payments), selectedTransact);
                        break;
                }
        }

        private void startNewTransact_execute(object obj)
        {
            Navigation.Navigate(typeof(Sales));
        }

        private async Task setTransacts()
        {
            List<Expression<Func<Transact, bool>>> whereClauses = new List<Expression<Func<Transact, bool>>>();
            whereClauses.Add(t => t.Date >= StartDate && t.Date <= EndDate);

            if (!string.IsNullOrWhiteSpace(SelectedTransaction))
            {
                int type = Int32.Parse(SelectedTransaction);
                whereClauses.Add(t => t.Type.Equals(type));
            }

            if (null != SelectedUser)
                whereClauses.Add(t => t.UserId.Equals(SelectedUser.Id));

            List<TransactVM> transactList = await TransactVM.getList<TransactVM>(
                whereClauses, new List<Expression<Func<Transact, object>>>() { c => c.Date });

            Transacts = new ObservableCollection<TransactVM>(transactList);

            double totalAccountBuff = 0.0;
            foreach (TransactVM transactBuff in Transacts)
            {
                totalAccountBuff += transactBuff.RemainingCost;
            }
            TotalAmount = totalAccountBuff;
        }

        private async Task setUsers()
        {
            Users = new ObservableCollection<UserVM>(await UserVM.getList<UserVM>());
            int userId = CrsAuthentication.getInstance().SessionControl.CurrentUser.Id;
            foreach (UserVM user in Users)
            {
                if (userId == user.Id)
                {
                    selectedUser = user;
                    RaisePropertyChanged("SelectedUser");
                    break;
                }
            }
        }
    }
}
