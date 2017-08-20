using System;
using System.Globalization;
using System.Windows.Data;
using MyLrcMaker.Extension;

namespace MyLrcMaker.Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleToLrcFormatConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return TimeSpan.FromMilliseconds((double) value).ToLrcFormat();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}