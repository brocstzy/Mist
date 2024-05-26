using Mist.Helper;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mist.Pages.ManageAccountWindowPages
{
    /// <summary>
    /// Interaction logic for RAP_EmailPage.xaml
    /// </summary>
    public partial class RAP_EmailPage : Page
    {
        public RAP_EmailPage()
        {
            InitializeComponent();
            SetBackground();
        }
        public Control GetEmptyFields()
        {
            if (String.IsNullOrWhiteSpace(email_TextBox.Text))
                return email_TextBox;
            return null;
        }

        private void forgotEmail_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new RAP_PhonePage());
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

        private void search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (GetEmptyFields() != null)
            {
                email_TextBox.BorderBrush = Brushes.Red;
                email_TextBox.BorderThickness = new Thickness(1);
                return;
            }
        }

        private void Page_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                search_Button_Click(sender, e);
        }

        private void email_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            email_TextBox.BorderThickness = new Thickness(0.5);
            email_TextBox.BorderBrush = Brushes.Black;

        }
    }
}
