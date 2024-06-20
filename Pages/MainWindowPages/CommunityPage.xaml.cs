using Mist.Helper;
using Mist.Model;
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
        public string SortingValue = "";
        public CommunityPage()
        {
            InitializeComponent();
            groupType_ComboBox.SelectedIndex = 0;
        }
        public void RefreshGroups()
        {
            groups_ListBox.Items.Clear();
            var groups = new List<Group>();
            if (String.IsNullOrWhiteSpace(SortingValue))
            {
                groups = App.Context.GroupMembers.Where(x => x.MemberId == App.CurrentUser.Id).Select(x => x.Group).ToList();
            }
            else
            {
                groups = App.Context.Groups.Where(x => x.Name.Contains(SortingValue)).ToList();
            }
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
            var devGroups = new List<Developer>();
            if (String.IsNullOrWhiteSpace(SortingValue))
            {
                devGroups = App.Context.Developers.Where(x => x.OwnerId == App.CurrentUser.Id).ToList();
            }
            else
            {
                devGroups = App.Context.Developers.Where(x => x.OwnerId == App.CurrentUser.Id && x.Name.Contains(SortingValue)).ToList();
            }
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
            PageManager.MainFrame.Navigate(new CreateDevGroupPage(false));
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

        private void search_Button_Click(object sender, RoutedEventArgs e)
        {
            switch (groupType_ComboBox.SelectedIndex)
            {
                case 0:
                    RefreshGroups();
                    break;
                case 1:
                    RefreshDevGroups();
                    break;
            }
        }

        private void search_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortingValue = search_TextBox.Text;
        }
    }
}
