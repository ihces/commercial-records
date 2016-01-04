using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecordSystem.Converters
{
    class Boolean2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool visible = false;
            if (null != value)
                visible = (bool)value;

            if (null != parameter && parameter.ToString().Equals("invert"))
                visible = !visible;

            if (visible)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (Visibility.Visible.Equals((Visibility)value))
                return true;
            return false;
        }
    }
}