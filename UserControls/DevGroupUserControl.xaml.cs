using Mist.Helper;
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
    /// Interaction logic for DevGroupUserControl.xaml
    /// </summary>
    public partial class DevGroupUserControl : UserControl
    {
        public DevGroupUserControl(Developer group)
        {
            InitializeComponent();
            devGroup_Image.Source = ImageHelper.GetImage(group.Pfp);
            devGroupName_Label.Content = group.Name;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
