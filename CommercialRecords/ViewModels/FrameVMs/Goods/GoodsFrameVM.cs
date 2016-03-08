using CommercialRecords.Common;
using CommercialRecords.Models.Goods;
using CommercialRecords.ViewModels.DataVMs;
using CommercialRecords.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;
using CommercialRecords.Views.Goods;
using CommercialRecords.ViewModels.DataVMs.Goods;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace CommercialRecords.ViewModels.FrameVMs.Goods
{
    class GoodsFrameVM : FrameVMBase
    {
        public static readonly int DEFAULT_VIEW = 0;
        public static readonly int SELECT_GOOD = 1;
        public static readonly int SELECT_BRAND = 2;
        public static readonly int SELECT_CATEGORY = 3;
        private int viewPurpose = DEFAULT_VIEW;

        private readonly List<string> good_OrderBy_List = CrsDictionary.getInstance().getValues("goodOrderBy").ToList();
        private readonly List<string> category_OrderBy_List = CrsDictionary.getInstance().getValues("categoryOrderBy").ToList();
        private enum CategoryOrderType { ALPHABETICAL, HIERARCHICAL };

        private List<BrandVM> brandsList;
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
                    ShowBrands = false;
                    ShowCategories = false;
                }
                RaisePropertyChanged("ShowGoods");
            }
        }

        private OrderedListVM<BrandVM, Brand> brandOrderedList;
        public OrderedListVM<BrandVM, Brand> BrandOrderedList
        {
            get
            {
                return brandOrderedList;
            }
            set
            {
                brandOrderedList = value;
                RaisePropertyChanged("BrandOrderedList");
            }
        }
        
        private bool showBrands;
        public bool ShowBrands
        {
            get
            {
                return showBrands;
            }
            set
            {
                showBrands = value;
                if (showBrands)
                {
                    ShowGoods = false;
                    ShowCategories = false;
                }
                RaisePropertyChanged("ShowBrands");
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
                    ShowBrands = false;
                }
                RaisePropertyChanged("ShowCategories");
            }
        }

        private ObservableCollection<string> displayList = new ObservableCollection<string>(CrsDictionary.getInstance().getKeys("goodsSelectList"));
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
                        setBrands(typeof(BrandVM).GetRuntimeProperty("Name"), true);
                        ShowBrands = true;
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

        private BrandVM selectedBrand;
        public BrandVM SelectedBrand
        {
            get
            {
                return selectedBrand;
            }
            set
            {
                selectedBrand = value;
                RaisePropertyChanged("SelectedBrand");
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

        private readonly ICommand tappedBrandCmd;
        public ICommand TappedBrandCmd
        {
            get
            {
                return tappedBrandCmd;
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
                    setBrands(typeof(BrandVM).GetRuntimeProperty("Name"), true);
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

        private void tappedBrandCmdHandler(object parameter)
        {
            if (DEFAULT_VIEW == viewPurpose)
            {
                navigation.Navigate<BrandInfo>(SelectedBrand.Id);
            }
            else if (SELECT_BRAND == viewPurpose)
            {
                navigation.GoBack(SelectedBrand);
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
            tappedBrandCmd = new ICommandImp(tappedBrandCmdHandler);
            tappedCategoryCmd = new ICommandImp(tappedCategoryCmdHandler);

            if (null != navigation.Message)
            {
                if (navigation.Back.Is<GoodInfo>())
                {
                    viewPurpose = (int)navigation.Message;
                    if (SELECT_BRAND == viewPurpose)
                    {
                        SelectedDisplayListIndex = 1;
                    }
                    else if (SELECT_CATEGORY == viewPurpose)
                    {
                        SelectedDisplayListIndex = 2;
                    }
                }
                else if (navigation.Back.Is<CategoryInfo>())
                {
                    ignoredCategoryId = (int)navigation.Message;
                    viewPurpose = SELECT_CATEGORY;
                    SelectedDisplayListIndex = 2;
                }

                PageReadOnly = true;
            }
            else if (navigation.Forward == null)
            {
                SelectedDisplayListIndex = 0;
            }
        }

        private async Task setBrands(PropertyInfo orderProperty,
            bool alphaNumericOrder = false, bool reverse = false,
            Expression<Func<Brand, bool>> whereClause = null,
            List<Expression<Func<Brand, object>>> orderByClauses = null)
        {
            brandsList = await BrandVM.getList<BrandVM>(whereClause, orderByClauses);
            if (!string.IsNullOrWhiteSpace(QueryText))
                brandsList = brandsList.Where(c => c.Name.ToLower().Contains(QueryText.ToLower())).ToList();

            BrandOrderedList = new OrderedListVM<BrandVM, Brand>();
            BrandOrderedList.FillList(brandsList, orderProperty, alphaNumericOrder, reverse);
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
            List<CategoryVM> categoriesList = await CategoryVM.getList<CategoryVM>();

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
