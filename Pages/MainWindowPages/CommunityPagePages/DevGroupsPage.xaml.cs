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

namespace Mist.Pages.MainWindowPages.CommunityPagePages
{
    /// <summary>
    /// Interaction logic for DevGroupsPage.xaml
    /// </summary>
    public partial class DevGroupsPage : Page
    {
        public DevGroupsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDevGroups();
        }

        public void RefreshDevGroups()
        {
            groups_StackPanel.Children.Clear();
            var groups = App.Context.Developers.Where(x => x.OwnerId == App.CurrentUser.Id).ToList();
            if (groups.Any())
            {
                foreach (var group in groups)
                {
                    groups_StackPanel.Children.Add(new DevGroupUserControl(group));
                }
            }
        }
    }
}
