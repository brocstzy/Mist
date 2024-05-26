using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Mist.Helper
{
    public static class EventHandlers
    {
        public static readonly TextChangedEventHandler CommonTextChangedHandler = new TextChangedEventHandler(OnCommonTextChanged);
        public static void OnCommonTextChanged(object sender, TextChangedEventArgs e)
        {
            ((Control)sender).BorderThickness = new Thickness(0.5);
            ((Control)sender).BorderBrush = Brushes.Black;
        }
    }
}
