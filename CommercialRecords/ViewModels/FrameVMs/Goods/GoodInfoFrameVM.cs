using CommercialRecords.Common;
using CommercialRecords.Models.Goods;
using System.Windows.Input;
using CommercialRecords.Views.Goods;
using CommercialRecords.ViewModels.DataVMs.Goods;
using System.Collections.ObjectModel;

namespace CommercialRecords.ViewModels.FrameVMs.Goods
{
    class GoodInfoFrameVM : InfoFrameVMBase<GoodVM, Good>
    {
        private ObservableCollection<string> measures = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("measures"));
        public ObservableCollection<string> Measures
        {
            get
            {
                return measures;
            }
        }

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
            Navigation.Navigate(typeof(GoodsView), GoodsFrameVM.SELECT_BRAND);
        }

        private void selectRecordedCategoryCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(GoodsView), GoodsFrameVM.SELECT_CATEGORY);
        }

        public GoodInfoFrameVM(FrameNavigation navigation)
            : base(navigation, CrsDictionary.getInstance().lookup("infoPageTitles","good"), 1.0)
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
                else if (navigation.Message is BrandVM)
                {
                    BrandVM selectedFirm = (BrandVM)navigation.Message;
                    CurrentInfo.BrandId = selectedFirm.Id;
                }
            }
        }
    }
}
