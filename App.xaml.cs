using Mist.Model;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            if (btn.Name.Equals("closeWindow_Button"))
                Application.Current.Shutdown();
            
        }
    }

}
