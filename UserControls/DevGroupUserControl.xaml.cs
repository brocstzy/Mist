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
    /// Interaction logic for DevGroupUserControl.xaml
    /// </summary>
    public partial class DevGroupUserControl : UserControl
    {
        public Developer Developer;
        public DevGroupUserControl(Developer developer)
        {
            InitializeComponent();
            Developer = developer;
            RefreshDevGroup();
        }

        public void RefreshDevGroup()
        {
            devGroup_Image.Source = ImageHelper.GetImage(Developer.Pfp);
            devGroupName_Label.Content = Developer.Name;
            followersCount_Label.Content = $"{Developer.DeveloperFollowers.Count} подписчиков";
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new DevGroupPage(Developer));
        }
    }
}
