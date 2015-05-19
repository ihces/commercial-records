using CommercialRecordSystem.Common;
using CommercialRecordSystem.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class GoodsFrameVM : FrameVMBase
    {
        #region Properties
        private ObservableCollection<FirmVM> firms;
        public ObservableCollection<FirmVM> Firms
        {
            get
            {
                return firms;
            }
            set
            {
                firms = value;
                RaisePropertyChanged("Firms");
            }
        }

        private readonly ICommand addFirmCmd;
        public ICommand AddFirmCmd
        {
            get
            {
                return addFirmCmd;
            }
        }

        private readonly ICommand addGoodCmd;
        public ICommand AddGoodCmd
        {
            get
            {
                return addGoodCmd;
            }
        }

        private readonly ICommand editGoodCmd;
        public ICommand EditGoodCmd
        {
            get
            {
                return editGoodCmd;
            }
        }
        #endregion

        public GoodsFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            addGoodCmd = new ICommandImp(addGood_execute);
            addFirmCmd = new ICommandImp(addFirm_execute);
            editGoodCmd = new ICommandImp(editGood_execute);

            setFirms();
        }

        private async Task setFirms(){
            Firms = await FirmVM.getList<FirmVM>(null, null);
            foreach (FirmVM firm in Firms)
            {
                await firm.loadGoods();
            }
        }

        private void editGood_execute(object obj)
        {
            Navigation.Navigate<GoodInfo>();
        }

        private void addGood_execute(object obj)
        {
            Navigation.Navigate<GoodInfo>();
        }

        private void addFirm_execute(object obj)
        {
            Navigation.Navigate<FirmInfo>();
        }
    }
}
