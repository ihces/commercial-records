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

                bool invert;
                bool equality;
                string paramBuff;
                foreach (string param in parameters)
                {
                    invert = false;
                    paramBuff = param;
                    if (param.Length > 1 && param[0] == '~')
                    {
                        paramBuff = paramBuff.Substring(1);
                        invert = true;
                    }

                    equality = (0 == value.ToString().CompareTo(paramBuff));

                    if (equality ^ invert)
                    {
                        return Visibility.Visible;
                    }
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
