using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mist.Helper
{
    public static class ButtonPainter
    {
        /// <summary>
        /// I hate Niggers.
        /// </summary>
        /// <param name="sender"></param>
        public static void SetButtonBackground(object sender)
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0.5);
            bg.EndPoint = new Point(1, 1.5);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(6, 143, 255), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(78, 79, 235), 1.0));
            ((Button)sender).Background = bg;
        }
        public static void SetButtonBackgroundHover(object sender)
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0.5);
            bg.EndPoint = new Point(1, 1.5);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(6, 143, 255), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(62, 166, 250), 0.3));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(78, 79, 235), 1.0));
            ((Button)sender).Background = bg;
        }
    }
}
