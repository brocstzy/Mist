using Mist.Helper;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mist.Pages.ManageAccountWindowPages
{
    /// <summary>
    /// Interaction logic for RAP_EmailPage.xaml
    /// </summary>
    public partial class RAP_EmailPage : Page
    {
        public RAP_EmailPage()
        {
            InitializeComponent();
            SetBackground();
        }

        private void forgotEmail_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new RAP_PhonePage());
        }
        private void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0.5);
            bg.EndPoint = new Point(1, 1.5);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(6, 143, 255), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(78, 79, 235), 1.0));
            
        }
    }
}
