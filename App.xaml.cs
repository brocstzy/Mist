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
        }

        public static void CommonTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            ((Control)sender).BorderThickness = new Thickness(0.5);
            ((Control)sender).BorderBrush = Brushes.Black;
        }
    }

}
