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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for SearchFriendsPage.xaml
    /// </summary>
    public partial class SearchFriendsPage : Page
    {
        public string SortingString = "";
        public SearchFriendsPage()
        {
            InitializeComponent();
            RefreshFriends();
        }

        private void RefreshFriends()
        {
            friends_StackPanel.Children.Clear();
            if (String.IsNullOrWhiteSpace(SortingString))
            {
                var users = App.Context.Users.ToList();
                users.Remove(users.FirstOrDefault(u => u.Id == App.CurrentUser.Id));
                foreach (var user in users)
                {
                    friends_StackPanel.Children.Add(new SearchFriendUserControl(user));
                }
            }
            else
            {
                var users = App.Context.Users.Where(u => u.Nickname.Contains(SortingString)).ToList();
                users.Remove(users.FirstOrDefault(u => u.Id == App.CurrentUser.Id));
                foreach (var user in users)
                {
                    friends_StackPanel.Children.Add(new SearchFriendUserControl(user));
                }
            }
            
        }

        private void search_Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshFriends();
        }

        private void nickname_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortingString = nickname_TextBox.Text;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 50);
            main_Grid.Margin = margin;
        }
    }
}
