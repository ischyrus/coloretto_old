using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Coloretto.Actions;
using Coloretto.Game;
using Coloretto.Player;
using Coloretto.State;
using System.Diagnostics;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for PlayerPanel.xaml
    /// </summary>
    public partial class PlayerPanel : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty PlayerProfileProperty =
           DependencyProperty.Register("PlayerProfile", typeof(Profile), typeof(PlayerPanel), new UIPropertyMetadata(null, PlayerProfilePropertyChanged));

        private static void PlayerProfilePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            PlayerPanel panel = (PlayerPanel)sender;
            Profile profile = (Profile)args.NewValue;

            BrushConverter converter = new BrushConverter();
            panel.PlayerOverlay.PlayerName = profile.Username;
            panel.PlayerOverlay.PlayerColor = (Brush)converter.ConvertFromString(profile.Color);
        }

        public bool IsPlayersTurn
        {
            get { return (bool)GetValue(IsPlayersTurnProperty); }
            set { SetValue(IsPlayersTurnProperty, value); }
        }

        public static readonly DependencyProperty IsPlayersTurnProperty =
            DependencyProperty.RegisterAttached("IsPlayersTurn", typeof(bool), typeof(PlayerPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region Properties
        public int PlayerIndex { get; set; }

        /// <summary>
        /// Get or set the player profile
        /// </summary>
        public Profile PlayerProfile
        {
            get { return (Profile)GetValue(PlayerProfileProperty); }
            set { SetValue(PlayerProfileProperty, value); }
        } 
        #endregion

        #region Constructor
        public PlayerPanel()
        {
            PlayerIndex = -1;
            InitializeComponent();
            //this.Loaded += PlayerPanel_Loaded;
        }

        void PlayerPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Profile p1 = new Profile() { Username = "Player 1", Color = "Red" };
            Profile p2 = new Profile() { Username = "Player 2", Color = "Green" };
            Profile p3 = new Profile() { Username = "Player 3", Color = "Blue" };
            ColorettoGame game = new ColorettoGame(p1, p2, p3);

            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;

            game = (game + PickupPileAction.Action(0)).Game;
            game = (game + PickupPileAction.Action(1)).Game;
            game = (game + PickupPileAction.Action(2)).Game;

            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;

            game = (game + PickupPileAction.Action(0)).Game;
            game = (game + PickupPileAction.Action(1)).Game;
            game = (game + PickupPileAction.Action(2)).Game;

            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;

            game = (game + PickupPileAction.Action(0)).Game;
            game = (game + PickupPileAction.Action(1)).Game;
            game = (game + PickupPileAction.Action(2)).Game;

            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;

            game = (game + PickupPileAction.Action(0)).Game;
            game = (game + PickupPileAction.Action(1)).Game;
            game = (game + PickupPileAction.Action(2)).Game;

            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(0)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(1)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;
            game = (game + PlaceCardAction.Action(2)).Game;

            game = (game + PickupPileAction.Action(0)).Game;
            game = (game + PickupPileAction.Action(1)).Game;
            game = (game + PickupPileAction.Action(2)).Game;

            playerHand.Cards = game.Hands[0];
        } 
        #endregion

        /// <summary>
        /// Method is wired up to the GameUpdated even on the game state controller.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Controller_GameUpdated(object sender, GameUpdatedEventArgs args)
        {
            Debug.Assert(PlayerIndex != -1, "The PlayerIndex was not initailzed on PlayerPanel.");

            IsPlayersTurn = args.NewGame.CurrentPlayerIndex == PlayerIndex;
            PlayerOverlay.GameScore = args.NewGame.GameScores[PlayerIndex].Sum(roundHand => roundHand.Score);
            PlayerOverlay.Score = args.NewGame.Hands[PlayerIndex].Score;
            playerHand.Cards = args.NewGame.Hands[PlayerIndex];
        }
    }
}
