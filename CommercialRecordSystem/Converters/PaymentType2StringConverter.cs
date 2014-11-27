using CommercialRecordSystem.Models;
using System;
using Windows.UI.Xaml.Data;
namespace CommercialRecordSystem.Converters
{
    class PaymentType2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str;
            switch ((int)value)
            {
                case 0:
                    str = "Nakit";
                    break;
                case 1:
                    str = "Kredi Kartı";
                    break;
                case 2:
                    str = "Diğer";
                    break;
                default:
                    str = (string)value;
                    break;
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            PaymentEntry.TYPE type;
            switch ((string)value)
            {
                case "Nakit":
                    type = PaymentEntry.TYPE.CASH;
                    break;
                case "Kredi Kartı":
                    type = PaymentEntry.TYPE.CREDIT_CARD;
                    break;
                case "Diğer":
                    type = PaymentEntry.TYPE.OTHER;
                    break;
                default:
                    type = PaymentEntry.TYPE.CASH;
                    break;
            }
            return type;
        }
    }
}
