using System;
using System.Globalization;
using System.Windows.Data;

namespace MyLrcMaker.Converters
{
    [ValueConversion(typeof(double), typeof(DateTime))]
    public class DoubleToTimeSpanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double doubleValue;
            if (value != null && double.TryParse(value.ToString(), out doubleValue))
            {
                return TimeSpan.FromMilliseconds(doubleValue);
            }

            return new TimeSpan();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                return System.Convert.ToDouble(((TimeSpan) value).Milliseconds);
            }

            return 0;
        }

        #endregion
    }
}