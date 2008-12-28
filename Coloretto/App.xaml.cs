using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool loaded = false;
            if (e.Args != null)
                foreach (string arg in e.Args)
                {
                    if (arg.Equals("/PlayerWindow", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!loaded)
                        {
                            loaded = true;
                            this.StartupUri = new Uri("pack://application:,,,/TestWindows/Playerwindow.xaml");
                        }
                    }
                    else if (arg.Equals("/GameWindow", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!loaded)
                        {
                            loaded = true;
                            this.StartupUri = new Uri("pack://application:,,,/TestWindows/PilesWindow.xaml");
                        }
                    }
#if DEBUG
                    else if (arg.Equals("/cheat", StringComparison.CurrentCulture))
                    {
                        Coloretto.MainWindow._showCheatWindow = true;
                    }
#endif
                }
        }
    }
}
