using Mist.Helper;
using Mist.Model;
using Mist.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            RefreshGroupComments();
        }

        public void RefreshGroupInfo()
        {
            groupPfp_Image.Source = ImageHelper.GetImage(Group.Pfp);
            groupName_Label.Content = Group.Name;
            groupTag_Label.Content = Group.Tag;
            aboutGroup_Label.Content = $"O {Group.Name}";
            groupBio_TextBlock.Text = Group.Bio;
            using (MistContext mc = new MistContext())
            {
                commentsCount_Label.Content = mc.GroupComments.Where(x => x.GroupId == Group.Id).Count();
            }
            pfpUserControl_Grid.Children.Add(new PFPUserControl(App.CurrentUser));
        }

        public void RefreshGroupComments()
        {
            comments_ListBox.Items.Clear();
            using (MistContext mc = new MistContext())
            {
                var comments = mc.GroupComments.Where(x => x.GroupId == Group.Id).OrderByDescending(x=> x.Timestamp).ToList();
                foreach (var comment in comments)
                {
                    comments_ListBox.Items.Add(new GroupCommentUserControl(comment, mc.Users.Where(x => x.Id == comment.PosterId).First()));
                }
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void leaveComment_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(comment_TextBox.Text))
            {
                comment_TextBox.BorderBrush = Brushes.Red;
                return;
            }
            using (MistContext mc = new MistContext())
            {
                mc.GroupComments.Add(new GroupComment(Group.Id, App.CurrentUser.Id, comment_TextBox.Text, DateTime.Now));
                mc.SaveChanges();
            }
            RefreshGroupComments();
        }

        private void comment_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = null;
        }
    }
}
