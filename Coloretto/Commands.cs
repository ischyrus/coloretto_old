using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Coloretto
{
    public static class Commands
    {
        public static readonly RoutedCommand DrawCardCommand = new RoutedCommand("DrawCard", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.Space, ModifierKeys.Control) });
        public static readonly RoutedCommand SelectPile1Command = new RoutedCommand("SelectPile1", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.D1, ModifierKeys.Alt) });
        public static readonly RoutedCommand SelectPile2Command = new RoutedCommand("SelectPile2", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.D2, ModifierKeys.Alt) });
        public static readonly RoutedCommand SelectPile3Command = new RoutedCommand("SelectPile3", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.D3, ModifierKeys.Alt) });
        public static readonly RoutedCommand SelectPile4Command = new RoutedCommand("SelectPile4", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.D4, ModifierKeys.Alt) });
        public static readonly RoutedCommand SelectPile5Command = new RoutedCommand("SelectPile5", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.D5, ModifierKeys.Alt) });
    }
}
