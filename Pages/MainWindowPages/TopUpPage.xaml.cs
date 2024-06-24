using Mist.Helper;
using Mist.Model;
using Mist.Windows;
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
    /// Interaction logic for TopUpPage.xaml
    /// </summary>
    public partial class TopUpPage : Page
    { 
        public TopUpPage()
        {
            InitializeComponent();
            
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void topUp_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(balance_TextBox.Text))
            {
                balance_TextBox.BorderBrush = Brushes.Red;
                return;
            }
            var topUp = Decimal.TryParse(balance_TextBox.Text, out decimal result);
            App.Context.Users.Where(u => u.Id == App.CurrentUser.Id).First().Balance += result ;
            App.Context.SaveChanges();
            App.Context = new MistContext();
            PageManager.MainFrame.Navigate(new StorePage());
            ((MainWindow)WindowManager.GetWindow<MainWindow>()).balance_Label.Content = App.Context.Users.Where(u => u.Id == App.CurrentUser.Id).First().Balance + " руб.";
        }

        private bool IsNumeric(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void balance_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }
    }
}
