using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecordSystem.Converters
{
    class Int2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value || 0 == (int)value%2)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (Visibility.Visible.Equals((Visibility)value))
                return 1;
            return 0;
        }
    }
}
