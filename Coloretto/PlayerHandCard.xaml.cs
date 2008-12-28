using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for PlayerHandCard.xaml
    /// </summary>
    public partial class PlayerHandCard : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public Brush CardColor
        {
            get { return (Brush)GetValue(CardColorProperty); }
            set { SetValue(CardColorProperty, value); }
        }

        public static readonly DependencyProperty CardColorProperty =
            DependencyProperty.Register("CardColor", typeof(Brush), typeof(PlayerHandCard), new UIPropertyMetadata(new SolidColorBrush(Colors.Black)));

        public PlayerHandCard()
        {
            InitializeComponent();
        }
    }
}
