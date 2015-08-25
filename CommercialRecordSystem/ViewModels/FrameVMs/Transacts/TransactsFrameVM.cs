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
            startNewTransactCmd = new ICommandImp(startNewTransact_execute);
            setTransacts();
        }

        private void startNewTransact_execute(object obj)
        {
            Navigation.Navigate(typeof(TransactTypeSelector));
        }

        private async Task setTransacts()
        {
            List<Expression<Func<Transact, object>>> orderByClauses = new List<Expression<Func<Transact, object>>>();
            //orderByClauses.Add(c => c.);
            //orderByClauses.Add(c => c.Surname);
            //Transacts = await TransactVM.getList<TransactVM>(null, orderByClauses);

            double totalAccountBuff = 0.0;
            foreach (TransactVM transactBuff in Transacts)
            {
                totalAccountBuff += transactBuff.Cost;
            }
            //TotalAccount = totalAccountBuff;
        }
    }
}
