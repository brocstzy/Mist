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

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for CommunityPage.xaml
    /// </summary>
    public partial class CommunityPage : Page
    {
        public CommunityPage()
        {
            InitializeComponent();
        }

        private void createGroup_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createDevGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new CreateDevGroupPage());
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }
    }
}
