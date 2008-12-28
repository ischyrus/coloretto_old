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
using Coloretto.Game;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : UserControl
    {
        protected class GameOverItem
        {
            public string PlayerName { get; set; }
            public int Score { get; set; }
        }

        /// <summary>
        /// Get or set the game we are reporting on.
        /// </summary>
        public ColorettoGame Game
        {
            get { return (ColorettoGame)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public static readonly DependencyProperty GameProperty =
            DependencyProperty.Register("Game", typeof(ColorettoGame), typeof(GameOver), new UIPropertyMetadata(null, GamePropertyChanged));

        /// <summary>
        /// Create an empty game over report card
        /// </summary>
        public GameOver()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Report the game over status for game
        /// </summary>
        /// <param name="game"></param>
        public GameOver(ColorettoGame game)
            : this()
        {
            this.Game = game;
        }

        private static void GamePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            GameOver gameOver = (GameOver)sender;
            ColorettoGame game = args.NewValue as ColorettoGame;
            if (game != null)
            {
                List<GameOverItem> scoreboard = new List<GameOverItem>();
                int i = 0;
                foreach (var item in game.Players)
                {
                    scoreboard.Add(new GameOverItem { PlayerName = item.Username, Score = game.GameScores[i++].Sum(cc => cc.Score) });
                }
                gameOver.ScoresListView.ItemsSource = scoreboard;
            }
            else
            {
                gameOver.ScoresListView.ItemsSource = null;
            }
        }
    }
}
