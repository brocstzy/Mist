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
        }


        private void forgotUsername_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new RAP_EmailPage());
        }
    }
}
