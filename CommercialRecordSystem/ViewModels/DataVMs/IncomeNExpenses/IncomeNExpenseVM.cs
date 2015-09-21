using System;
using CommercialRecordSystem.Models.IncomeNExpense;

namespace CommercialRecordSystem.ViewModels.DataVMs.IncomeNExpenses
{
    class IncomeNExpenseVM : DataVMBase<IncomeNExpense>
    {
        private  IncomeNExpense.MODE mode;
        public IncomeNExpense.MODE Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                RaisePropertyChanged("Mode");
            }
        }

        private int type;
        public int Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                RaisePropertyChanged("Type");
            }
        }
        
        private int accountId;
        public int AccountId
        {
            get
            {
                return accountId;
            }
            set
            {
                accountId = value;
                RaisePropertyChanged("AccountId");
            }
        }
        
        private string details;
        public string Details
        {
            get
            {
                return details;
            }
            set
            {
                details = value;
                RaisePropertyChanged("Details");
            }
        }

        private double cost;
        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
                RaisePropertyChanged("Cost");
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                RaisePropertyChanged("Date");
            }
        }
    }
}
