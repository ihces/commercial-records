using System;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Common;
using System.Windows.Input;
using CommercialRecordSystem.Views.Customers;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommercialRecordSystem.Views.Transacts;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerAccountFrameVM : FrameVMBase
    {
        #region Properties
        private CustomerVM currentCustomer = new CustomerVM();
        public CustomerVM CurrentCustomer
        { 
            get
            {
                return currentCustomer;
            }
            set
            {
                currentCustomer = value;
                RaisePropertyChanged("CurrentCustomer");
            }
        }

        private TransactVM selectedTransact = new TransactVM();
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

        private ObservableCollection<TransactVM> transacts = new ObservableCollection<TransactVM>();
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

        private readonly ICommand opemTransactionCmd;
        public ICommand OpemTransactionCmd
        {
            get
            {
                return opemTransactionCmd;
            }
        }

        private readonly ICommand editCustomerCmd;
        public ICommand EditCustomerCmd
        {
            get
            {
                return editCustomerCmd;
            }
        }

        private void editCustomer_execute(object obj)
        {
            Navigation.Navigate(typeof(CustomerInfo), CurrentCustomer.Id);
        }

        private void opemTransactionCmd_execute(object obj)
        {
            if (Transact.TYPE.PAYMENT.Equals(SelectedTransact.Type))
                Navigation.Navigate(typeof(Payments), SelectedTransact);
            else
                Navigation.Navigate(typeof(Sales), SelectedTransact);
        }
        

        #endregion

        public CustomerAccountFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            editCustomerCmd = new ICommandImp(editCustomer_execute);
            opemTransactionCmd = new ICommandImp(opemTransactionCmd_execute);

            if (navigation.Forward == null) {
                CurrentCustomer.get((int)navigation.Message);
                CurrentCustomer.Name += " " + CurrentCustomer.Surname;
                setTransacts();
            }
        }

        private async Task setTransacts()
        {
            List<Expression<Func<Transact, object>>> orderByClauses = null;
            orderByClauses = new List<Expression<Func<Transact, object>>>();
                orderByClauses.Add(c => c.Date);
            
            Transacts = new ObservableCollection<TransactVM>(
                await TransactVM.getList<TransactVM>(c => c.CustomerId == CurrentCustomer.Id, orderByClauses));
        }
    }
}
