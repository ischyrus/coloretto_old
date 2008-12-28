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
using Coloretto.Actions;
using Coloretto.Player;
using Coloretto.Game;
using CardManagement.Coloretto;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for GamePiles.xaml
    /// </summary>
    public partial class GamePiles : UserControl
    {
        public GamePiles()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(GamePiles_Loaded);
        }

        private void GamePiles_Loaded(object sender, RoutedEventArgs e)
        {
            //Random random = new Random();
            //BrushConverter bc = new BrushConverter();
            //ColorettoCardColors colors = (ColorettoCardColors)random.Next(1, 8);
            //for (int i = 0; i < 3; i++)
            //{
            //    Brush brush = (Brush)bc.ConvertFromString(colors.ToString());
            //    ss.Children.Add(new PlayerHandCard { CardColor = brush });
            //}
        }
    }
}
