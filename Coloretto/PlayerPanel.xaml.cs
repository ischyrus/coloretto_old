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
using Coloretto.Player;
using Coloretto.Game;
using Coloretto.Actions;

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

            panel.PlayerOverlay.PlayerName = profile.Username;
            panel.PlayerOverlay.PlayerColor = profile.Color;
        } 
        #endregion

        #region Properties
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
            InitializeComponent();
            this.Loaded += PlayerPanel_Loaded;
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
    }
}
