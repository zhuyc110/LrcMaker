using System;
using System.Globalization;
using System.Windows.Data;
using MyLrcMaker.Extension;

namespace MyLrcMaker.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public class TimeSpanToLrcFormatConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                return ((TimeSpan) value).ToLrcFormat();
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