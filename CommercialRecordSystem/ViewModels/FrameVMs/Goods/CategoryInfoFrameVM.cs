using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
using CommercialRecordSystem.ViewModels.DataVMs.Goods;
using CommercialRecordSystem.Views.Goods;
using System.Windows.Input;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class CategoryInfoFrameVM : InfoFrameVMBase<CategoryVM, Category>
    {
        private readonly ICommand selectRecordedCategoryCmd = null;
        public ICommand SelectRecordedCategoryCmd
        {
            get
            {
                return selectRecordedCategoryCmd;
            }
        }

        private void selectRecordedCategoryCmdHandler(object parameter)
        {
            Navigation.Navigate(typeof(GoodsView), CurrentInfo.Id);
        }

        public CategoryInfoFrameVM(FrameNavigation navigation)
            : base(navigation, CrsDictionary.getInstance().lookup("infoPageTitles", "category"), 1.0)
        {
            selectRecordedCategoryCmd = new ICommandImp(selectRecordedCategoryCmdHandler);

            if (null != navigation.Message)
            {
                if (navigation.Message is CategoryVM) 
                {
                    CategoryVM selectedCategory = (CategoryVM)navigation.Message;
                    CurrentInfo.ParentId = selectedCategory.Id;
                    CurrentInfo.HierarchyDepth = selectedCategory.HierarchyDepth + 1;
                    CurrentInfo.ParentName = selectedCategory.Name;
                }
            }
        }
    }
}
