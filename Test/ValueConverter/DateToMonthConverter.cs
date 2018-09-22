using System;
using System.Globalization;
using Xamarin.Forms;

namespace Test.ValueConverter
{
    public class DateToMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is DateTime)) return string.Empty;

            var result = string.Empty;
            var difference = (DateTime)value - DateTime.Today;

            return difference.TotalDays < 1 ?
                             $"{Math.Abs(difference.TotalDays):0} days in the past" :
                             $"{difference.TotalDays:0} days in the future";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Today;
        }
    }
}
