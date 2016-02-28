using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecords.Converters
{
    class NullRef2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //if (Visibility.Visible.Equals((Visibility)value))
              //  return new object();
            return null;
        }
    }
}
