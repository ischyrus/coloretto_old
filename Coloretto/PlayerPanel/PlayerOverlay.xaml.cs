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
	/// Interaction logic for PlayerOverlay.xaml
	/// </summary>
	public partial class PlayerOverlay
	{
        /// <summary>
        /// Get or set the player's color
        /// </summary>
        public Brush PlayerColor
        {
            get { return (Brush)GetValue(PlayerColorProperty); }
            set { SetValue(PlayerColorProperty, value); }
        }

        public static readonly DependencyProperty PlayerColorProperty = DependencyProperty.Register("PlayerColor", typeof(Brush), typeof(PlayerOverlay), new UIPropertyMetadata(Brushes.Black));

        /// <summary>
        /// Get or set the player's name1
        /// </summary>
        public string PlayerName
        {
            get { return (string)GetValue(PlayerNameProperty); }
            set { SetValue(PlayerNameProperty, value); }
        }

        public static readonly DependencyProperty PlayerNameProperty = DependencyProperty.Register("PlayerName", typeof(string), typeof(PlayerOverlay), new UIPropertyMetadata("[Empty]"));

        /// <summary>
        /// Get or set the score
        /// </summary>
        public int Score
        {
            get { return (int)GetValue(ScoreProperty); }
            set { SetValue(ScoreProperty, value); }
        }

        public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(PlayerOverlay), new UIPropertyMetadata(0));

        /// <summary>
        /// Get or set the total score from previous rounds
        /// </summary>
        public int GameScore
        {
            get { return (int)GetValue(GameScoreProperty); }
            set { SetValue(GameScoreProperty, value); }
        }

        public static readonly DependencyProperty GameScoreProperty = DependencyProperty.Register("GameScore", typeof(int), typeof(PlayerOverlay), new UIPropertyMetadata(0));

        /// <summary>
        /// Default constructor
        /// </summary>
		public PlayerOverlay()
		{
			this.InitializeComponent();
		}
	}
}