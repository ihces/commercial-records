using System;
using System.Globalization;
using Windows.UI.Xaml.Data;


namespace CommercialRecordSystem.Converters
{
    class Int2MeasureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string measure;
            switch((int)value)
            {
                case 0:
                    measure = "Adet";
                    break;
                case 1:
                    measure = "Kg";
                    break;
                case 2:
                    measure = "Torba";
                    break;
                case 3:
                    measure = "Tnk";
                    break;
                default:
                    measure = (string)value;
                    break;
            }
            return measure;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            int measure;
            switch ((string)value)
            {
                case "Adet":
                    measure = 0;
                    break;
                case "Kg":
                    measure = 1;
                    break;
                case "Torba":
                    measure = 2;
                    break;
                case "Tnk":
                    measure = 3;
                    break;
                default:
                    measure = 0;
                    break;
            }
            return measure;
        }
    }
}
