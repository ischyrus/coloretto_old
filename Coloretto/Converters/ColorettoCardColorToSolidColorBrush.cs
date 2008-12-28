using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using CardManagement.Coloretto;
using System.Windows.Media;

namespace Coloretto.Converters
{
    public class ColorettoCardColorToSolidColorBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ColorettoCardColors cardColor = (ColorettoCardColors)value;
            switch (cardColor)
            {
                case ColorettoCardColors.Blue:
                    return new SolidColorBrush(Colors.Blue);
                case ColorettoCardColors.Brown:
                    return new SolidColorBrush(Colors.Brown);
                case ColorettoCardColors.Gray:
                    return new SolidColorBrush(Colors.Gray);
                case ColorettoCardColors.Green:
                    return new SolidColorBrush(Colors.Green);
                case ColorettoCardColors.Orange:
                    return new SolidColorBrush(Colors.Orange);
                case ColorettoCardColors.Pink:
                    return new SolidColorBrush(Colors.Pink);
                case ColorettoCardColors.Yellow:
                    return new SolidColorBrush(Colors.Yellow);
                case ColorettoCardColors.None:
                default:
                    return new SolidColorBrush(Colors.Red);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
