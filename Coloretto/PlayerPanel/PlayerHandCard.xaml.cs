using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CardManagement.Coloretto;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for PlayerHandCard.xaml
    /// </summary>
    public partial class PlayerHandCard : UserControl
    {
        public static readonly DependencyProperty CardColorProperty =
            DependencyProperty.Register("CardColor", typeof(ColorettoCardColors), typeof(PlayerHandCard), new UIPropertyMetadata(ColorettoCardColors.None));

        /// <summary>
        /// 
        /// </summary>
        public ColorettoCardColors CardColor
        {
            get { return (ColorettoCardColors)GetValue(CardColorProperty); }
            set { SetValue(CardColorProperty, value); }
        }


        public PlayerHandCard()
        {
            InitializeComponent();
        }
    }
}
