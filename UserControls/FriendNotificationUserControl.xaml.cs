using Mist.Helper;
using Mist.Model;
using Mist.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mist.UserControls
{
    /// <summary>
    /// Interaction logic for FriendNotificationUserControl.xaml
    /// </summary>
    public partial class FriendNotificationUserControl : UserControl
    {
        public User User;
        public FriendNotificationUserControl(User user)
        {
            InitializeComponent();
            User = user;
            RefreshThis();
        }

        public void RefreshThis()
        {
            pfp_Image.Source = ImageHelper.GetImage(User.Pfp);
            name_Label.Content = User.Nickname;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            Background.Opacity = 0.2;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = null;
            Background.Opacity = 0.1;
        }

        private void UserControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var confWin = new ConfirmationWindow();
            confWin.text_TextBlock.Text = $"Вы уверены, что хотите добавить пользователя {User.Nickname} в друзья?";
            confWin.ShowDialog();
            if (confWin.Confirmed)
            {
                using (MistContext mc = new MistContext())
                {
                    var friendShip = mc.Friendships.Where(f => f.Sender == User && f.RecipientId == App.CurrentUser.Id).FirstOrDefault();
                    if (friendShip != null)
                    {
                        friendShip.Pending = false;
                        friendShip.FriendsSince = DateTime.Now;
                        mc.SaveChanges();
                    }
                }
                App.Context = new MistContext();
                ((MainWindow)WindowManager.GetWindow<MainWindow>()).RefreshNotifications();
            }
        }
    }
}
