using Mist.Helper;
using Mist.Model;
using Mist.UserControls;
using System.Globalization;
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
            aboutGroup_Label.Content = $"O {Group.Name.ToUpper()}";
            groupBio_TextBlock.Text = Group.Bio;
            pfpUserControl_Grid.Children.Add(new PFPUserControl(App.CurrentUser));
            membersCount_Label.Content = Group.GetMembersCount();
            members_Label.Content = Group.GetMembersCount() switch
            {
                1 => "УЧАСТНИК",
                2 => "УЧАСТНИКА",
                3 => "УЧАСТНИКА",
                4 => "УЧАСТНИКА",
                _ => "УЧАСТНИКОВ"
            };
            creationDate_Label.Content = $"{Group.CreationDate.Day} {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Group.CreationDate.Month)} {Group.CreationDate.Year}";
            adminUserControl_Grid.Children.Add(new PFPUserControl(Group.Owner));
            if (App.CurrentUser.IsInGroup(Group))
            {
                enterGroup_Button.Visibility = Visibility.Collapsed;
                leaveGroup_Button.Visibility = Visibility.Visible;
            }
            else
            {
                enterGroup_Button.Visibility = Visibility.Visible;
                leaveGroup_Button.Visibility = Visibility.Collapsed;
            }

        }

        public void RefreshGroupComments()
        {
            comments_ListBox.Items.Clear();
            using (MistContext mc = new MistContext())
            {
                var comments = mc.GroupComments.Where(x => x.GroupId == Group.Id).OrderByDescending(x => x.Timestamp).ToList();
                foreach (var comment in comments)
                {
                    comments_ListBox.Items.Add(new GroupCommentUserControl(comment, comment.Poster));
                }
            }
            using (MistContext mc = new MistContext())
            {
                var commentsCount = mc.GroupComments.Where(x => x.GroupId == Group.Id).Count();
                commentsCount_Label.Content = commentsCount;
                comments_Label.Content = commentsCount switch
                {
                    1 => "Комментарий",
                    2 => "Комментария",
                    3 => "Комментария",
                    4 => "Комментария",
                    _ => "Комментариев"
                };
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 50);
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
            comment_TextBox.Text = "";
            RefreshGroupComments();
        }

        private void comment_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = null;
        }

        private void leaveGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void enterGroup_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
