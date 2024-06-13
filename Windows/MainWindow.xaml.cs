using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages;
using Mist.Pages.MainWindowPages.CommunityPagePages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var sp in stackpanels)
            {
                sp.Visibility = Visibility.Collapsed;
            }
        }

        private void logOut_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowManager.Close<MainWindow>();
            new MainWindow().Show();
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
            MainFrame.Navigate(new ProfilePage());
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
            _mw = null;
            Application.Current.Shutdown();
        }
    }
}
