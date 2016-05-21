using CommercialRecords.Common;
using CommercialRecords.ViewModels.DataVMs;
using CommercialRecords.ViewModels.DataVMs.Settings;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CommercialRecords.ViewModels.FrameVMs
{
    class TransactionReportsFrameVM : FrameVMBase
    {
        #region Properties
        private ObservableCollection<TransactReportVM> transactReports;
        public ObservableCollection<TransactReportVM> TransactReports
        {
            get
            {
                return transactReports;
            }
            set
            {
                transactReports = value;
                RaisePropertyChanged("TransactReports");
            }
        }

        private TransactReportVM selectedTransactReport;
        public TransactReportVM SelectedTransactReport
        {
            get
            {
                return selectedTransactReport;
            }
            set
            {
                selectedTransactReport = value;
                RaisePropertyChanged("SelectedTransactReport");
            }
        }

        private ObservableCollection<string> transactions = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("transactions"));
        public ObservableCollection<string> Transactions
        {
            get
            {
                return transactions;
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
            }
        }

        private DateTime startDate = DateTime.Now.AddMonths(-1);
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                RaisePropertyChanged("StartDate");
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
                endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }
        #endregion Properties

        public TransactionReportsFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            if (Transactions.Count > 0)
                SelectedTransaction = Transactions[0];

            setUsers();
            setTransactReports();
        }

        private async Task setUsers()
        {
            Users = new ObservableCollection<UserVM>(await UserVM.getList<UserVM>());
            if (Users.Count > 0)
                SelectedUser = Users[0];
        }

        private async Task setTransactReports()
        {
            TransactReports = new ObservableCollection<TransactReportVM>(await TransactReportVM.getList<TransactReportVM>());
        }
    }
}
