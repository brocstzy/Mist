using Mist.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mist.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window _mw = new Window();

        public List<StackPanel> stackpanels = new List<StackPanel>();
        public List<Label> titleBarLabels = new List<Label>();
        public List<Label> dropDownLabels = new List<Label>();
        public List<Control> controls = new List<Control>();
        public MainWindow()
        {
            InitializeComponent();
            FillHelperLists();
        }

        public void FillHelperLists()
        {
            stackpanels.AddRange(new List<StackPanel>
            {
                mist_Label_StackPanel,
                view_Label_StackPanel,
                friends_Label_StackPanel,
                games_Label_StackPanel,
                help_Label_StackPanel
            });
            titleBarLabels.AddRange(new List<Label>
            {
                mist_Label,
                view_Label,
                friends_Label,
                games_Label,
                help_Label
            });
            foreach (var stackpanel in stackpanels)
            {
                foreach (var item in stackpanel.Children)
                {
                    if (item is Separator)
                        ;
                    else
                        dropDownLabels.Add((Label)item);
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            _mw = WindowManager.GetWindow<MainWindow>();
            Point mistPoint = mist_Label.TransformToAncestor(this).Transform(new Point(0,0));
            Thickness margin = mist_Label_StackPanel.Margin;
            margin = new Thickness(mistPoint.X, mist_Label.ActualHeight, 0, 0);
            mist_Label_StackPanel.Margin = margin;
        }

        private void titleBar_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                _mw.DragMove();
            }
            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (this.ActualWidth == SystemParameters.WorkArea.Width &&
            //    this.ActualHeight == SystemParameters.WorkArea.Height)
            //    this.ResizeMode = ResizeMode.NoResize;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var sp in stackpanels)
            {
                sp.Visibility = Visibility.Collapsed;
            }
        }

        private void logOut_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowManager.Close<MainWindow>();
            new MainWindow().Show();
        }
    }
}
