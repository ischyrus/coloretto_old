using System.Windows.Controls;
using System.Windows;
using CardManagement.Coloretto;
using System.Linq;
using System.Diagnostics;
using System.Windows.Media;
using System.Collections.Generic;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for PlayerHand.xaml
    /// </summary>
    public partial class PlayerHand : Panel
    {
        #region Dependency Properties
        public static readonly DependencyProperty CardsProperty =
            DependencyProperty.Register("Cards", typeof(CardCollection), typeof(PlayerHand), new PropertyMetadata(null, CardsPropertyChanged));

        public static readonly DependencyProperty IsNewProperty = DependencyProperty.RegisterAttached("IsNew", typeof(bool), typeof(PlayerHand), new PropertyMetadata(false));
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

        public static bool GetIsNew(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNewProperty);
        }

        public static void SetIsNew(DependencyObject obj, bool isNew)
        {
            obj.SetValue(IsNewProperty, isNew);
        }

        #region Private Methods
        private static void CardsPropertyChanged(object o, DependencyPropertyChangedEventArgs args)
        {
            PlayerHand playerHand = (PlayerHand)o;
            // TODO: Considering doing a diff or something less bulky.
            playerHand.Children.Clear();

            CardCollection cards = args.NewValue as CardCollection;
            CardCollection oldCards = args.OldValue as CardCollection;
            List<ColorettoCard> newCards = new List<ColorettoCard>(3);

            if (cards == null)
            {
                return;
            }
            else if (oldCards != null && cards.Count == oldCards.Count)
            {
                return;
            }
            else
            {
                // In order to highlight new cards we need to determine which ones are the new ones.
                for (int i = 0, j = 0; i < cards.Count; i++)
                {
                    if (j == oldCards.Count || cards[i] != oldCards[j])
                        newCards.Add(cards[i]);
                    else
                        j++;
                }
            }

            var colorGroups = cards.Where(c => c.CardType == ColorettoCardTypes.Color).GroupBy(c => c.Color).OrderByDescending(g => g.Count());
            foreach (IGrouping<ColorettoCardColors, ColorettoCard> colorGroup in colorGroups)
            {
                PlayerHandPile pile = new PlayerHandPile();
                foreach (var card in colorGroup)
                {
                    PlayerHandCard handCard = new PlayerHandCard { CardColor = card.Color };
                    pile.Children.Add(handCard);
                }

                int numberThatAreNew = newCards.Where(c => c.Color == colorGroup.Key).Count();
                for (int i = pile.Children.Count - 1; numberThatAreNew > 0 && i >= 0; i--, numberThatAreNew--) { SetIsNew(pile.Children[i], true); }

                playerHand.Children.Add(pile);
            }

            var specialGroups = cards.Where(c => c.CardType != ColorettoCardTypes.Color).GroupBy(c => c.CardType).OrderBy(g => g.Key);
            foreach (IGrouping<ColorettoCardTypes, ColorettoCard> cardGroup in specialGroups)
            {
                Debug.Assert(cardGroup.Key != ColorettoCardTypes.Unknown || cardGroup.Key != ColorettoCardTypes.LastCycle, "LastCycle and unknown card types should not be in the player's hand");
                PlayerHandPile pile = new PlayerHandPile();
                foreach (var card in cardGroup)
                {
                    UIElement specialCard = null;
                    if (cardGroup.Key == ColorettoCardTypes.Plus2)
                        specialCard = new PlayerHandPlusTwoCard();
                    else if (cardGroup.Key == ColorettoCardTypes.Wild)
                        specialCard = new PlayerHandWildCard();

                    pile.Children.Add(specialCard);
                }

                int numberThatAreNew = newCards.Where(c => c.CardType == cardGroup.Key).Count();
                for (int i = pile.Children.Count - 1; numberThatAreNew > 0 && i >= 0; i--, numberThatAreNew--) { SetIsNew(pile.Children[i], true); }

                playerHand.Children.Add(pile);
            }


        }
        #endregion
    }
}
