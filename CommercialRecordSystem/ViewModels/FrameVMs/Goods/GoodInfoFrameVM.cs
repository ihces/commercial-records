﻿using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
using System;
using System.Windows.Input;
using CommercialRecordSystem.Views.Goods;
using CommercialRecordSystem.ViewModels.DataVMs.Goods;
using System.Collections.ObjectModel;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
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
                else if (navigation.Message is BrandVM)
                {
                    BrandVM selectedFirm = (BrandVM)navigation.Message;
                    CurrentInfo.FirmId = selectedFirm.Id;
                }
            }
        }
    }
}
