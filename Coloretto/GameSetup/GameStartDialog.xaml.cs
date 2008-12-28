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

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for GameStartDialog.xaml
    /// </summary>
    public partial class GameStartDialog : Window
    {
        public static RoutedCommand StartGameCommand = new RoutedCommand("StartGame", typeof(GameStartDialog));
        private static readonly DependencyPropertyKey GameReadyPropertyKey = DependencyProperty.RegisterReadOnly("GameReady", typeof(bool), typeof(GameStartDialog), new UIPropertyMetadata(false));
        public static readonly DependencyProperty GameReadyProperty = GameReadyPropertyKey.DependencyProperty;

        /// <summary>
        /// Get the configured players
        /// </summary>
        public IEnumerable<string> ConfiguredPlayers
        {
            get
            {
                return StackPanel_Players.Children.OfType<PlayerSetup>()
                                                  .Where(player => player.IsRequired || (!player.IsEmpty && player.PlayerName.Length > 0))
                                                  .Select(player => player.PlayerName);
            }
        }

        /// <summary>
        /// Get if the game config is ready and valid
        /// </summary>
        public bool GameReady
        {
            get { return (bool)GetValue(GameReadyProperty); }
            private set { SetValue(GameReadyPropertyKey, value); }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameStartDialog()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(StartGameCommand, StartGame_Executed, StartGame_CanExecute));
            Loaded += new RoutedEventHandler(GameStartDialog_Loaded);
        }

        /// <summary>
        /// Determine if the start button should be enabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var playerSetupControls = StackPanel_Players.Children.OfType<PlayerSetup>();
            List<string> usedNames = new List<string>(5);
            foreach (var playerSetupControl in playerSetupControls)
            {
                if (playerSetupControl.IsRequired && playerSetupControl.IsEmpty)
                {
                    e.CanExecute = false;
                    e.Handled = true;
                    return;
                }
                else if (playerSetupControl.IsEmpty == false)
                {
                    if (usedNames.Contains(playerSetupControl.PlayerName.ToUpper()))
                    {
                        e.CanExecute = false;
                        e.Handled = true;
                        return;
                    }
                    usedNames.Add(playerSetupControl.PlayerName.ToUpper());
                }
            }

            e.CanExecute = true;
            e.Handled = true;
        }

        private void StartGame_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void GameStartDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Player1.GetFocus();
        }
    }
}
