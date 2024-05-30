using Mist.Helper;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mist.Windows
{
    partial class MainWindowStyles : ResourceDictionary
    {
        Window _mw = new Window();
        public MainWindowStyles()
        {
            InitializeComponent();
        }

        protected void TitleBarLabel_Click(object sender, MouseButtonEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent((DependencyObject)sender) as StackPanel;
            var parentTag = parent.Tag;
            if (parentTag != null)
            {
                if (parentTag.Equals("TitleBar") || parentTag.Equals("Profile"))
                {
                    if (e.ButtonState == e.LeftButton)
                    {
                        _mw = WindowManager.GetWindow<MainWindow>();
                        Label label = ((Label)sender);
                        StackPanel stackpanel = (StackPanel)_mw.FindName(label.Name + "_StackPanel");
                        if (label.Name.Equals("REPLACE_LABEL"))
                            stackpanel = (StackPanel)_mw.FindName("mist_Label_StackPanel");
                        if (label.Name.Equals("nickname_Label") || label.Name.Equals("balance_Label"))
                        {
                            stackpanel = (StackPanel)_mw.FindName("profile_Label_StackPanel");
                            label = (Label)_mw.FindName("profile_Label");
                        }
                        AdjustStackPanel(label, stackpanel);
                    }
                }
            }
        }

        public void AdjustStackPanel(Label label, StackPanel stackpanel)
        {
            Point labelPoint = label.TransformToAncestor(_mw).Transform(new Point(0, 0));
            Thickness margin = stackpanel.Margin;
            margin = new Thickness(labelPoint.X, label.ActualHeight + labelPoint.Y, 0, 0);
            stackpanel.Margin = margin;
            stackpanel.Visibility = Visibility.Visible;
        }
    }
}
