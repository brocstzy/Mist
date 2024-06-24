using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages;
using Mist.Pages.MainWindowPages.CommunityPagePages;
using Mist.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mist.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window _mw = new Window();
        private User CurrentUser = App.CurrentUser;

        public List<StackPanel> stackpanels = new List<StackPanel>();
        public List<Label> titleBarLabels = new List<Label>();
        public List<Label> dropDownLabels = new List<Label>();
        public List<Control> controls = new List<Control>();

        public Label activeLabel = new Label();
        public MainWindow()
        {
            InitializeComponent();
            FillHelperLists();
            SetUserData();
            AddLabelEvents();
            RefreshNotifications();
        }

        public void SetUserData()
        {
            pfp_Image.Source = ImageHelper.GetImage(App.CurrentUser.Pfp);
            nickname_Label.Content = CurrentUser.Nickname + "  ▼";
            balance_Label.Content = CurrentUser.Balance.ToString() + " руб.";
        }

        public void AddLabelEvents()
        {
            store_Label.PreviewMouseLeftButtonDown += TopLabel_PreviewMouseLeftButtonDown;
            library_Label.PreviewMouseLeftButtonDown += TopLabel_PreviewMouseLeftButtonDown;
            community_Label.PreviewMouseLeftButtonDown += TopLabel_PreviewMouseLeftButtonDown;
            gigaProfile_Label.PreviewMouseLeftButtonDown += TopLabel_PreviewMouseLeftButtonDown;
        }

        private void TopLabel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResetLabelColor();
            activeLabel = (Label)sender;
            SetLabelColor();
        }

        public void FillHelperLists()
        {
            stackpanels.AddRange(new List<StackPanel>
            {
                mist_Label_StackPanel,
                view_Label_StackPanel,
                friends_Label_StackPanel,
                games_Label_StackPanel,
                help_Label_StackPanel,
                profile_Label_StackPanel
            });
            titleBarLabels.AddRange(new List<Label>
            {
                mist_Label,
                view_Label,
                friends_Label,
                games_Label,
                help_Label
            });
            foreach (var stackpanel in stackpanels)
            {
                foreach (var item in stackpanel.Children)
                {
                    if (item is Separator)
                        ;
                    else
                        dropDownLabels.Add((Label)item);
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            _mw = WindowManager.GetWindow<MainWindow>();
            PageManager.MainFrame = MainFrame;
        }

        private void titleBar_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                _mw.DragMove();
            }

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Width == SystemParameters.WorkArea.Width &&
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Height == SystemParameters.WorkArea.Height &&
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Top == 0 &&
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Left == 0)
            {
                maximizeWindow_Button.Visibility = Visibility.Collapsed;
                restoreWindow_Button.Visibility = Visibility.Visible;
            }
            else
            {
                maximizeWindow_Button.Visibility = Visibility.Visible;
                restoreWindow_Button.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            store_Label_MouseLeftButtonDown(null, null);
            activeLabel = store_Label;
            SetLabelColor();
        }

        private void logOut_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowManager.Close<MainWindow>();
            App.Context.Users.Where(u => u.Id == App.CurrentUser.Id).First().Status = false;
            App.Context.SaveChanges();
            App.Context = new MistContext();
            new AuthWindow().Show();
        }

        public void ResetLabelColor()
        {
            if (activeLabel != null)
                activeLabel.Foreground = new SolidColorBrush(Color.FromRgb(217, 217, 217));

        }
        public void SetLabelColor()
        {
            if (activeLabel != null)
                activeLabel.Foreground = new SolidColorBrush(Color.FromRgb(6, 143, 255));
        }

        private void store_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new StorePage());
        }

        private void library_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new LibraryPage());
        }

        private void community_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new CommunityPage());
        }

        private void gigaProfile_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage(App.CurrentUser));
        }

        private void addGame_Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new AddGamePage());
        }

        private void friends_Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void addGame_Label_MouseEnter(object sender, MouseEventArgs e)
        {
            Image theImage = (Image)((Label)sender).FindName("addGame_Image");
            theImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/add-game-white.png"));
            addGame_TextBlock.Foreground = Brushes.White;
        }

        private void addGame_Label_MouseLeave(object sender, MouseEventArgs e)
        {
            Image theImage = (Image)((Label)sender).FindName("addGame_Image");
            theImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/add-game.png"));
            addGame_TextBlock.Foreground = new SolidColorBrush(Color.FromRgb(191, 191, 191));
        }

        private void friendsWindow_Label_MouseEnter(object sender, MouseEventArgs e)
        {
            Image theImage = (Image)((Label)sender).FindName("friends_Image");
            theImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/friends-white.png"));
            friends_TextBlock.Foreground = Brushes.White;
        }

        private void friendsWindow_Label_MouseLeave(object sender, MouseEventArgs e)
        {
            Image theImage = (Image)((Label)sender).FindName("friends_Image");
            theImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/friends.png"));
            friends_TextBlock.Foreground = new SolidColorBrush(Color.FromRgb(191, 191, 191));
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            App.CurrentPage = MainFrame.Content as Page;
        }

        public void RefreshNotifications()
        {
            notifications_StackPanel.Children.Clear();
            var friendships = App.Context.Friendships.Where(f => f.RecipientId == App.CurrentUser.Id && f.Pending).ToList();
            if (friendships.Any())
            {
                notifications_Button.Content = new Image()
                {
                    Source = ImageHelper.GetImageFromPath("/Assets/bell-green.png")
                };
                noNotifications_Label.Visibility = Visibility.Collapsed;
                foreach (var  friendship in friendships)
                {
                    notifications_StackPanel.Children.Add(new FriendNotificationUserControl(friendship.Sender));
                }
            }
            else
            {
                noNotifications_Label.Visibility = Visibility.Visible;
                notifications_Button.Content = new Image()
                {
                    Source = ImageHelper.GetImageFromPath("/Assets/bell.png")
                };
            }
        }

        private void notifications_Button_Click(object sender, RoutedEventArgs e)
        {
            notifications_Button_StackPanel.Visibility = Visibility.Visible;
        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var sp in stackpanels)
            {
                sp.Visibility = Visibility.Collapsed;
            }
            notifications_Button_StackPanel.Visibility = Visibility.Collapsed;
        }

        private void myWallet_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new TopUpPage());
        }

        private void myProfile_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ProfilePage(App.CurrentUser));
        }

        private void goToLibrary_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new LibraryPage());
        }

        private void friendsWindow_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new FriendsWindow(App.CurrentUser) { Owner = this }.Show();
        }

        private void goToLibrary_Label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new LibraryPage());
        }

        private void friendsAndChat_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new FriendsWindow(App.CurrentUser).Show();
        }

        private void quitApp_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void viewFriendsList_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new FriendsWindow(App.CurrentUser).Show();
        }

        private void changeNameAndPfp_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new EditProfilePage(App.CurrentUser));
        }

        private void addFriend_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new SearchFriendsPage()) ;
        }

        private void system_Specs_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new PcSpecsWindow() { Owner = this}.Show();
        }

        private void aboutMist_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new PcSpecsWindow("zxc") { Owner = this }.Show();
        }
    }
}
