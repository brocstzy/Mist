using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mist.Helper
{
    public static class ButtonHelper
    {
        public static readonly DependencyProperty ButtonNameProperty =
    DependencyProperty.RegisterAttached("ButtonName", typeof(string), typeof(ButtonHelper), new PropertyMetadata(string.Empty));

        public static string GetButtonName(DependencyObject obj)
        {
            return (string)obj.GetValue(ButtonNameProperty);
        }

        public static void SetButtonName(DependencyObject obj, string value)
        {
            obj.SetValue(ButtonNameProperty, value);
        }
    }
}
