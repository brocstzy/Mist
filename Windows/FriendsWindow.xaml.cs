using Mist.Helper;
using Mist.Model;
using Mist.UserControls;
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
    /// Interaction logic for FriendsWindow.xaml
    /// </summary>
    public partial class FriendsWindow : Window
    {
        public User User;
        public FriendsWindow(User user)
        {
            InitializeComponent();
            User = user;
            RefreshThis();
            this.Owner = WindowManager.GetWindow<MainWindow>();
        }

        private void RefreshThis()
        {
            pfp_Image.Source = ImageHelper.GetImage(User.Pfp);
            nickname_Label.Content = User.Nickname;
            var friends = User.GetFriends();
            foreach ( var friend in friends )
            {
                friends_ListBox1.Items.Add(new ProfileFriendUserControl(friend));
            }
            foreach (var item in friends_ListBox1.Items)
            {
                var pfuc = item as ProfileFriendUserControl;
                pfuc.PreviewMouseLeftButtonDown -= pfuc.UserControl_PreviewMouseLeftButtonDown;
            }
        }

        private void resize_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                this.DragMove();
            }
        }
    }
}
