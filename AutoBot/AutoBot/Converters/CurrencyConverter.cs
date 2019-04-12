using Dice.Client.Web;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AutoBot.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Currencies)value + 1;
        }
    }
}
