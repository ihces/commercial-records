using CommercialRecordSystem.Common;
using CommercialRecordSystem.Views;
using System.Windows.Input;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class GoodsFrameVM : FrameVMBase
    {
        #region Properties
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
            editGoodCmd = new ICommandImp(editGood_execute);
        }

        private void editGood_execute(object obj)
        {
            Navigation.Navigate<GoodInfo>();
        }

        private void addGood_execute(object obj)
        {
            Navigation.Navigate<GoodInfo>();
        }
    }
}
