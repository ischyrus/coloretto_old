using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Coloretto.Converters
{
    /// <summary>
    /// Using the converter parameter as the format string, this converter will allow multiple values to be
    /// bound to a single format string.
    /// </summary>
    public class StringFormatConverter : IValueConverter, IMultiValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string formatSring = parameter as string;
            if (string.IsNullOrEmpty(formatSring))
                return string.Empty;
            else
                return string.Format(formatSring, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string formatSring = parameter as string;
            if (string.IsNullOrEmpty(formatSring))
                return string.Empty;
            else
                return string.Format(formatSring, values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
