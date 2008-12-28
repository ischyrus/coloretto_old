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
using System.Windows.Shapes;
using Coloretto.Game;
using Coloretto.Player;
using Coloretto.Actions;
using CardManagement.Coloretto;

namespace Coloretto.TestWindows
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        public PlayerWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PlayerWindow_Loaded);
        }

        void PlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CardCollection cards = CardCollection.Empty;
            cards = cards.Add(new ColorettoCard(ColorettoCardTypes.Plus2));
            cards = cards.Add(new ColorettoCard(ColorettoCardTypes.Plus2));
            cards = cards.Add(new ColorettoCard(ColorettoCardTypes.Plus2));
            cards = cards.Add(new ColorettoCard(ColorettoCardTypes.Wild));
            cards = cards.Add(new ColorettoCard(ColorettoCardTypes.Wild));
            cards = cards.Add(new ColorettoCard(ColorettoCardColors.Green));
            cards = cards.Add(new ColorettoCard(ColorettoCardColors.Orange));
            PP.playerHand.Cards = cards;
        }
    }
}
