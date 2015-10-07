
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.EnterpriseAccounts;
using CommercialRecordSystem.Models.IncomeNExpense;
using CommercialRecordSystem.ViewModels.DataVMs.EnterpriseAccounts;
using CommercialRecordSystem.ViewModels.DataVMs.IncomeNExpenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommercialRecordSystem.ViewModels.FrameVMs.IncomeNExpenses
{
    class IncomeNExpenseFrameVM : FrameVMBase
    {
        #region Properties
        private ObservableCollection<EnterpriseAccountVM> accounts = new ObservableCollection<EnterpriseAccountVM>();
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

        private EnterpriseAccountVM selectedAccount = new EnterpriseAccountVM();
        public EnterpriseAccountVM SelectedAccount
        {
            get
            {
                return selectedAccount;
            }
            set
            {
                if (value != selectedAccount)
                {
                    selectedAccount = value;
                    setAccountRecordList();
                }

                RaisePropertyChanged("SelectedAccount");
            }
        }

        private IncomeNExpenseVM newAccountRecord = new IncomeNExpenseVM();
        public IncomeNExpenseVM NewAccountRecord
        {
            get
            {
                return newAccountRecord;
            }
            set
            {
                newAccountRecord = value;
                RaisePropertyChanged("NewAccountRecord");
            }
        }

        private ObservableCollection<IncomeNExpenseVM> accountRecordList = new ObservableCollection<IncomeNExpenseVM>();
        public ObservableCollection<IncomeNExpenseVM> AccountRecordList
        {
            get
            {
                return accountRecordList;
            }
            set
            {
                accountRecordList = value;
                RaisePropertyChanged("AccountRecordList");
            }
        }

        private double totalIncome;
        public double TotalIncome
        {
            get
            {
                return totalIncome;
            }
            set
            {
                totalIncome = value;
                RaisePropertyChanged("TotalIncome");
            }
        }

        private double totalExpense;
        public double TotalExpense
        {
            get
            {
                return totalExpense;
            }
            set
            {
                totalExpense = value;
                RaisePropertyChanged("TotalExpense");
            }
        }

        private double revenue;
        public double Revenue
        {
            get
            {
                return revenue;
            }
            set
            {
                revenue = value;
                RaisePropertyChanged("Revenue");
            }
        }
        #endregion

        #region Commands
        private readonly ICommand addRecordsToListCmd;
        public ICommand AddRecordsToListCmd
        {
            get
            {
                return addRecordsToListCmd;
            }
        }

        public void addRecordsToListCmd_execute(object parameter)
        {
            NewAccountRecord.AccountId = SelectedAccount.Id;
            NewAccountRecord.save();
            AccountRecordList.Add(NewAccountRecord);
            NewAccountRecord = new IncomeNExpenseVM();
            NewAccountRecord.Refresh();
        }
        #endregion

        public IncomeNExpenseFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            addRecordsToListCmd = new ICommandImp(addRecordsToListCmd_execute);
            setAccounts();
            setAccountRecordList();
        }

        private async Task setAccounts()
        {
            List<Expression<Func<EnterpriseAccount, object>>> orderByClauses = new List<Expression<Func<EnterpriseAccount, object>>>();
            orderByClauses.Add(c => c.Name);

            Accounts = new ObservableCollection<EnterpriseAccountVM>(await EnterpriseAccountVM.getList<EnterpriseAccountVM>(null, orderByClauses));
            
            if (Accounts.Count > 0)
                SelectedAccount = Accounts[0];
        }

        private async Task setAccountRecordList()
        {
            List<Expression<Func<IncomeNExpense, object>>> orderByList = new List<Expression<Func<IncomeNExpense, object>>>();
            orderByList.Add(e => e.Id);
            Expression<Func<IncomeNExpense, bool>> whereClause = null;
            if (null != SelectedAccount)
                whereClause = I_E => I_E.AccountId == SelectedAccount.Id;
            AccountRecordList = new ObservableCollection<IncomeNExpenseVM>(await IncomeNExpenseVM.getList<IncomeNExpenseVM>(whereClause, null));
        }
    }
}
