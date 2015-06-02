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

        private FirmVM selectedFirm;
        public FirmVM SelectedFirm
        {
            get
            {
                return selectedFirm;
            }
            set
            {
                selectedFirm = value;
                RaisePropertyChanged("SelectedFirm");
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

        private readonly ICommand editFirmCmd;
        public ICommand EditFirmCmd
        {
            get
            {
                return editFirmCmd;
            }
        }

        private readonly ICommand firmTappedCmd;
        public ICommand FirmTappedCmd
        {
            get
            {
                return firmTappedCmd;
            }
        }
        #endregion

        public GoodsFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            addFirmCmd = new ICommandImp(addFirm_execute);
            editFirmCmd = new ICommandImp(editFirm_execute);
            firmTappedCmd = new ICommandImp(firmTapped_execute);

            setFirms();
        }

        private async Task setFirms(){
            Firms = await FirmVM.getList<FirmVM>(null, null);
        }

        private async void firmTapped_execute(object obj)
        {
            if (SelectedFirm.ShowGoodList)
            {
                SelectedFirm.Navigation = Navigation;
                await SelectedFirm.loadGoods();
            }
        }

        private void addFirm_execute(object obj)
        {
            Navigation.Navigate<FirmInfo>();
        }

        private void editFirm_execute(object obj)
        {
            if (null != SelectedFirm)
                Navigation.Navigate<FirmInfo>(SelectedFirm.Id);
        }
    }
}
