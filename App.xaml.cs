using Mist.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MistContext Context = new MistContext();

        public static User CurrentUser { get; set; }
        public static Cursor Cursor { get; set; }
        public static Page CurrentPage { 
            get;
            set; 
        }
        private void TitleBarButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var window = Window.GetWindow(btn);
            if (btn.Name.Equals("closeWindow_Button"))
            {
                window.Close();
            }
            else if (btn.Name.Equals("minimizeWindow_Button"))
            {
                window.WindowState = WindowState.Minimized;
            }
            else if (btn.Name.Equals("maximizeWindow_Button"))
            {
                window.Top = SystemParameters.WorkArea.Top;
                window.Left = SystemParameters.WorkArea.Left;
                window.Width = SystemParameters.WorkArea.Width;
                window.Height = SystemParameters.WorkArea.Height;

            }
            else if (btn.Name.Equals("restoreWindow_Button"))
            {
                window.WindowState = WindowState.Normal;
            }
        }

        public static void CommonTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            ((Control)sender).BorderThickness = new Thickness(0.5);
            ((Control)sender).BorderBrush = Brushes.Black;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = ((Page)sender).ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            var mainGrid = (Grid)((Page)sender).FindName("main_Grid");
            mainGrid.Margin = margin;
        }
        //protected override async void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    // Ensure the application shuts down when the main window is closed
        //    //this.ShutdownMode = ShutdownMode.OnLastWindowClose;

        //    while (true)
        //    {
        //        var zxc = App.Current.Windows;
        //        await Task.Delay(5000);
        //    }
        //}
    }

}
