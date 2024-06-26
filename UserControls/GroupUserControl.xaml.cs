﻿using Mist.Helper;
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
    /// Interaction logic for GroupUserControl.xaml
    /// </summary>
    public partial class GroupUserControl : UserControl
    {
        public Group Group;
        public GroupUserControl(Group group)
        {
            InitializeComponent();
            Group = group;
            RefreshGroup();
        }

        public void RefreshGroup()
        {
            Group_Image.Source = ImageHelper.GetImage(Group.Pfp);
            GroupName_Label.Content = Group.Name;
            using (MistContext mc = new MistContext())
            {
                var members = mc.GroupMembers.Where(x => x.GroupId == Group.Id).Select(x => x.Member).ToList();
                membersCount_Label.Content = members.Any() ? $"{members.Count} участников" : "0 участников";
            }
            groupType_Label.Content = Group.IsPrivate ? "Частная группа" : "Публичная группа";
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new GroupPage(Group));
        }
    }
}
