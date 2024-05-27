using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mist.Helper
{
    public static class ParentNameHelper
    {
        public static readonly DependencyProperty ParentNameProperty =
            DependencyProperty.RegisterAttached(
                "ParentName",
                typeof(string),
                typeof(ParentNameHelper),
                new PropertyMetadata(null));

        public static string GetParentName(DependencyObject obj)
        {
            return (string)obj.GetValue(ParentNameProperty);
        }

        public static void SetParentName(DependencyObject obj, string value)
        {
            obj.SetValue(ParentNameProperty, value);
        }
    }
}
