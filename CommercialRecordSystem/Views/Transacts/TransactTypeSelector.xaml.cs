using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Views.Transacts
{
    public sealed partial class TransactTypeSelector : ViewBase
    {
        public TransactTypeSelector() : base(typeof(TransactTypeFrameVM))
        {
            this.InitializeComponent();
        }
        /*public TransactTypeSelector()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TransactVM transact = new TransactVM();
            if (null != e.Parameter)
            {
                if (e.Parameter is TransactVM)
                { 
                    transact = (TransactVM)e.Parameter;
                }
                else if (e.Parameter is int)
                { 
                    transact.CustomerId=(int)e.Parameter;
                }
                this.DataContext = new TransactTypeFrameVM(this.Frame, transact);
            }
            else
            {
                this.DataContext = new TransactTypeFrameVM(this.Frame, transact);
            }
        }
        
        private void StartTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            TransactTypeFrameVM dataContextBuff = (TransactTypeFrameVM)this.DataContext;
            this.Frame.Navigate(typeof(Sales), dataContextBuff);
        }*/
    }
}
