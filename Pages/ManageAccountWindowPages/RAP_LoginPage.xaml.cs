using Mist.Helper;
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
    /// Interaction logic for RAP_LoginPage.xaml
    /// </summary>
    public partial class RAP_LoginPage : Page
    {
        public RAP_LoginPage()
        {
            InitializeComponent();
            var captcha = Captcha.Create();
            captcha_Image.Source = Captcha.ImageSourceFromBitmap(captcha.Image);
            SetBackground();
        }

        private void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 1);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(36, 53, 74), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(11, 25, 41), 1.0));
            search_StackPanel.Background = bg;
        }


        private void forgotUsername_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new RAP_EmailPage());
        }
    }
}
