using Mist.Model;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MistContext Context = new MistContext();
        public static Cursor Cursor { get; set; }
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
                window.Width = SystemParameters.WorkArea.Width;
                window.Height = SystemParameters.WorkArea.Height;
                window.Top = SystemParameters.WorkArea.Top;
                window.Left = SystemParameters.WorkArea.Left;
            }
        }

        public static void CommonTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            ((Control)sender).BorderThickness = new Thickness(0.5);
            ((Control)sender).BorderBrush = Brushes.Black;
        }
    }

}
