using CommercialRecordSystem.Common;
using System.Linq;
using CommercialRecordSystem.ViewModels.DataVMs.Accounts;
using CommercialRecordSystem.Models.Accounts;
using System;

namespace CommercialRecordSystem.ViewModels.DataVMs.Accounts.EnterpriseAccounts
{
    class EnterpriseAccountVM : AccountBaseVM<EnterpriseAccount>
    {
        public string accountNumber;
        public string AccountNumber
        {
            get
            {
                return accountNumber;
            }
            set
            {
                accountNumber = value;
                RaisePropertyChanged("AccountNumber");
            }
        }

        public string bankName;
        public string BankName
        {
            get
            {
                return bankName;
            }
            set
            {
                bankName = value;
                RaisePropertyChanged("BankName");
            }
        }

        public double balance;
        public double Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
                if (balance == 0)
                    Active = false;
                else
                    Active = true;

                RaisePropertyChanged("Balance");
            }
        }

        public EnterpriseAccountVM()
        {
        }
    }
}
