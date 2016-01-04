using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Goods;
using CommercialRecordSystem.ViewModels.DataVMs;
using CommercialRecordSystem.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;
using CommercialRecordSystem.Views.Goods;
using CommercialRecordSystem.ViewModels.DataVMs.Goods;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class GoodsFrameVM : FrameVMBase
    {
        public static readonly int DEFAULT_VIEW = 0;
        public static readonly int SELECT_GOOD = 1;
        public static readonly int SELECT_FIRM = 2;
        public static readonly int SELECT_CATEGORY = 3;
        private int viewPurpose = DEFAULT_VIEW;

        private readonly string[] good_OrderBy_List = new string[] { "Alfabetik Sıralı", "Markaya Göre Sıralı", "Kategoriye Göre Sıralı" };
        private readonly string[] category_OrderBy_List = new string[] { "Hiyerarşik Sıralı", "Alfabetik Sıralı" };
        private enum CategoryOrderType { ALPHABETICAL, HIERARCHICAL };

        private List<BrandVM> firmsList;
        private List<GoodVM> goodsList;
        private int ignoredCategoryId = 0;

        #region Properties
        private string queryText = string.Empty;
        public string QueryText
        {
            get
            {
                return queryText;
            }
            set
            {
                queryText = value;
                RaisePropertyChanged("QueryText");
            }
        }

        private OrderedListVM<GoodVM, Good> goodOrderedList;
        public OrderedListVM<GoodVM, Good> GoodOrderedList
        {
            get
            {
                return goodOrderedList;
            }
            set
            {
                goodOrderedList = value;
                RaisePropertyChanged("GoodOrderedList");
            }
        }

        private bool showGoods;
        public bool ShowGoods
        {
            get
            {
                return showGoods;
            }
            set
            {
                showGoods = value;
                if (showGoods)
                {
                    OrderByCriterias = new ObservableCollection<string>(good_OrderBy_List);
                    ShowFirms = false;
                    ShowCategories = false;
                }
                RaisePropertyChanged("ShowGoods");
            }
        }

        private OrderedListVM<BrandVM, Firm> firmOrderedList;
        public OrderedListVM<BrandVM, Firm> FirmOrderedList
        {
            get
            {
                return firmOrderedList;
            }
            set
            {
                firmOrderedList = value;
                RaisePropertyChanged("FirmOrderedList");
            }
        }

        private bool showDisplayList = true;
        public bool ShowDisplayList
        {
            get
            {
                return showDisplayList;
            }
            set
            {
                showDisplayList = value;
                RaisePropertyChanged("ShowDisplayList");
            }
        }

        private bool showFirms;
        public bool ShowFirms
        {
            get
            {
                return showFirms;
            }
            set
            {
                showFirms = value;
                if (showFirms)
                {
                    ShowGoods = false;
                    ShowCategories = false;
                }
                RaisePropertyChanged("ShowFirms");
            }
        }

        private ObservableCollection<CategoryVM> categoryList;
        public ObservableCollection<CategoryVM> CategoryList
        {
            get
            {
                return categoryList;
            }
            set
            {
                categoryList = value;
                RaisePropertyChanged("CategoryList");
            }
        }

        private bool showCategories;
        public bool ShowCategories
        {
            get
            {
                return showCategories;
            }
            set
            {
                showCategories = value;
                if (showCategories)
                {
                    OrderByCriterias = new ObservableCollection<string>(category_OrderBy_List);
                    ShowGoods = false;
                    ShowFirms = false;
                }
                RaisePropertyChanged("ShowCategories");
            }
        }

        private ObservableCollection<string> displayList;
        public ObservableCollection<string> DisplayList
        {
            get
            {
                return displayList;
            }
            set
            {
                displayList = value;
                RaisePropertyChanged("DisplayList");
            }
        }

        private int selectedDisplayListIndex;
        public int SelectedDisplayListIndex
        {
            get
            {
                return selectedDisplayListIndex;
            }
            set
            {
                selectedDisplayListIndex = value;
                switch (selectedDisplayListIndex)
                {
                    case 0:
                        OrderByCriterias = new ObservableCollection<string>(good_OrderBy_List);
                        ShowGoods = true;
                        ShowOrderByCriterias = true;
                        break;
                    case 1:
                        setFirms(typeof(BrandVM).GetRuntimeProperty("Name"), true);
                        ShowFirms = true;
                        ShowOrderByCriterias = false;
                        break;
                    case 2:
                        OrderByCriterias = new ObservableCollection<string>(category_OrderBy_List);
                        ShowCategories = true;
                        ShowOrderByCriterias = true;
                        break;
                }
                OrderByCriteriaIndex = 0;
                RaisePropertyChanged("SelectedDisplayListIndex");
            }
        }
        private ObservableCollection<string> orderByCriterias;
        public ObservableCollection<string> OrderByCriterias
        {
            get
            {
                return orderByCriterias;
            }
            set
            {
                orderByCriterias = value;
                RaisePropertyChanged("OrderByCriterias");
            }
        }

        private bool showOrderByCriterias = true;
        public bool ShowOrderByCriterias
        {
            get
            {
                return showOrderByCriterias;
            }
            set
            {
                showOrderByCriterias = value;
                RaisePropertyChanged("ShowOrderByCriterias");
            }
        }

        private int orderByCriteriaIndex = -1;
        public int OrderByCriteriaIndex
        {
            get
            {
                return orderByCriteriaIndex;
            }
            set
            {
                orderByCriteriaIndex = value;

                if (ShowGoods)
                {
                    switch (orderByCriteriaIndex)
                    {
                        case 0:
                            setGoods(typeof(GoodVM).GetRuntimeProperty("Name"), true);
                            break;
                        case 1:
                            setGoods(typeof(GoodVM).GetRuntimeProperty("BrandName"));
                            break;
                        case 2:
                            setGoods(typeof(GoodVM).GetRuntimeProperty("CategoryName"));
                            break;
                    }
                    RaisePropertyChanged("GoodOrderedList");
                }
                else if (ShowCategories)
                {
                    switch (orderByCriteriaIndex)
                    {
                        case 0:
                            setCategories(CategoryOrderType.HIERARCHICAL, ignoredCategoryId);
                            break;
                        case 1:
                            setCategories(CategoryOrderType.ALPHABETICAL, ignoredCategoryId);
                            break;
                    }
                }
                RaisePropertyChanged("OrderByCriteriaIndex");
            }
        }

        private GoodVM selectedGood;
        public GoodVM SelectedGood
        {
            get
            {
                return selectedGood;
            }
            set
            {
                selectedGood = value;
                RaisePropertyChanged("SelectedGood");
            }
        }

        private BrandVM selectedFirm;
        public BrandVM SelectedFirm
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

        private CategoryVM selectedCategory;
        public CategoryVM SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged("SelectedCategory");
            }
        }
        #endregion
        #region Commands
        private readonly ICommand querySubmittedCmd;
        public ICommand QuerySubmittedCmd
        {
            get
            {
                return querySubmittedCmd;
            }
        }

        private readonly ICommand tappedFirmCmd;
        public ICommand TappedFirmCmd
        {
            get
            {
                return tappedFirmCmd;
            }
        }

        private readonly ICommand tappedGoodCmd;
        public ICommand TappedGoodCmd
        {
            get
            {
                return tappedGoodCmd;
            }
        }

        private ICommand addButtonCmd = null;
        public ICommand AddButtonCmd
        {
            get
            {
                return addButtonCmd;
            }
        }

        private readonly ICommand tappedCategoryCmd;
        public ICommand TappedCategoryCmd
        {
            get
            {
                return tappedCategoryCmd;
            }
        }
        #endregion
        #region CommandHandler
        private void querySubmittedCmd_execute(object obj)
        {
            switch (SelectedDisplayListIndex)
            {
                case 0:
                    OrderByCriteriaIndex = 0;
                    break;
                case 1:
                    setFirms(typeof(BrandVM).GetRuntimeProperty("Name"), true);
                    break;
                case 2:
                    OrderByCriteriaIndex = 1;
                    break;
            }
        }

        private void addButtonCmd_execute(object obj)
        {
            switch (SelectedDisplayListIndex)
            {
                case 0:
                    Navigation.Navigate<GoodInfo>();
                    break;
                case 1:
                    Navigation.Navigate<BrandInfo>();
                    break;
                case 2:
                    Navigation.Navigate<CategoryInfo>();
                    break;
            }
        }

        private void tappedGoodCmdHandler(object parameter)
        {
            navigation.Navigate<GoodInfo>(SelectedGood.Id);
        }

        private void tappedFirmCmdHandler(object parameter)
        {
            if (DEFAULT_VIEW == viewPurpose)
            {
                navigation.Navigate<BrandInfo>(SelectedFirm.Id);
            }
            else if (SELECT_FIRM == viewPurpose)
            {
                navigation.GoBack(SelectedFirm);
            }
        }

        private void tappedCategoryCmdHandler(object parameter)
        {
            if (DEFAULT_VIEW == viewPurpose)
            {
                navigation.Navigate<CategoryInfo>(SelectedCategory.Id);
            }
            else if (SELECT_CATEGORY == viewPurpose)
            {
                navigation.GoBack(SelectedCategory);
            }
        }
        #endregion

        public GoodsFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            querySubmittedCmd = new ICommandImp(querySubmittedCmd_execute);
            addButtonCmd = new ICommandImp(addButtonCmd_execute);
            tappedGoodCmd = new ICommandImp(tappedGoodCmdHandler);
            tappedFirmCmd = new ICommandImp(tappedFirmCmdHandler);
            tappedCategoryCmd = new ICommandImp(tappedCategoryCmdHandler);

            if (null != navigation.Message)
            {
                if (navigation.Back.PageType.Equals(typeof(GoodInfo)))
                {
                    viewPurpose = (int)navigation.Message;
                    if (SELECT_FIRM == viewPurpose)
                    {
                        PageTitle = "Marka Seç";
                        ShowOrderByCriterias = false;
                        ShowFirms = true;
                        setFirms(typeof(BrandVM).GetRuntimeProperty("Name"), true);
                    }
                    else if (SELECT_CATEGORY == viewPurpose)
                    {
                        PageTitle = "Kategori Seç";
                        ShowCategories = true;
                        setCategories();
                    }
                }
                else if (navigation.Back.PageType.Equals(typeof(CategoryInfo)))
                {
                    ignoredCategoryId = (int)navigation.Message;
                    viewPurpose = SELECT_CATEGORY;
                    PageTitle = "Kategori Seç";
                    ShowCategories = true;
                    setCategories(CategoryOrderType.ALPHABETICAL, ignoredCategoryId);
                }

                ShowDisplayList = false;
            }
            else if (navigation.Forward == null)
            {
                DisplayList = new ObservableCollection<string>();
                DisplayList.Add("Ürünler");
                DisplayList.Add("Firmalar");
                DisplayList.Add("Kategoriler");

                SelectedDisplayListIndex = 0;
                ShowPageTitle = false;
            }
        }

        private async Task setFirms(PropertyInfo orderProperty,
            bool alphaNumericOrder = false, bool reverse = false,
            Expression<Func<Firm, bool>> whereClause = null,
            List<Expression<Func<Firm, object>>> orderByClauses = null)
        {
            firmsList = await BrandVM.getList<BrandVM>(whereClause, orderByClauses);
            if (!string.IsNullOrWhiteSpace(QueryText))
                firmsList = firmsList.Where(c => c.Name.ToLower().Contains(QueryText.ToLower())).ToList();

            FirmOrderedList = new OrderedListVM<BrandVM, Firm>();
            FirmOrderedList.FillList(firmsList, orderProperty, alphaNumericOrder, reverse);
        }

        private async Task setGoods(PropertyInfo orderProperty,
            bool alphaNumericOrder = false, bool reverse = false,
            Expression<Func<Good, bool>> whereClause = null,
            List<Expression<Func<Good, object>>> orderByClauses = null)
        {
            goodsList = await GoodVM.getList<GoodVM>(whereClause, orderByClauses);

            if (!string.IsNullOrWhiteSpace(QueryText))
                goodsList = goodsList.Where(c => c.Name.ToLower().Contains(QueryText.ToLower())).ToList();

            GoodOrderedList = new OrderedListVM<GoodVM, Good>();

            GoodOrderedList.FillList(goodsList, orderProperty, alphaNumericOrder, reverse);
        }

        private async Task setCategories(CategoryOrderType orderType = CategoryOrderType.HIERARCHICAL,
            int ignoredCatId = 0)
        {
            List<CategoryVM> categoriesList = await CategoryVM.getList<CategoryVM>(null, null);

            int depth = 0;
            if (orderType.Equals(CategoryOrderType.HIERARCHICAL))
            {
                depth = 1;
                categoriesList = categoriesList.OrderBy(c => c.Name).ToList();
            }


            categoriesList = orderCategoryListByHier(categoriesList.OrderBy(c => c.Name).ToList(), depth, ignoredCatId);

            if (orderType.Equals(CategoryOrderType.ALPHABETICAL))
            {
                if (!string.IsNullOrWhiteSpace(QueryText))
                    categoriesList = categoriesList.Where(c => c.Name.ToLower().Contains(QueryText.ToLower())).ToList();

                categoriesList = categoriesList.OrderBy(c => c.Name).ToList();
            }

            CategoryList = new ObservableCollection<CategoryVM>(categoriesList);
        }

        private static List<CategoryVM> orderCategoryListByHier(List<CategoryVM> categoryList,
            int depth = 0, int ignoredCatId = 0, int parentId = 0)
        {
            List<CategoryVM> orderedList = new List<CategoryVM>();

            for (int i = 0; i < categoryList.Count; ++i)
            {
                if (ignoredCatId != categoryList[i].Id && parentId == categoryList[i].ParentId)
                {
                    CategoryVM categoryBuff = categoryList[i];
                    if (0 == depth)
                        categoryBuff.HierarchyDepth = 1;
                    else
                        categoryBuff.HierarchyDepth = depth;

                    List<CategoryVM> subCategories = orderCategoryListByHier(categoryList, depth == 0 ? 0 : depth + 1, ignoredCatId, categoryBuff.Id);
                    orderedList.Add(categoryBuff);
                    if (0 < subCategories.Count)
                        orderedList.AddRange(subCategories);
                }
            }
            return orderedList;
        }
    }
}
