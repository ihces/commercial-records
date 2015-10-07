using CommercialRecordSystem.Common;
using System.Linq;
using CommercialRecordSystem.Models.EnterpriseAccounts;

namespace CommercialRecordSystem.ViewModels.DataVMs.EnterpriseAccounts
{
    class EnterpriseAccountVM : InfoDataVMBase<EnterpriseAccount>
    {
        private string type = App.EnglishDictionary["enterpriseAccountTypes"].Keys.LastOrDefault();
        public string Type
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

        public string depositType = App.EnglishDictionary["depositTypes"].Keys.LastOrDefault();
        public string DepositType
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

        public string currencyType = App.EnglishDictionary["currencies"].Keys.LastOrDefault();
        public string CurrencyType
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
