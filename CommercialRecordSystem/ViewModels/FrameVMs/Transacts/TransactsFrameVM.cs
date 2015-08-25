using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.Views.Transacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Transacts
{
    class TransactsFrameVM : FrameVMBase
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
        #endregion

        public TransactsFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            openTransactionCmd = new ICommandImp(openTransactionCmd_execute);
            startNewTransactCmd = new ICommandImp(startNewTransact_execute);
            setTransacts();
        }

        private void openTransactionCmd_execute(object obj)
        {
            switch (SelectedTransact.Type)
            {
                case Transact.TYPE.SALE:
                    Navigation.Navigate(typeof(Sales), selectedTransact);
                    break;
                case Transact.TYPE.ORDER:
                    Navigation.Navigate(typeof(Sales), selectedTransact);
                    break;
                case Transact.TYPE.PAYMENT:
                    Navigation.Navigate(typeof(Payments), selectedTransact);
                    break;
            }
        }

        private void startNewTransact_execute(object obj)
        {
            Navigation.Navigate(typeof(TransactTypeSelector));
        }

        private async Task setTransacts()
        {
            List<Expression<Func<Transact, object>>> orderByClauses = new List<Expression<Func<Transact, object>>>();
            orderByClauses.Add(c => c.Date);
            Transacts = new ObservableCollection<TransactVM>(await TransactVM.getList<TransactVM>(null, orderByClauses));

            double totalAccountBuff = 0.0;
            foreach (TransactVM transactBuff in Transacts)
            {
                totalAccountBuff += transactBuff.Cost;
            }
            //TotalAccount = totalAccountBuff;
        }
    }
}
