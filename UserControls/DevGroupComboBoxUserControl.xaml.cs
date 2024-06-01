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
    /// Interaction logic for DevGroupComboBoxUserControl.xaml
    /// </summary>
    public partial class DevGroupComboBoxUserControl : UserControl
    {
        public DevGroupComboBoxUserControl(Developer developer)
        {
            InitializeComponent();
            image.Source = ImageHelper.GetImage(developer.Pfp);
            label.Content = developer.Name;
        }
    }
}
