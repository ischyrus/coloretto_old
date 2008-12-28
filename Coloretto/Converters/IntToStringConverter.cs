/*
 * Created by SharpDevelop.
 * User: sscherm
 * Date: 8/3/2008
 * Time: 12:31 AM
 */

using System;
using System.Windows.Data;

namespace Coloretto.Converters
{
	/// <summary>
	/// Description of IntToStringConverter.
	/// </summary>
	public class IntToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value.ToString();
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
