using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecordSystem.Converters
{
    class Boolean2IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
                return -1;
            else
            {
                Boolean boolValue = (Boolean)value;

                if (null != parameter && parameter.ToString().Equals("invert"))
                    boolValue = !boolValue;

                if (boolValue)
                    return 1;
                else 
                    return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
                return false;
            else
            {
                int intValue = (int)value;
                if (-1 == intValue)
                    return null;
                else if (0 == intValue)
                    return false;
                else
                    return true;
            }
        }
    }
}