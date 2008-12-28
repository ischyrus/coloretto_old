using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using CardManagement.Coloretto;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for SidewaysStack.xaml
    /// </summary>
    public partial class SidewaysStack : Panel
    {
        #region Properties
        public static readonly DependencyProperty PileNumberProperty = DependencyProperty.Register("PileNumber", typeof(int), typeof(SidewaysStack), new PropertyMetadata(-1));

        public int PileNumber
        {
            get { return (int)GetValue(PileNumberProperty); }
            set { SetValue(PileNumberProperty, value); }
        }
        #endregion

        public CardCollection Cards
        {
            get { return (CardCollection)GetValue(CardsProperty); }
            set { SetValue(CardsProperty, value); }
        }

        public static readonly DependencyProperty CardsProperty =
            DependencyProperty.Register("Cards", typeof(CardCollection), typeof(SidewaysStack), new UIPropertyMetadata(null, CardsPropertyChanged));

        private static void CardsPropertyChanged(object o, DependencyPropertyChangedEventArgs args)
        {
            SidewaysStack stack = (SidewaysStack)o;
            CardCollection collection = (CardCollection)args.NewValue;
            if (collection.IsEmpty)
            {
                var _oldCards = stack.InternalChildren.OfType<UIElement>().Where(child => !(child is PilePlaceHolder)).ToList();
                foreach (var item in _oldCards)
                {
                    stack.Children.Remove(item);
                }
                return;
            }
            else
            {
                foreach (var groups in collection.Where(card => card.CardType == ColorettoCardTypes.Color).GroupBy(card => card.Color))
                {
                    int exist = stack.Children.OfType<PlayerHandCard>().Where(phc => phc.CardColor == groups.Key).Count();
                    int different = groups.Count() - exist;
                    if (different > 0)
                    {
                        PlayerHandCard card = new PlayerHandCard { CardColor = groups.Key };
                        PlayerHand.SetIsNew(card, true);
                        stack.Children.Add(card);
                        return;
                    }
                    else if (different < 0)
                    {
                        Debug.Fail("A color was removed?");
                    }
                    else
                    {
                        // no change in the number of this color
                    }
                }

                foreach (var group in collection.Where(card => card.CardType != ColorettoCardTypes.Color).OrderBy(card => card.CardType).GroupBy(card => card.CardType))
                {
                    if (group.Key == ColorettoCardTypes.Wild)
                    {
                        int exist = stack.Children.OfType<PlayerHandWildCard>().Count();
                        int differnet = group.Count() - exist;
                        if (differnet > 0)
                        {
                            PlayerHandWildCard card = new PlayerHandWildCard();
                            PlayerHand.SetIsNew(card, true);
                            stack.Children.Add(card);
                            return;
                        }
                        else if (differnet < 0)
                        {
                            Debug.Fail("A wild card was removed?");
                        }
                        else
                        {
                            // no change in number of wild cards
                        }
                    }
                    else if (group.Key == ColorettoCardTypes.Plus2)
                    {
                        int exist = stack.Children.OfType<PlayerHandPlusTwoCard>().Count();
                        int differnet = group.Count() - exist;
                        if (differnet > 0)
                        {
                            PlayerHandPlusTwoCard card = new PlayerHandPlusTwoCard();
                            PlayerHand.SetIsNew(card, true);
                            stack.Children.Add(card);
                            return;
                        }
                        else if (differnet < 0)
                        {
                            Debug.Fail("A wild card was removed?");
                        }
                        else
                        {
                            // no change in number of wild cards
                        }
                    }
                }
            }
            Debug.Fail("No card was added and the piles weren't cleared.");
        }


        public SidewaysStack()
        {
            InitializeComponent();
        }

        internal void ClearNewStatusOnCards()
        {
            foreach (DependencyObject existingCard in Children) { PlayerHand.SetIsNew(existingCard, false); }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size adjustedSize = new Size(finalSize.Width - 15, finalSize.Height);
            Size cardSize = adjustedSize.GoldenSize(4, Orientation.Horizontal);

            foreach (UIElement child in InternalChildren.OfType<PilePlaceHolder>())
            {
                child.Arrange(new Rect(new Point(0, 0), cardSize));
            }

            double x = cardSize.Width + 15;
            foreach (UIElement child in InternalChildren.OfType<UIElement>().Where(c => !(c is PilePlaceHolder)))
            {
                child.Arrange(new Rect(x, 0, cardSize.Width, cardSize.Height));
                x += cardSize.Width;
            }

            return new Size((cardSize.Width * 4) + 15, cardSize.Height);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size adjustedSize = new Size(availableSize.Width - 15, availableSize.Height);
            Size cardSize = adjustedSize.GoldenSize(4, Orientation.Horizontal);

            foreach (UIElement child in InternalChildren)
                child.Measure(cardSize);

            return new Size((cardSize.Width * 4) + 15, cardSize.Height);
        }
    }
}