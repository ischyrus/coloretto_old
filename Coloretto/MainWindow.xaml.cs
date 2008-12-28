using System.Linq;
using System.Windows;
using CardManagement;
using CardManagement.Coloretto;
using Coloretto.Player;
using System.Collections.Generic;
using System.Windows.Controls;
using Coloretto.Game;
using Coloretto.State;
using Coloretto.Actions;
using System.Diagnostics;
using Coloretto.TestWindows;
using System.Windows.Input;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static internal bool _showCheatWindow = false;
        GameStartDialog _startDialog;
        ContentControl[] _playerSlots;

        /// <summary>
        /// Get or set the contoller for this game.
        /// </summary>
        public GameStateController Controller
        {
            get { return (GameStateController)GetValue(ControllerProperty); }
            set { SetValue(ControllerProperty, value); }
        }

        public static readonly DependencyProperty ControllerProperty =
            DependencyProperty.Register("Controller", typeof(GameStateController), typeof(MainWindow), new UIPropertyMetadata(null));

        public Dictionary<Profile, PlayerPanel> PlayerPanels { get; private set; }

        public MainWindow()
        {
            _startDialog = new GameStartDialog();
            bool? result = _startDialog.ShowDialog();
            if (result == null || !result.Value)
                Application.Current.Shutdown();

            this.Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D:
                    e.Handled = true;
                    Commands.DrawCardCommand.Execute(this, this);
                    break;
                case Key.D1:
                case Key.NumPad1:
                    e.Handled = true;
                    Commands.SelectPile1Command.Execute(this, this);
                    break;
                case Key.D2:
                case Key.NumPad2:
                    e.Handled = true;
                    Commands.SelectPile2Command.Execute(this, this);
                    break;
                case Key.D3:
                case Key.NumPad3:
                    e.Handled = true;
                    Commands.SelectPile3Command.Execute(this, this);
                    break;
                case Key.D4:
                case Key.NumPad4:
                    e.Handled = true;
                    Commands.SelectPile4Command.Execute(this, this);
                    break;
                case Key.D5:
                case Key.NumPad5:
                    e.Handled = true;
                    Commands.SelectPile5Command.Execute(this, this);
                    break;
                default:
                    break;
            }

            base.OnPreviewKeyUp(e);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();

            CommandBinding drawCardBinding = new CommandBinding(Commands.DrawCardCommand, DrawCard_Execute, DrawCard_CanExecute);
            CommandBindings.Add(drawCardBinding);

            CommandBinding selectPile1 = new CommandBinding(Commands.SelectPile1Command, SelectPile1_Execute, SelectPile1_CanExecute);
            CommandBindings.Add(selectPile1);

            CommandBinding selectPile2 = new CommandBinding(Commands.SelectPile2Command, SelectPile2_Execute, SelectPile2_CanExecute);
            CommandBindings.Add(selectPile2);

            CommandBinding selectPile3 = new CommandBinding(Commands.SelectPile3Command, SelectPile3_Execute, SelectPile3_CanExecute);
            CommandBindings.Add(selectPile3);

            CommandBinding selectPile4 = new CommandBinding(Commands.SelectPile4Command, SelectPile4_Execute, SelectPile4_CanExecute);
            CommandBindings.Add(selectPile4);

            CommandBinding selectPile5 = new CommandBinding(Commands.SelectPile5Command, SelectPile5_Execute, SelectPile5_CanExecute);
            CommandBindings.Add(selectPile5);

            CommandBinding quitBinding = new CommandBinding(ApplicationCommands.Close, Close_Execute);
            CommandBindings.Add(quitBinding);
        }

        private void Close_Execute(object sender, ExecutedRoutedEventArgs args)
        {
            args.Handled = true;
            Application.Current.Shutdown();
        }

        private void DrawCard_Execute(object sender, ExecutedRoutedEventArgs args)
        {
            ColorettoGame originalGame = Controller.CurrentGame;
            ActionResult result = originalGame + DrawCardAction.DefaultAction;
            Controller.CurrentGame = result.Game;
        }

        private void DrawCard_CanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.Handled = true;
            args.CanExecute = Controller.CurrentGame.AvailableActions == GameActions.Draw || Controller.CurrentGame.AvailableActions == GameActions.DrawOrPickupPile;
        }

        private void SelectPile1_Execute(object sender, ExecutedRoutedEventArgs args) { SelectPile(args, 0); }
        private void SelectPile1_CanExecute(object sender, CanExecuteRoutedEventArgs args) { CanSelectPile(args, 0); }
        private void SelectPile2_Execute(object sender, ExecutedRoutedEventArgs args) { SelectPile(args, 1); }
        private void SelectPile2_CanExecute(object sender, CanExecuteRoutedEventArgs args) { CanSelectPile(args, 1); }
        private void SelectPile3_Execute(object sender, ExecutedRoutedEventArgs args) { SelectPile(args, 2); }
        private void SelectPile3_CanExecute(object sender, CanExecuteRoutedEventArgs args) { CanSelectPile(args, 2); }
        private void SelectPile4_Execute(object sender, ExecutedRoutedEventArgs args) { SelectPile(args, 3); }
        private void SelectPile4_CanExecute(object sender, CanExecuteRoutedEventArgs args) { CanSelectPile(args, 3); }
        private void SelectPile5_Execute(object sender, ExecutedRoutedEventArgs args) { SelectPile(args, 4); }
        private void SelectPile5_CanExecute(object sender, CanExecuteRoutedEventArgs args) { CanSelectPile(args, 4); }

        private void CanSelectPile(CanExecuteRoutedEventArgs args, int pileNumber)
        {
            ColorettoGame originalGame = Controller.CurrentGame;
            if ((originalGame.AvailableActions & GameActions.PickupPile) == GameActions.PickupPile)
            {
                args.Handled = true;
                bool canExecute = pileNumber < originalGame.Piles.Count && originalGame.Piles[pileNumber] != null && originalGame.Piles[pileNumber].Count > 0;
                args.CanExecute = canExecute;
                return;
            }
            else if (originalGame.AvailableActions == GameActions.PlaceCard)
            {
                args.Handled = true;
                // TODO: 3 should not be hard coded.
                bool canExecute = pileNumber < originalGame.Piles.Count && originalGame.Piles[pileNumber] != null && originalGame.Piles[pileNumber].Count < 3;
                args.CanExecute = canExecute;
                return;
            }

            args.CanExecute = false;
        }

        private void SelectPile(ExecutedRoutedEventArgs args, int pileNumber)
        {
            ColorettoGame originalGame = Controller.CurrentGame;
            if ((originalGame.AvailableActions & GameActions.PickupPile) == GameActions.PickupPile)
            {
                args.Handled = true;
                ActionResult result = originalGame + PickupPileAction.Action(pileNumber);
                Controller.CurrentGame = result.Game;
            }
            else if (originalGame.AvailableActions == GameActions.PlaceCard)
            {
                args.Handled = true;
                ActionResult result = originalGame + PlaceCardAction.Action(pileNumber);
                Controller.CurrentGame = result.Game;
            }
        }

        private void Init()
        {
            InitializePlayers();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Stacks.NumberOfStacks = _startDialog.ConfiguredPlayers.Count();
            ColorettoGame game = new ColorettoGame(PlayerPanels.Keys.Select(key => key).ToArray());
            Controller = new GameStateController(game, this);
            Controller.GameUpdated += new System.EventHandler<GameUpdatedEventArgs>(Controller_GameUpdated);

            foreach (var playerPanels in PlayerPanels)
                Controller.GameUpdated += playerPanels.Value.Controller_GameUpdated;

#if DEBUG
            if (_showCheatWindow)
            {
                CheatWindow cheat = new CheatWindow(this, Controller);
            }
#else
            if(_showCheatWindow)
            {
                MessageBox.Show("You are trying to cheat. You have been reported. Cheater.");
            }
#endif
        }

        private void Controller_GameUpdated(object sender, GameUpdatedEventArgs e)
        {
            CurrentPlayer.Text = "Current Player: " + e.NewGame.CurrentPlayer.Username;
            Round.Text = "Round: " + e.NewGame.Round;
            Turn.Text = "Turn: " + e.NewGame.Turn;
            Cycle.Text = "Cycle: " + e.NewGame.Cycle;

            if (e.NewGame.State == GameStates.Finished)
            {
                GameOver gameOverDisplay = new GameOver(e.NewGame)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumnSpan(gameOverDisplay, 3);
                Grid.SetRowSpan(gameOverDisplay, 3);
                MainGrid.Children.Add(gameOverDisplay);
            }

            if (e.NewGame.VisibleCard == null)
            {
                VisibleCard.Content = null;
            }
            else if (e.NewGame.VisibleCard.CardType == ColorettoCardTypes.Color)
            {
                PlayerHandCard card = new PlayerHandCard { CardColor = e.NewGame.VisibleCard.Color };
                VisibleCard.Content = card;
            }
            else if (e.NewGame.VisibleCard.CardType == ColorettoCardTypes.Wild)
            {
                VisibleCard.Content = new PlayerHandWildCard();
            }
            else if (e.NewGame.VisibleCard.CardType == ColorettoCardTypes.Plus2)
            {
                VisibleCard.Content = new PlayerHandPlusTwoCard();
            }

            // The stacks should be listening themselves.
            Stacks.UpdatePiles(e.NewGame.Piles);
        }

        private void InitializePlayers()
        {
            _playerSlots = new ContentControl[] { ThisPlayer, PlayerSlot1, PlayerSlot2, PlayerSlot3, PlayerSlot4 };
            PlayerPanels = new Dictionary<Profile, PlayerPanel>(5);

            List<Profile> profiles = new List<Profile>(5);
            int i = 0;
            foreach (string player in _startDialog.ConfiguredPlayers)
            {
                Profile profile = new Profile { Username = player, Color = ((ColorettoCardColors)(PlayerPanels.Count + 1)).ToString() };
                PlayerPanel panel = new PlayerPanel { PlayerProfile = profile, PlayerIndex = i++ };
                PlayerPanels.Add(profile, panel);

                _playerSlots[profiles.Count].Content = panel;
                profiles.Add(profile);
            }

            PlayerPanels[profiles[0]].IsPlayersTurn = true;
        }

        //private void DrawAction_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorettoGame originalGame = Controller.CurrentGame;
        //    ActionResult result = originalGame + DrawCardAction.DefaultAction;
        //    Debug.WriteLine(result.Data.ToString());
        //    Controller.CurrentGame = result.Game;
        //}

        //private void PlaceAction_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorettoGame originalGame = Controller.CurrentGame;
        //    int i = 0;
        //    for (; i < originalGame.Piles.Count && originalGame.Piles[i].Count == 3; i++) { }
        //    ActionResult result = originalGame + PlaceCardAction.Action(i);
        //    Debug.WriteLine(result.Data.ToString());
        //    Controller.CurrentGame = result.Game;
        //}

        //private void PickUpAction_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorettoGame originalGame = Controller.CurrentGame;
        //    ActionResult result = originalGame + PickupPileAction.Action(originalGame.Piles.IndexOf(originalGame.Piles.Where(pile => pile != null).First()));
        //    Debug.WriteLine(result.Data.ToString());
        //    Controller.CurrentGame = result.Game;
        //}
    }
}
