using Microsoft.EntityFrameworkCore.ValueGeneration;
using Mist.Helper;
using Mist.Model;
using Mist.UserControls;
using Mist.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public User User;
        public List<Game> UserGames;
        public List<UserComment> UserComments;
        public List<Friendship> UserFriendShips = new List<Friendship>();
        public ProfilePage(User user)
        {
            InitializeComponent();
            User = user;
            User = App.Context.Users.Where(u => u.Id == user.Id).First();
            UserGames = App.Context.UserGames.Where(ug => ug.User == User).Select(ug => ug.Game).ToList();
            UserComments = App.Context.UserComments.Where(uc => uc.User == User).ToList();
            UserFriendShips = App.Context.Friendships
                                        .Where(f => !f.Pending && (f.Sender == User || f.Recipient == User))
                                        .ToList();
            RefreshProfileInfo();
            RefreshRecentGames();
            RefreshComments();
            RefreshButtons();
            RefreshGroups();
            RefreshFriends();
        }

        private void RefreshProfileInfo()
        {
            if (User.Status)
            {
                status_Border.BorderBrush = Brushes.DeepSkyBlue;
                status_Label.Content = "В сети";
                status_Label.Foreground = Brushes.DeepSkyBlue;
            }
            else
            {
                status_Border.BorderBrush = new SolidColorBrush(Color.FromRgb(106, 106, 106));
                status_Label.Content = "Не в сети";
                status_Label.Foreground = new SolidColorBrush(Color.FromRgb(106, 106, 106));
            }

            pfp_Image.Source = ImageHelper.GetImage(User.Pfp);
            nickname_Label.Content = User.Nickname;
            if (App.Context.Users.Where(u => u == User).Select(u => u.DisplayCountry) != null)
            {

            }
            else
                country_StackPanel.Visibility = Visibility.Collapsed;
            if (User.Bio != null)
                bio_TextBlock.Text = User.Bio;
            else bio_TextBlock.Text = "Пользователь не предоставил информации.";
            mistLevel_TextBlock.Text = $"{UserGames.Count}";
            gameCount_Label.Content = $"{UserGames.Count}";
            groupCount_Label.Content = $"{App.Context.GroupMembers.Where(gm => gm.MemberId == User.Id).Select(gm => gm.Group).ToList().Count}";
            friendsCount_Label.Content = $"{UserFriendShips.Count}";
            if (User.DisplayCountry!= null)
            {
                countryFlag_TextBlock.Text = EmojiHelper.ConvertUnicodeToEmoji(User.DisplayCountry.EmojiU);
                country_TextBlock.Text = User.DisplayCountry.Name;
                if (User.DisplayState != null)
                {
                    state_TextBlock.Text = User.DisplayState.Name + ", ";
                    if (User.DisplayCity != null)
                    {
                        city_TextBlock.Text = User.DisplayCity.Name + ", ";
                    }
                    else
                    {
                        city_TextBlock.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    state_TextBlock.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                country_StackPanel.Visibility = Visibility.Collapsed;
            }

            // handle buttons
        }

        private void RefreshRecentGames()
        {
            if (UserGames.Any())
            {
                var sortedUserGames = UserGames.OrderByDescending(ug => ug.Id).Take(3).ToList();
                foreach (var userGame in sortedUserGames)
                {
                    recentGames_StackPanel.Children.Add(new ProfileGameUserControl(userGame) { Margin = new Thickness(0, 0, 0, 20) });
                }
            }
            else
            {
                recentActivity_StackPanel.Visibility = Visibility.Collapsed;
            }
        }
        private void RefreshComments()
        {
            comments_ListBox.Items.Clear();
            var userComments = App.Context.UserComments.Where(uc => uc.User == User).ToList();
            if (userComments.Any())
            {
                var newUserComments = userComments.OrderByDescending(uc => uc.Id);
                foreach (var comment in newUserComments)
                {
                    comments_ListBox.Items.Add(new GroupCommentUserControl(comment));
                }
            }
            pfpUserControl_Grid.Children.Add(new PFPUserControl(App.CurrentUser));
        }

        private void RefreshButtons()
        {
            if (User.Id == App.CurrentUser.Id)
            {
                editProfile_Button.Visibility = Visibility.Visible;
                addFriend_Button.Visibility = Visibility.Collapsed;
                removeFriend_Button.Visibility = Visibility.Collapsed;
                message_Button.Visibility = Visibility.Collapsed;
            }
            else if (App.CurrentUser.IsFriendsWith(User))
            {
                editProfile_Button.Visibility = Visibility.Collapsed;
                addFriend_Button.Visibility = Visibility.Collapsed;
                removeFriend_Button.Visibility = Visibility.Visible;
                message_Button.Visibility = Visibility.Visible;
            }
            else if (!App.CurrentUser.IsFriendsWith(User))
            {
                editProfile_Button.Visibility = Visibility.Collapsed;
                addFriend_Button.Visibility = Visibility.Visible;
                removeFriend_Button.Visibility = Visibility.Collapsed;
                message_Button.Visibility = Visibility.Collapsed;
            }
        }

        public void RefreshGroups()
        {
            groups_StackPanel.Children.Clear();
            var userGroups = App.Context.GroupMembers.Where(gm => gm.MemberId == User.Id).Select(gm => gm.Group).ToList();
            if (userGroups.Any())
            {
                foreach (var group in userGroups)
                {
                    groups_StackPanel.Children.Add(new ProfileGroupUserControl(group) { Margin = new Thickness(0,0,0,10)});
                }
            }
            else
            {
                groups_StackPanel.Visibility = Visibility.Collapsed;
            }
        }

        public void RefreshFriends()
        {
            friends_ListBox.Items.Clear();
            var friends = User.GetFriends();
            foreach (var friend in friends)
            {
                friends_ListBox.Items.Add(new ProfileFriendUserControl(friend));
            }
        }
        public void RefreshEverything()
        {
            RefreshProfileInfo();
            RefreshRecentGames();
            RefreshComments();
            RefreshButtons();
            RefreshGroups();
            RefreshFriends();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 0, m, 50);
            main_Grid.Margin = margin;
        }

        private void addFriend_Button_Click(object sender, RoutedEventArgs e)
        {
            var confWin = new ConfirmationWindow();
            var curFriendship = App.Context.Friendships.Where(f => f.SenderId == App.CurrentUser.Id && f.RecipientId == User.Id).FirstOrDefault();
            if (curFriendship == null)
            {
                confWin.text_TextBlock.Text = $"Вы уверены, что хотите добавить пользователя {User.Nickname} в друзья?";
                confWin.ShowDialog();
                if (confWin.Confirmed)
                {
                    App.Context.Friendships.Add(new Friendship(App.CurrentUser.Id, User.Id, true));
                    App.Context.SaveChanges();
                    App.Context = new MistContext();
                }
            }
            else
            {
                confWin.text_TextBlock.Text = $"Вы уже отправили {User.Nickname} заявку в друзья. Ожидайте принятия.";
                confWin.confirm_Button.Visibility = Visibility.Collapsed;
                confWin.conf_Label.Visibility = Visibility.Collapsed;
                confWin.ShowDialog();
            }
        }

        private void removeFriend_Button_Click(object sender, RoutedEventArgs e)
        {
            var confWin = new ConfirmationWindow();
            confWin.text_TextBlock.Text = $"Вы уверены, что хотите удалить пользователя {User.Nickname} из друзей?";
            confWin.ShowDialog();
            if (confWin.Confirmed)
            {
                // Find the friendship where the current user is either the sender or recipient
                var friendship = App.Context.Friendships
                    .FirstOrDefault(f => (f.SenderId == App.CurrentUser.Id && f.RecipientId == User.Id && !f.Pending) ||
                                         (f.RecipientId == App.CurrentUser.Id && f.SenderId == User.Id && !f.Pending));

                if (friendship != null)
                {
                    // Remove the friendship and save changes
                    App.Context.Friendships.Remove(friendship);
                    App.Context.SaveChanges();
                }

                // Refresh the context
                App.Context = new MistContext();
                RefreshEverything();
            }
        }

        private void message_Button_Click(object sender, RoutedEventArgs e)
        {
            new MessengerWindow(User).Show();
        }

        private void editProfile_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new EditProfilePage(User));
        }

        private void leaveComment_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(comment_TextBox.Text))
            {
                App.Context.UserComments.Add(new UserComment(User.Id, App.CurrentUser.Id, comment_TextBox.Text, DateTime.Now));
                App.Context.SaveChanges();
                App.Context = new MistContext();
                RefreshComments();
                comment_TextBox.Text = String.Empty;
            }
        }
    }
}
