using CommercialRecordSystem.Common;
using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.Panels
{
    public class CRSPanel : Panel
    {
        #region Properties
        #region Orientation
        public enum ORIENTATION { Vertical, Horizontal};
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
                typeof(CRSPanel),
                new PropertyMetadata(ORIENTATION.Vertical, null)
            );
        #endregion

        #region GapSize
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
                typeof(CRSPanel),
                new PropertyMetadata(0, null)
            );
        #endregion
        #endregion

        public CRSPanel()
        {
            GapSize = 0;
            Orientation = ORIENTATION.Vertical;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement element in this.Children)
            {
                element.Measure(availableSize);
            }
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Children.Count > 0)
            {
                double x = 0.0f;
                double y = 0.0f;
                int elementNum = this.Children.Count;
                double elementWidth = (finalSize.Width - (elementNum-1)*GapSize) / this.Children.Count;
                foreach (UIElement element in this.Children)
                {
                    if (ORIENTATION.Vertical.Equals(Orientation))
                    {
                        element.Arrange(new Rect(x, y, finalSize.Width, element.DesiredSize.Height));
                        y += element.DesiredSize.Height + (double)GapSize;
                    }
                    else
                    {
                        element.Arrange(new Rect(x, y, elementWidth, element.DesiredSize.Height));
                        x += elementWidth + (double)GapSize;
                    }
                    //finalSize.Height = y + element.DesiredSize.Height;
                }
            }

            return finalSize;
        }
    }
}
