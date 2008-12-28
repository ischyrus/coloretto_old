using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CardManagement.Coloretto;

namespace Coloretto.State
{
    public class PlayerStateController : DependencyObject
    {
        /// <summary>
        /// Get or set the current score of the player. This refers to the player's current hand.
        /// </summary>
        public int Score
        {
            get { return (int)GetValue(ScoreProperty); }
            set { SetValue(ScoreProperty, value); }
        }

        public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(PlayerStateController), new UIPropertyMetadata(0));

        /// <summary>
        /// 
        /// </summary>
        public int GameScore
        {
            get { return (int)GetValue(GameScoreProperty); }
            set { SetValue(GameScoreProperty, value); }
        }

        public static readonly DependencyProperty GameScoreProperty = DependencyProperty.Register("GameScore", typeof(int), typeof(PlayerStateController), new UIPropertyMetadata(0));

        /// <summary>
        /// Get or set the player's hand
        /// </summary>
        public CardCollection Hand
        {
            get { return (CardCollection)GetValue(HandProperty); }
            set { SetValue(HandProperty, value); }
        }

        public static readonly DependencyProperty HandProperty = DependencyProperty.Register("Hand", typeof(CardCollection), typeof(PlayerStateController), new UIPropertyMetadata(CardCollection.Empty));


    }
}
