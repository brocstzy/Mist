using Mist.Model;
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
    /// Interaction logic for GroupCommentUserControl.xaml
    /// </summary>
    public partial class GroupCommentUserControl : UserControl
    {
        public GroupComment Comment;
        public User User;

        public GroupCommentUserControl(GroupComment comment, User user)
        {
            InitializeComponent();
            Comment = comment;
            User = user;
            RefreshComment();
        }

        public void RefreshComment()
        {
            pfpUserControl_Grid.Children.Add(new PFPUserControl(User));
            userNickName_Label.Content = User.Nickname;
            timestamp_Label.Content = Comment.Timestamp;
            comment_TextBlock.Text = Comment.Comment;
        }
    }
}
