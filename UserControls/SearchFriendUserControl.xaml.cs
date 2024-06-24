using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages;
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
    /// Interaction logic for SearchFriendUserControl.xaml
    /// </summary>
    public partial class SearchFriendUserControl : UserControl
    {
        public User User;
        public SearchFriendUserControl(User user)
        {
            InitializeComponent();
            User = user;
            RefreshThis();
        }

        private void RefreshThis()
        {
            pfp_Image.Source = ImageHelper.GetImage(User.Pfp);
            nickname_Label.Content = User.Nickname;
            var friendsCount = App.Context.Users.Where(u => u.Id == User.Id).First().GetFriends().Count;
            friendsCount_Label.Content = friendsCount;
            switch (friendsCount)
            {
                case 0:
                    friendsCount_Label.Content += " друзей";
                    break;
                case 1:
                    friendsCount_Label.Content += " друг";
                    break;
                case 2:
                    friendsCount_Label.Content += " друга";
                    break;
                case 3:
                    friendsCount_Label.Content += " друга";
                    break;
                case 4:
                    friendsCount_Label.Content += " друга";
                    break;
                default:
                    friendsCount_Label.Content += " друзей";
                    break;

            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background.Opacity = 0.4;
            this.Cursor = Cursors.Hand;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background.Opacity = 0.3;
            this.Cursor = null;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ProfilePage(User));
        }
    }
}
