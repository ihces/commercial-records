using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.EnterpriseAccounts;

namespace CommercialRecordSystem.ViewModels.DataVMs.EnterpriseAccounts
{
    class EnterpriseAccountVM : InfoDataVMBase<EnterpriseAccount>
    {
        public EnterpriseAccount.TYPE type;
        public EnterpriseAccount.TYPE Type
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
        
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public EnterpriseAccount.DEPOSIT_TYPE depositType;
        public EnterpriseAccount.DEPOSIT_TYPE DepositType
        {
            get
            {
                return depositType;
            }
            set
            {
                depositType = value;
                RaisePropertyChanged("DepositType");
            }
        }

        public int currencyType;
        public int CurrencyType
        {
            get
            {
                return currencyType;
            }
            set
            {
                currencyType = value;
                RaisePropertyChanged("CurrencyType");
            }
        }

        public string iban;
        public string Iban
        {
            get
            {
                return iban;
            }
            set
            {
                iban = value;
                RaisePropertyChanged("Iban");
            }
        }

        public string detail;
        public string Detail
        {
            get
            {
                return detail;
            }
            set
            {
                detail = value;
                RaisePropertyChanged("Detail");
            }
        }

        private double totalReceivable;
        public double TotalReceivable
        {
            get
            {
                return totalReceivable;
            }
            set
            {
                totalReceivable = value;
                RaisePropertyChanged("TotalReceivable");
            }
        }

        private double totalPayable;
        public double TotalPayable
        {
            get
            {
                return totalPayable;
            }
            set
            {
                totalPayable = value;
                RaisePropertyChanged("TotalPayable");
            }
        }

        private double totalAsset;
        public double TotalAsset
        {
            get
            {
                return totalAsset;
            }
            set
            {
                totalPayable = value;
                RaisePropertyChanged("TotalAsset");
            }
        }

        public EnterpriseAccountVM(EnterpriseAccount enterpriseAccount)
            : base(enterpriseAccount, App.CommonImgFolder)
        {
        }

        public EnterpriseAccountVM()
            : base(App.CommonImgFolder)
        {
        }
    }
}
