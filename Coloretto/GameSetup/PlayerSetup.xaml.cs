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
    /// Interaction logic for PlayerSetup.xaml
    /// </summary>
    public partial class PlayerSetup
    {

        private static readonly DependencyPropertyKey IsEmptyPropertyKey = DependencyProperty.RegisterReadOnly("IsEmpty", typeof(bool), typeof(PlayerSetup), new UIPropertyMetadata(true));

        public static readonly DependencyProperty IsEmptyProperty = IsEmptyPropertyKey.DependencyProperty;
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register("IsRequired", typeof(bool), typeof(PlayerSetup), new UIPropertyMetadata(false));
        public static readonly DependencyProperty PlayerIndexProperty = DependencyProperty.Register("PlayerIndex", typeof(int), typeof(PlayerSetup), new UIPropertyMetadata(1));
        public static readonly DependencyProperty PlayerNameProperty = DependencyProperty.Register("PlayerName", typeof(string), typeof(PlayerSetup));

        public static readonly RoutedEvent PlayerModifiedEvent = EventManager.RegisterRoutedEvent("PlayerModified", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PlayerSetup));

        /// <summary>
        /// Get if the player's info is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(IsEmptyPropertyKey, value); }
        }

        /// <summary>
        /// Get or set if this player is required
        /// </summary>
        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        }

        /// <summary>
        /// Get or set the index that this player is being setup for
        /// </summary>
        public int PlayerIndex
        {
            get { return (int)GetValue(PlayerIndexProperty); }
            set { SetValue(PlayerIndexProperty, value); }
        }

        /// <summary>
        /// Get or set the player's name 
        /// </summary>
        public string PlayerName
        {
            get { return (string)GetValue(PlayerNameProperty); }
            set { SetValue(PlayerNameProperty, value); }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerSetup()
        {
            this.InitializeComponent();
            Loaded += new RoutedEventHandler(PlayerSetup_Loaded);
        }

        public void GetFocus()
        {
            TextBox_Username.Focus();
        }

        private void PlayerSetup_Loaded(object sender, RoutedEventArgs e)
        {
            if (!TextBox_Username.IsFocused)
                PlayerName = "Player Name";
        }

        private void TextBox_Username_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsEmpty)
            {
                TextBox_Username.Style = (Style)Resources["NonEmptyTextbox"];
                PlayerName = string.Empty;
            }
        }

        private void TextBox_Username_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsEmpty)
            {
                TextBox_Username.Style = (Style)Resources["EmptyTextbox"];
                TextBox_Username.Text = "Player Name";
            }

            OnPlayerModified();
        }

        private void OnPlayerModified()
        {
            RoutedEventArgs args = new RoutedEventArgs(PlayerModifiedEvent, this);
            RaiseEvent(args);
        }

        private void TextBox_Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateIsEmpty();
        }

        private void UpdateIsEmpty()
        {
            IsEmpty = TextBox_Username.Text.Trim().Length == 0 || TextBox_Username.Text.Trim().Equals("Player Name");
        }
    }
}