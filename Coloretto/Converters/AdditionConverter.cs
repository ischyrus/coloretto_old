using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Coloretto.Converters
{
    /// <summary>
    /// A multivalue converter that will return the sum of values.
    /// </summary>
    public class AdditionConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double sum = 0d;
            for (int i = 0; i < values.Length; i++)
            {
                double value = (double)values[i];
                if (double.IsNaN(value) == false)
                    sum += (double)values[i];
            }
            return sum;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
