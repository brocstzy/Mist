using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages;
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

namespace Mist.UserControls
{
    /// <summary>
    /// Interaction logic for ProfileFriendUserControl.xaml
    /// </summary>
    public partial class ProfileFriendUserControl : UserControl
    {
        public User User;
        public ProfileFriendUserControl(User user)
        {
            InitializeComponent();
            this.User = user;
            RefreshThis();
        }

        private void RefreshThis()
        {
            pfp_Image.Source = ImageHelper.GetImage(User.Pfp);
            nickname_Label.Content = User.Nickname;
            mistLevel_Label.Content = User.GetGames().Count;
            if (User.Status)
            {
                status_Border.BorderBrush = Brushes.DeepSkyBlue;
                nickname_Label.Foreground = Brushes.DeepSkyBlue;
                status_Label.Content = "В сети";
                status_Label.Foreground = Brushes.DeepSkyBlue;
            }
            else
            {
                status_Border.BorderBrush = new SolidColorBrush(Color.FromRgb(106, 106, 106));
                nickname_Label.Foreground = new SolidColorBrush(Color.FromRgb(106, 106, 106));
                status_Label.Content = "Не в сети";
                status_Label.Foreground = new SolidColorBrush(Color.FromRgb(106, 106, 106));
            }
        }

        public void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ProfilePage(User));
        }

        private void UserControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (((ListBox)Parent).Name.Equals("friends_ListBox1"))
            {
                new MessengerWindow(User).Show();
            }
        }
    }
}
