using Mist.Helper;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mist.Windows
{
    partial class MainWindowStyles : ResourceDictionary
    {
        public MainWindowStyles()
        {
            InitializeComponent();
        }

        protected void DropdownLabel_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                string labelName = ((Control)sender).Name;
                string stackpanelName = labelName + "_StackPanel";
                object stackpanel = WindowManager.GetWindow<MainWindow>().FindName(stackpanelName);
                ((StackPanel)stackpanel).Visibility = Visibility.Visible;
            }
        }
    }
}
