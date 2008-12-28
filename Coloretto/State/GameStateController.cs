using System.Collections.ObjectModel;
using System.Windows;
using Coloretto.Game;
using System;
using System.ComponentModel;

namespace Coloretto.State
{
    public class GameUpdatedEventArgs : EventArgs
    {
        public ColorettoGame OldGame { get; private set; }
        public ColorettoGame NewGame { get; private set; }

        public GameUpdatedEventArgs(ColorettoGame old, ColorettoGame newGame)
        {
            OldGame = old;
            NewGame = newGame;
        }
    }

    public class GameStateController : DependencyObject
    {
        #region Depdenency Properties
        /// <summary>
        /// Identifier for the CurrentGame dependency property
        /// </summary>
        public static readonly DependencyProperty CurrentGameProperty =
            DependencyProperty.Register("CurrentGame", typeof(ColorettoGame), typeof(GameStateController), new UIPropertyMetadata(ColorettoGame.Empty, OnCurrentGameChanged));

        /// <summary>
        /// Indentifier for the PlayerStates dependency property
        /// </summary>
        public static readonly DependencyProperty PlayerStatesProperty =
            DependencyProperty.Register("PlayerStates", typeof(ObservableCollection<PlayerStateController>), typeof(GameStateController), new UIPropertyMetadata(null));
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the current game
        /// </summary>
        public ColorettoGame CurrentGame
        {
            get { return (ColorettoGame)GetValue(CurrentGameProperty); }
            set { SetValue(CurrentGameProperty, value); }
        }

        public Guid Id { get; private set; }

        /// <summary>
        /// Get or set the list of players
        /// </summary>
        public ObservableCollection<PlayerStateController> PlayerStates
        {
            get { return (ObservableCollection<PlayerStateController>)GetValue(PlayerStatesProperty); }
            set { SetValue(PlayerStatesProperty, value); }
        }
        #endregion

        public event EventHandler<GameUpdatedEventArgs> GameUpdated;

        private MainWindow _window;

        private GameStateController() { }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameStateController(ColorettoGame game, MainWindow window)
        {
            Id = Guid.NewGuid();
            _window = window;
            PlayerStates = new ObservableCollection<PlayerStateController>();
            CurrentGame = game;
        }

        private static void OnCurrentGameChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            GameStateController controller = (GameStateController)sender;
            ColorettoGame game = args.NewValue as ColorettoGame;

            if (controller.GameUpdated != null)
            {
                controller.GameUpdated(controller, new GameUpdatedEventArgs((ColorettoGame)args.OldValue, game));
            }
        }
    }
}
