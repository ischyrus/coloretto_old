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

namespace Coloretto.TestWindows
{
    /// <summary>
    /// Interaction logic for CheatWindow.xaml
    /// </summary>
    public partial class CheatWindow : Window
    {
        public State.GameStateController Controller { get; set; }
        public MainWindow Main { get; set; }

        public CheatWindow()
        {
            InitializeComponent();
        }

        public CheatWindow(MainWindow main, State.GameStateController controller)
        {
            Main = main;
            Controller = controller ;
        }
    }
}
