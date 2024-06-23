using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages;
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

namespace Mist.UserControls
{
    /// <summary>
    /// Interaction logic for ProfileGroupUserControl.xaml
    /// </summary>
    public partial class ProfileGroupUserControl : UserControl
    {
        public Group Group;
        public ProfileGroupUserControl(Group group)
        {
            InitializeComponent();
            Group = group;
            RefreshThis();
        }
        public void RefreshThis()
        {
            group_Image.Source = ImageHelper.GetImage(Group.Pfp);
            groupName_Label.Content = Group.Name;
            membersCount_Label.Content = App.Context.GroupMembers.Where(gm => gm.GroupId == Group.Id).ToList().Count + " участник (ов)";
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = null;
        }

        private void UserControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new GroupPage(Group));
        }
    }
}
