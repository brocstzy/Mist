using Mist.Helper;
using Mist.Pages.MainWindowPages.CommunityPagePages;
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
    /// Interaction logic for CommunityPage.xaml
    /// </summary>
    public partial class CommunityPage : Page
    {
        public CommunityPage()
        {
            InitializeComponent();
            groupType_ComboBox.SelectedIndex = 0;
        }
        public void RefreshGroups()
        {
            groups_ListBox.Items.Clear();
            var groups = App.Context.Groups.Where(x => x.OwnerId == App.CurrentUser.Id).ToList();
            if (groups.Any() )
            {
                foreach ( var group in groups )
                {
                    groups_ListBox.Items.Add(new GroupUserControl(group));
                }
                noGroups_Label.Visibility = Visibility.Collapsed;
            }
            else
            {
                noGroups_Label.Visibility= Visibility.Visible;
            }
        }

        public void RefreshDevGroups()
        {
            groups_ListBox.Items.Clear();
            var devGroups = App.Context.Developers.Where(x => x.OwnerId == App.CurrentUser.Id).ToList();
            if (devGroups.Any())
            {
                foreach (var devGroup in devGroups)
                {
                    groups_ListBox.Items.Add(new DevGroupUserControl(devGroup));
                }
                noGroups_Label.Visibility = Visibility.Collapsed;
            }
            else
            {
                noGroups_Label.Visibility = Visibility.Visible;
            }
        }

        private void createGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new CreateGroupPage());
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

        private void groupType_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (groupType_ComboBox.SelectedIndex == 0)
            {
                RefreshGroups();
            }
            else
            {
                RefreshDevGroups();
            }
        }
    }
}
