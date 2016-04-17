﻿using CommercialRecords.Models.Settings;
using CommercialRecords.ViewModels.DataVMs.Accounts.EnterpriseAccounts;

namespace CommercialRecords.ViewModels.DataVMs.Settings
{
    public class UserVM : InfoDataVMBase<User>
    {
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

        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                RaisePropertyChanged("Surname");
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        private string role;
        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
                RaisePropertyChanged("Role");
            }
        }

        private int cashRegisterId;
        public int CashRegisterId
        {
            get
            {
                return cashRegisterId;
            }
            set
            {
                cashRegisterId = value;

                if (cashRegisterId > 0)
                    CashRegisterName = ((EnterpriseAccountVM)(new EnterpriseAccountVM()).get(cashRegisterId)).Name;
                RaisePropertyChanged("CashRegisterId");
            }
        }

        private string cashRegisterName;
        public string CashRegisterName
        {
            get
            {
                return cashRegisterName;
            }
            set
            {
                cashRegisterName = value;
                RaisePropertyChanged("CashRegisterName");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string passwordValidation;
        public string PasswordValidation
        {
            get
            {
                return passwordValidation;
            }
            set
            {
                passwordValidation = value;
                RaisePropertyChanged("PasswordValidation");
            }
        }

        private string detail;
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

        public UserVM() : base(App.ProfileImgFolder) { }
    }
}
