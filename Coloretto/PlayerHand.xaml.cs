using System.Windows.Controls;
using System.Windows;
using CardManagement.Coloretto;
using System.Linq;
using System.Diagnostics;
using System.Windows.Media;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for PlayerHand.xaml
    /// </summary>
    public partial class PlayerHand : Panel
    {
        #region Dependency Properties
        public static readonly DependencyProperty CardsProperty =
            DependencyProperty.Register("Cards", typeof(CardManagement.Coloretto.CardCollection), typeof(PlayerHand), new PropertyMetadata(null, CardsPropertyChanged));
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the cards in the player's hand
        /// </summary>
        public CardManagement.Coloretto.CardCollection Cards
        {
            get { return (CardManagement.Coloretto.CardCollection)GetValue(CardsProperty); }
            set { SetValue(CardsProperty, value); }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a default instance of PlayerHand
        /// </summary>
        public PlayerHand()
        {
            InitializeComponent();
        }
        #endregion

        #region Measure and Arrange
        private const double GoldenRatio = 1.6180339887;
        private double spaceBetweenCardsTops = 25d;
        private Size DefaultSize = new Size(200d, 323.606798d);

        protected override Size MeasureOverride(Size constraint)
        {
            if (double.IsInfinity(constraint.Width))
            {
                double targetHeight = constraint.Height - (spaceBetweenCardsTops * 7);
                double targetWidth = targetHeight / GoldenRatio;
                Size targetSize = new Size(targetWidth, targetHeight);

                double width = 0d;
                foreach (UIElement item in InternalChildren)
                {
                    item.Measure(targetSize);
                    width += item.DesiredSize.Width;
                }
                return new Size(width, targetHeight);
            }
            else if (double.IsInfinity(constraint.Height))
            {
                double targetWidth = constraint.Width / InternalChildren.Count;
                double targetHeight = (targetWidth * GoldenRatio) + (spaceBetweenCardsTops * 7);
                Size targetSize = new Size(targetWidth, targetHeight);

                double height = 0d;
                foreach (UIElement item in InternalChildren)
                {
                    item.Measure(targetSize);
                    height += item.DesiredSize.Height;
                }
                return new Size(height, targetWidth);
            }

            Size itemSize = new Size(constraint.Width / InternalChildren.Count, constraint.Height);
            foreach (UIElement item in InternalChildren)
            {
                item.Measure(itemSize);
            }
            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size itemSize = new Size(finalSize.Width / InternalChildren.Count, finalSize.Height);
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Arrange(new Rect(new Point(i * itemSize.Width, 0), itemSize));
            }
            return finalSize;
        } 
        #endregion

        #region Private Methods
        private static void CardsPropertyChanged(object o, DependencyPropertyChangedEventArgs args)
        {
            PlayerHand playerHand = (PlayerHand)o;
            // TODO: Considering doing a diff or something less bulky.
            playerHand.Children.Clear();

            CardCollection cards = args.NewValue as CardCollection;
            if (cards == null)
                return;


            var colorGroups = cards.GroupBy(c => c.Color).OrderByDescending(g => g.Count());
            foreach (IGrouping<ColorettoCardColors, ColorettoCard> colorGroup in colorGroups)
            {
                if (colorGroup.Key != ColorettoCardColors.None)
                {
                    PlayerHandPile pile = new PlayerHandPile();
                    foreach (var card in colorGroup)
                    {
                        Brush brush = Brushes.Gold;
                        switch (card.Color)
                        {
                            case ColorettoCardColors.Blue:
                                brush = Brushes.Blue;
                                break;
                            case ColorettoCardColors.Brown:
                                brush = Brushes.Brown;
                                break;
                            case ColorettoCardColors.Gray:
                                brush = Brushes.Gray;
                                break;
                            case ColorettoCardColors.Green:
                                brush = Brushes.Green;
                                break;
                            case ColorettoCardColors.Orange:
                                brush = Brushes.Orange;
                                break;
                            case ColorettoCardColors.Pink:
                                brush = Brushes.Pink;
                                break;
                            case ColorettoCardColors.Yellow:
                                brush = Brushes.Yellow;
                                break;
                            case ColorettoCardColors.None:
                            default:
                                break;
                        }
                        pile.Children.Add(new PlayerHandCard { CardColor = brush });
                    }
                    playerHand.Children.Add(pile);
                }
            }
        }
        #endregion
    }
}
