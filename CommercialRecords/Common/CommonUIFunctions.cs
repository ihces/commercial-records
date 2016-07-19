using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CommercialRecords.Common
{
    class CommonUIFunctions
    {
        public static bool isVisible(FrameworkElement element)
        {
            FrameworkElement parent = element.Parent as FrameworkElement;

            if (element.Visibility.Equals(Visibility.Collapsed) 
                || null == parent 
                || parent.Visibility.Equals(Visibility.Collapsed))
                return false;
            else {
                if (parent.Equals(getPageMainPanel()))
                    return true;

                return isVisible(parent);
            }
        }

        public static FrameworkElement Parent(DependencyObject child)
        {
            return VisualTreeHelper.GetParent(child) as FrameworkElement;
        }

        public static Panel getPageMainPanel()
        {
            return (Panel)((Page)(((Frame)Window.Current.Content).Content)).Content;
        }
    }
}
