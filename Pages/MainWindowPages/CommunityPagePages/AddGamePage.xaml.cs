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

namespace Mist.Pages.MainWindowPages.CommunityPagePages
{
    /// <summary>
    /// Interaction logic for AddGamePage.xaml
    /// </summary>
    public partial class AddGamePage : Page
    {
        public byte[] libraryIcon;
        public byte[] libraryLogo;
        public byte[] backgroundLibraryImage;
        public byte[] verticalLibraryImage;
        public byte[] frontImage;
        public byte[] frontSearchImage;
        public byte[][] screenshots;
        public byte[][] videos;
        public AddGamePage()
        {
            InitializeComponent();
        }

        private void addGame_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            if (!btn.Name.Equals("addGame_Button"))
            {

            }
        }
    }
}
