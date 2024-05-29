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
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
