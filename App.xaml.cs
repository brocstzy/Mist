using Mist.Model;
using System.Windows;
using System.Windows.Controls;

namespace Mist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MistContext Context = new MistContext();
        private void TitleBarButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var window = Window.GetWindow(btn);
            if (btn.Name.Equals("closeWindow_Button"))
            {
                window.Close();
            }
        }
    }

}
