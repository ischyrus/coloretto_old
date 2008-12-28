using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CardManagement.Coloretto;

namespace Coloretto
{
    /// <summary>
    /// Interaction logic for SidewaysStacks.xaml
    /// </summary>
    public partial class SidewaysStacks : Panel
    {
        List<SidewaysStack> _stacks;

        #region Dependency Properties
        public static readonly DependencyProperty NumberOfStacksProperty =
            DependencyProperty.Register("NumberOfStacks", typeof(int), typeof(SidewaysStacks), new UIPropertyMetadata(-1, NumberOfStacksPropertyChanged));

        private static void NumberOfStacksPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            SidewaysStacks stacks = (SidewaysStacks)sender;
            stacks._stacks = new List<SidewaysStack>(value);
            for (int i = 0; i < value; i++)
            {
                SidewaysStack stack = new SidewaysStack { PileNumber = i + 1 };
                stacks._stacks.Add(stack);
                stacks.Children.Add(stack);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the number of stacks
        /// </summary>
        public int NumberOfStacks
        {
            get { return (int)GetValue(NumberOfStacksProperty); }
            set { SetValue(NumberOfStacksProperty, value); }
        }
        #endregion

        public SidewaysStack this[int i]
        {
            get { return _stacks[i]; }
        }

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SidewaysStacks()
        {
            InitializeComponent();
        }
        #endregion

        #region Measure and Override
        protected override Size MeasureOverride(Size availableSize)
        {
            if (NumberOfStacks < 1)
            {
                return availableSize;
            }

            Size stackSize = new Size(availableSize.Width, availableSize.Height / NumberOfStacks);

            foreach (var stack in _stacks)
            {
                stack.Measure(stackSize);
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (NumberOfStacks < 1)
            {
                return finalSize;
            }

            Size stackSize = new Size(finalSize.Width, finalSize.Height / NumberOfStacks);

            for (int i = 0; i < _stacks.Count; i++)
            {
                _stacks[i].Arrange(new Rect(new Point(0, i * stackSize.Height), stackSize));
            }

            return base.ArrangeOverride(finalSize);
        } 
        #endregion

        /// <summary>
        /// Update the cards that are in the stack
        /// </summary>
        /// <param name="readOnlyCollection"></param>
        internal void UpdatePiles(ReadOnlyCollection<CardCollection> readOnlyCollection)
        {
            for (int i = 0; i < readOnlyCollection.Count; i++)
            {
                _stacks[i].ClearNewStatusOnCards();
                _stacks[i].Cards = readOnlyCollection[i] ?? CardCollection.Empty;
            }
        }
    }
}
