﻿using Mist.Model;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MistContext Context = new MistContext();
        public static Cursor Cursor { get; set; }
        private void TitleBarButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var window = Window.GetWindow(btn);
            if (btn.Name.Equals("closeWindow_Button"))
            {
                window.Close();
            }
        }

        private void CommonTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            sender.BorderThickness = new Thickness(0.5);
            sender.BorderBrush = Brushes.Black;
        }
    }

}
