using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecords.Converters
{
    class Compare2AssignVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            
            if (null != value && null != parameter)
            {
                string[] parameters = parameter.ToString().Split('|');
                bool invert = false;
                if (parameters.Length > 1 && parameters[1] == "Invert")
                    invert = true;

                if (0 == value.ToString().CompareTo(parameters[0].ToString())){
                    if (invert)
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }
                else
                {
                    if (invert)
                        return Visibility.Visible;
                    else
                        return Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
