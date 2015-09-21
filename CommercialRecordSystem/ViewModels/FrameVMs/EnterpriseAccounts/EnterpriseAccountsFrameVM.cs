using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.EnterpriseAccounts;
using CommercialRecordSystem.ViewModels.DataVMs.EnterpriseAccounts;
using CommercialRecordSystem.ViewModels.DataVMs.IncomeNExpenses;
using CommercialRecordSystem.Views.EnterpriseAccounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommercialRecordSystem.ViewModels.FrameVMs.CashRegNBank
{
    class EnterpriseAccountsFrameVM : FrameVMBase
    {
        #region Properties
        private ObservableCollection<EnterpriseAccountVM> accounts;
        public ObservableCollection<EnterpriseAccountVM> Accounts
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

        private EnterpriseAccountVM selectedAccount;
        public EnterpriseAccountVM SelectedAccount
        {
            get
            {
                return selectedAccount;
            }
            set
            {
                selectedAccount = value;
                RaisePropertyChanged("SelectedAccount");
            }
        }

        private ObservableCollection<IncomeNExpenseVM> accountRecords;
        public ObservableCollection<IncomeNExpenseVM> AccountRecords
        {
            get
            {
                return accountRecords;
            }
            set
            {
                accountRecords = value;
                RaisePropertyChanged("AccountRecords");
            }
        }

        private DateTime startDate;
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

        private DateTime endDate;
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

        private int selectedListingModeIndex;
        public int SelectedListingModeIndex
        {
            get
            {
                return selectedListingModeIndex;
            }
            set
            {
                selectedListingModeIndex = value;
                RaisePropertyChanged("SelectedListingModeIndex");
            }
        }

        private int totalRecord;
        public int TotalRecord
        {
            get
            {
                return totalRecord;
            }
            set
            {
                totalRecord = value;
                RaisePropertyChanged("TotalRecord");
            }
        }

        private double totalCost;
        public double    TotalCost
        {
            get
            {
                return totalCost;
            }
            set
            {
                totalCost = value;
                RaisePropertyChanged("TotalCost");
            }
        }
        #endregion

        #region Commands
        private readonly ICommand createNewAccountCmd;
        public ICommand CreateNewAccountCmd
        {
            get
            {
                return createNewAccountCmd;
            }
        }

        private readonly ICommand editCurrentAccountCmd;
        public ICommand EditCurrentAccountCmd
        {
            get
            {
                return editCurrentAccountCmd;
            }
        }
        #endregion

        #region CommandHandlers
        public void createNewAccountCmd_execute(object parameter)
        {
            Navigation.Navigate<EnterpriseAccountInfo>();
        }

        public void editCurrentAccountCmd_execute(object parameter)
        {
            Navigation.Navigate<EnterpriseAccountInfo>(SelectedAccount.Id);
        }
        #endregion

        public EnterpriseAccountsFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            createNewAccountCmd = new ICommandImp(createNewAccountCmd_execute);
            editCurrentAccountCmd = new ICommandImp(editCurrentAccountCmd_execute);
            setAccounts();
        }

        private async Task setAccounts()
        {
            List<Expression<Func<EnterpriseAccount, object>>> orderByClauses = new List<Expression<Func<EnterpriseAccount, object>>>();
            orderByClauses.Add(c => c.Name);

            Accounts = new ObservableCollection<EnterpriseAccountVM>(await EnterpriseAccountVM.getList<EnterpriseAccountVM>(null, orderByClauses));
        }
    }
}
