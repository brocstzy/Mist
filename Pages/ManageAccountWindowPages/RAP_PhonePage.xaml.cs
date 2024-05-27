using Mist.Helper;
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

namespace Mist.Pages.ManageAccountWindowPages
{
    /// <summary>
    /// Interaction logic for RAP_PhonePage.xaml
    /// </summary>
    public partial class RAP_PhonePage : Page
    {
        public RAP_PhonePage()
        {
            InitializeComponent();
            SetBackground();
        }

        private void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 1);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(36, 53, 74), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(11, 25, 41), 1.0));
            search_StackPanel.Background = bg;
        }

        public Control GetEmptyFields()
        {
            if (String.IsNullOrWhiteSpace(phone_TextBox.Text))
                return phone_TextBox;
            return null;
        }

        private void forgotPhone_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new RAP_SorryPage());
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                search_Button_Click(sender, e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((Control)sender).BorderThickness = new Thickness(0.5);
            ((Control)sender).BorderBrush = Brushes.Black;
        }

        private void search_Button_Click(object sender, RoutedEventArgs e)
        {
            var emptyFields = GetEmptyFields();
            if (emptyFields != null)
            {
                ((Control)emptyFields).BorderBrush = Brushes.Red;
                ((Control)emptyFields).BorderThickness = new Thickness(1);
            }
        }
    }
}
