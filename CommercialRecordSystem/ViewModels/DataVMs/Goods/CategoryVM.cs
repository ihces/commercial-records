using CommercialRecordSystem.Models.Goods;
using Windows.UI.Xaml;

namespace CommercialRecordSystem.ViewModels.DataVMs.Goods
{
    class CategoryVM : InfoDataVMBase<Category>
    {
        public int parentId;
        public int ParentId
        {
            get
            {
                return parentId;
            }
            set
            {
                if (parentId != value)
                {
                    CategoryVM categoryBuff = new CategoryVM();
                    categoryBuff.get(value);
                    ParentName = categoryBuff.Name;
                }

                parentId = value;
                RaisePropertyChanged("ParentId");
            }
        }

        private string parentName;
        public string ParentName
        {
            get
            {
                return parentName;
            }
            set
            {
                parentName = value;
                RaisePropertyChanged("ParentName");
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = UpperCaseFirst(value);

                RaisePropertyChanged("Name");
            }
        }

        private string details;
        public string Details
        {
            get
            {
                return details;
            }
            set
            {
                details = value;
                RaisePropertyChanged("Details");
            }
        }

        public int hierarchyDepth = 1;
        public int HierarchyDepth
        {
            get
            {
                return hierarchyDepth;
            }
            set
            {
                hierarchyDepth = value;
                HierarchyMargin = new Thickness(hierarchyDepth * 40, 0, 0, 0);
                RaisePropertyChanged("HierarchyDepth");
            }
        }

        private Thickness hierarchyMargin;
        public Thickness HierarchyMargin
        {
            get
            {
                return hierarchyMargin;
            }
            set
            {
                hierarchyMargin = value;
                RaisePropertyChanged("HierarchyMargin");
            }
        }

        public CategoryVM()
            : base(App.CategoryImgFolder)
        { }
    }
}
