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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mist.Windows
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        public bool Confirmed = false;
        public ConfirmationWindow()
        {
            InitializeComponent();
            Owner = WindowManager.GetWindow<MainWindow>();
            ShowInTaskbar = false;
            BlurMainWindow();
        }

        private void BlurMainWindow()
        {
            ((MainWindow)WindowManager.GetWindow<MainWindow>()).blackTint.Visibility = Visibility.Visible;
            WindowManager.GetWindow<MainWindow>().Effect = new BlurEffect();
        }

        private void UnBlurMainWindow()
        {
            WindowManager.GetWindow<MainWindow>().Effect = null;
            ((MainWindow)WindowManager.GetWindow<MainWindow>()).blackTint.Visibility = Visibility.Collapsed;
        }

        private void confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = true;
            UnBlurMainWindow();
            this.Close();
        }

        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = false;
            UnBlurMainWindow();
            this.Close();
        }
    }
}
