using Mist.Helper;
using Mist.Windows;
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

namespace Mist.Pages.ManageAccountWindowPages
{
    /// <summary>
    /// Interaction logic for SuccessfulRegistrationPage.xaml
    /// </summary>
    public partial class SuccessfulRegistrationPage : Page
    {
        public SuccessfulRegistrationPage()
        {
            InitializeComponent();
            SetBackground();
            ContinueReg();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private async void ContinueReg()
        {
            await Task.Delay(1000);
            timer_Label.Content = "Автоматическое перенаправление через 3...";
            await Task.Delay(1000);
            timer_Label.Content = "Автоматическое перенаправление через 2...";
            await Task.Delay(1000);
            timer_Label.Content = "Автоматическое перенаправление через 1...";
            await Task.Delay(1000);
            WindowManager.Close<ManageAccountWindow>();
        }
        public void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 1);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(12, 11, 61), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 0), 1.0));
            this.Background = bg;
        }
    }
}
