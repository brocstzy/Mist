using Mist.Model;
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

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for CreateDevGroupPage.xaml
    /// </summary>
    public partial class CreateDevGroupPage : Page
    {
        public CreateDevGroupPage()
        {
            InitializeComponent();
        }

        private void choosePfp_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void chooseBackgroundPfp_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createDevGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Context.Developers.Add(new Developer(App.CurrentUser.Id,
                                                     null,
                                                     null,
                                                     groupName_TextBox.Text,
                                                     bio_TextBox.Text));
            App.Context.SaveChanges();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }
    }
}
