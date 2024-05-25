using Mist.Helper;
using Mist.Pages.ManageAccountWindowPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mist.Windows
{
    /// <summary>
    /// Interaction logic for ManageAccountWindow.xaml
    /// </summary>
    public partial class ManageAccountWindow : Window
    {
        public int Page;
        public ManageAccountWindow(int page)
        {
            InitializeComponent();
            Page = page;
            SetBackground();
        }

        private void titleBar_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame = MainFrame;
            if (Page == 1)
                MainFrame.Navigate(new RestoreAccountPage());
            else if (Page == 2)
                MainFrame.Navigate(new CreateAccountPage());
            
        }

        public void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 1);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(12, 11, 61), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 0), 1.0));
            content_Grid.Background = bg;
        }
    }
}
