

using System;
using Windows.UI.Xaml.Data;
namespace CommercialRecordSystem.Converters
{
    class SizeAdjustmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double actualSize = (double)value;
            double change = 0;
            if (null != parameter)
            {
                string paramStr = parameter.ToString();
                Double.TryParse(paramStr.Substring(1), out change);
                if (change > 0)
                {
                    switch (paramStr[0])
                    {
                        case '+':
                            actualSize += change;
                            break;
                        case '-':
                            actualSize -= change;
                            break;
                        case '.':
                            actualSize *= change;
                            break;
                        case '/':
                            actualSize /= change;
                            break;
                        default:
                            break;
                    }
                }
            }
            actualSize += change;
            if (actualSize >= 0)
                return actualSize;
            else
                return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double actualSize = (double)value;
            double change = 0;
            Double.TryParse(parameter.ToString(), out change);

            actualSize -= change;
            if (actualSize >= 0)
                return actualSize;
            else
                return 0.0;
        }
    }
}
