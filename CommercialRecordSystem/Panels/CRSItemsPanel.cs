

using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
namespace CommercialRecordSystem.Panels
{
    class CRSItemsPanel : ItemsControl
    {
        #region Properties
        protected bool itemSourceDefined = false;

        private List<UIElement> children = new List<UIElement>();
        public List<UIElement> Children
        {
            get{
                return children;
            }
        }

        public enum ORIENTATION { Vertical, Horizontal };
        public ORIENTATION Orientation
        {
            get
            {
                return (ORIENTATION)GetValue(OrientationProperty);
            }
            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                "Orientation",
                typeof(ORIENTATION),
                typeof(CRSItemsPanel),
                new PropertyMetadata(ORIENTATION.Vertical, null)
            );

        public int GapSize
        {
            get
            {
                return (int)GetValue(GapSizeProperty);
            }
            set
            {
                SetValue(GapSizeProperty, value);
            }
        }

        public static readonly DependencyProperty GapSizeProperty =
            DependencyProperty.Register(
                "GapSize",
                typeof(int),
                typeof(CRSItemsPanel),
                new PropertyMetadata(0, null)
            );
        #endregion

        protected override Size ArrangeOverride(Size finalSize)
        {
            children = new List<UIElement>();
            if (null != this.ItemsSource && null != this.ItemTemplate)
                itemSourceDefined = true;

            foreach (UIElement child in this.ItemsPanelRoot.Children)
            {
                UIElement childBuff = child;
                if (itemSourceDefined)
                    childBuff = (UIElement)VisualTreeHelper.GetChild(childBuff, 0);
                
                children.Add(childBuff);
            }

            if (children.Count > 0)
            {
                double x = 0.0f;
                double y = 0.0f;
                int elementNum = children.Count;
                double elementWidth = (finalSize.Width - (elementNum - 1) * GapSize) / elementNum;
                for (int i = 0; i < children.Count; ++i)
                {
                    if (Visibility.Visible == children[i].Visibility)
                    {
                        if (ORIENTATION.Vertical.Equals(Orientation))
                        {
                            children[i].Arrange(new Rect(x, y, finalSize.Width, children[i].DesiredSize.Height));
                            y += children[i].DesiredSize.Height + (double)GapSize;
                        }
                        else
                        {
                            if (i == children.Count - 1)
                            {
                                x = (elementWidth + (double)GapSize - 0.2) * (elementNum - 1);
                                elementWidth = finalSize.Width - x;
                            }

                            children[i].Arrange(new Rect(x, y, elementWidth, children[i].DesiredSize.Height));
                            x += elementWidth + (double)GapSize;
                        }
                    }
                    //finalSize.Height = y + element.DesiredSize.Height;
                }
            }

            return finalSize;
        }
    }
}
