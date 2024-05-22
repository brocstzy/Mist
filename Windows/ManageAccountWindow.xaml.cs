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
    }
}
