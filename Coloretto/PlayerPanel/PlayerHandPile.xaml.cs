using System;
using System.Collections.Generic;
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
    /// Interaction logic for PlayerHandPile.xaml
    /// </summary>
    public partial class PlayerHandPile : Panel
    {
        public PlayerHandPile()
        {
            InitializeComponent();
        }

        private const double GoldenRatio = 1.6180339887;
        private double spaceBetweenCardsTops = 25d;

        /// <summary>
        /// Handle the measurement of the player's hand. This is mainly to tell the
        /// children what their sizes can be.
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
        	Size cardSize = CalculateCardSize(availableSize);
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Measure(cardSize);
            }
            return new Size(cardSize.Width, cardSize.Height + ((InternalChildren.Count - 1) * spaceBetweenCardsTops));
        }

        /// <summary>
        /// Handles the arrangement of the cards
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size cardHeight = CalculateCardSize(finalSize);
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                double y = i * spaceBetweenCardsTops;
                InternalChildren[i].Arrange(new Rect(new Point(0, y), cardHeight));
            }
            return new Size(cardHeight.Width, cardHeight.Height + ((InternalChildren.Count - 1) * spaceBetweenCardsTops));
        }

        /// <summary>
        /// Calculates the size of cards such that they will all fit inside the given area.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private Size CalculateCardSize(Size givenArea)
        {
            double targetCardWidth = Math.Max(10d, System.Math.Min(300d, givenArea.Width));
            double goldenHeight = targetCardWidth * GoldenRatio;

            double targetCardHeight = double.IsInfinity(givenArea.Height) ? (300d * GoldenRatio) : (givenArea.Height - ((InternalChildren.Count - 1) * spaceBetweenCardsTops));
            if (targetCardHeight < 0)
            {
                // TODO: There is a better way to handle when the display area gets too small.
                targetCardHeight = goldenHeight;
            }
            else if (goldenHeight > targetCardHeight)
            {
                targetCardWidth = targetCardHeight / GoldenRatio;
            }
            else if (goldenHeight < targetCardHeight)
            {
                targetCardHeight = goldenHeight;
            }

            Size cardSize = new Size(targetCardWidth, targetCardHeight);
            return cardSize;
        }
    }
}