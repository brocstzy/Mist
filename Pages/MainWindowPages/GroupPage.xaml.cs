using Mist.Helper;
using Mist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        public Group Group;
        public GroupPage(Group group)
        {
            InitializeComponent();
            Group = group;
            RefreshGroupInfo();
        }

        public void RefreshGroupInfo()
        {
            groupPfp_Image.Source = ImageHelper.GetImage(Group.Pfp);
            groupName_Label.Content = Group.Name;
            groupTag_Label.Content = Group.Tag;
            aboutGroup_Label.Content = $"O {Group.Name}";
            groupBio_TextBlock.Text = Group.Bio;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }
    }
}
