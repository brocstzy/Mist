using Mist.Model;
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
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public User User;
        public ProfilePage(User user)
        {
            InitializeComponent();
            User = user;
            RefreshProfileInfo();
        }

        private void RefreshProfileInfo()
        {

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 0, m, 50);
            main_Grid.Margin = margin;
        }

        private void addFriend_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeFriend_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void message_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editProfile_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
