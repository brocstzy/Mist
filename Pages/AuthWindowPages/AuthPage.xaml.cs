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
using Mist.Helper;

namespace Mist.Pages.AuthWindowPages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private AuthWindow _aw = (AuthWindow)WindowManager.GetWindow<AuthWindow>();
        public AuthPage()
        {
            InitializeComponent();
            SetBackground();
            SetButtonBackground();
        }

        public void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 1);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(7, 15, 43), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 0), 1.0));
            this.Background = bg;
        }
        public void SetButtonBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0.5);
            bg.EndPoint = new Point(1, 1.5);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(6, 143, 255), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(78, 79, 235), 1.0));
            logIn_Button.Background = bg;
        }

        private void titleBar_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
                _aw.DragMove();
        }



        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
