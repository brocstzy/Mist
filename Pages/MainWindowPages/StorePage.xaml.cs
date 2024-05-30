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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {
        public StorePage()
        {
            InitializeComponent();
        }

        public void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 0);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(28, 44, 64), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(47, 78, 117), 0.50));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(28, 44, 64), 1.00));
            this.Background = bg;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetBackground();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }
    }
}
