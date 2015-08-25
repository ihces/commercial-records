using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
using System;
using System.Windows.Input;
using CommercialRecordSystem.Views.Goods;
using CommercialRecordSystem.ViewModels.DataVMs.Goods;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class GoodInfoFrameVM : InfoFrameVMBase<GoodVM, Good>
    {
        private readonly ICommand selectRecordedFirmCmd = null;
        public ICommand SelectRecordedFirmCmd
        {
            get
            {
                return selectRecordedFirmCmd;
            }
        }

        private readonly ICommand selectRecordedCategoryCmd = null;
        public ICommand SelectRecordedCategoryCmd
        {
            get
            {
                return selectRecordedCategoryCmd;
            }
        }

        private void selectRecordedFirmCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(GoodsView), GoodsFrameVM.SELECT_FIRM);
        }

        private void selectRecordedCategoryCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(GoodsView), GoodsFrameVM.SELECT_CATEGORY);
        }

        public GoodInfoFrameVM(FrameNavigation navigation)
            : base(navigation, "Ürün", 1.0)
        {
            selectRecordedFirmCmd = new ICommandImp(selectRecordedFirmCmdHandler);
            selectRecordedCategoryCmd = new ICommandImp(selectRecordedCategoryCmdHandler);

            if (null != navigation.Message)
            {
                if (navigation.Message is CategoryVM)
                {
                    CategoryVM selectedCategory = (CategoryVM)navigation.Message;
                    CurrentInfo.CategoryId = selectedCategory.Id;
                }
                else if (navigation.Message is FirmVM)
                {
                    FirmVM selectedFirm = (FirmVM)navigation.Message;
                    CurrentInfo.FirmId = selectedFirm.Id;
                }
            }
        }
    }
}
