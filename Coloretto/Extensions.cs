using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Coloretto
{
    public static class Extensions
    {
        private const double GoldenRatio = 1.6180339887;

        public static Size GoldenSize(this Size originalSize, int numberToContain, Orientation cardFlow)
        {
            double width, height;
            if (cardFlow == Orientation.Horizontal)
            {
                width = originalSize.Width / numberToContain;
                height = originalSize.Height;
            }
            else
            {
                width = originalSize.Width;
                height = originalSize.Height / numberToContain;
            }

            double goldenHeight = width * GoldenRatio;
            if (goldenHeight > height)
            {
                goldenHeight = height;
                width = goldenHeight / GoldenRatio;
            }

            return new Size(width, goldenHeight);
        }
    }
}
