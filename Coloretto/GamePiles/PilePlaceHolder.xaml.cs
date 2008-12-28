using System.Windows;
using System.Windows.Controls;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for PilePlaceHolder.xaml
    /// </summary>
    public partial class PilePlaceHolder : UserControl
    {
        /// <summary>
        /// Get or set the pile number that this placeholder is for
        /// </summary>
        public int PileNumber
        {
            get { return (int)GetValue(PileNumberProperty); }
            set { SetValue(PileNumberProperty, value); }
        }

        public static readonly DependencyProperty PileNumberProperty =
            DependencyProperty.Register("PileNumber", typeof(int), typeof(PilePlaceHolder), new UIPropertyMetadata(-1));

        /// <summary>
        /// Default constructor
        /// </summary>
        public PilePlaceHolder()
        {
            InitializeComponent();
        }
    }
}
