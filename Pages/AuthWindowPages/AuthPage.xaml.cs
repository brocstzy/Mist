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
using Mist.Helper;

namespace Mist.Pages.AuthWindowPages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private AuthWindow _aw = (AuthWindow)WindowManager.GetWindow<AuthWindow>();
        public AuthPage()
        {
            InitializeComponent();
            SetBackground();
            ButtonPainter.SetButtonBackground(logIn_Button);
            
        }
        public List<Control> GetEmptyFields()
        {
            List<Control> emptyControls = new List<Control>();
            if (String.IsNullOrWhiteSpace(login_textBox.Text))
                emptyControls.Add(login_textBox);
            if (String.IsNullOrWhiteSpace(password_PasswordBox.Password))
                emptyControls.Add(password_PasswordBox);
            return emptyControls;
        }

        public void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 1);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(7, 15, 43), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 0), 1.0));
            this.Background = bg;
        }

        private void titleBar_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
                _aw.DragMove();
        }

        private void logIn_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonPainter.SetButtonBackgroundHover(sender);
        }

        private void logIn_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonPainter.SetButtonBackground(sender);
        }

        private void logIn_Button_Click(object sender, RoutedEventArgs e)
        {
            var emptyFields = GetEmptyFields();
            if (emptyFields.Any())
            {
                foreach (var field in emptyFields)
                {
                    ((Control)field).BorderBrush = Brushes.Red;
                }
                return;
            }
            else
            {
                var user = App.Context.Users.Where(x => x.Login.Equals(login_textBox.Text) && x.Password.Equals(password_PasswordBox.Password)).FirstOrDefault();
                if (user != null)
                {
                    App.CurrentUser = user;
                    new MainWindow().Show();
                    WindowManager.Close<AuthWindow>();
                }
                else
                {
                    incorrectPassword_Label.Visibility = Visibility.Visible;
                    login_textBox.BorderBrush = Brushes.Red;
                    password_PasswordBox.BorderBrush = Brushes.Red;
                }
            }
        }

        private void createAccount_Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            ManageAccountWindow maw = new ManageAccountWindow(2);
            maw.Owner = WindowManager.GetWindow<AuthWindow>();
            maw.ShowDialog();
        }

        private void restoreAccount_Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ManageAccountWindow maw = new ManageAccountWindow(1);
            maw.Owner = WindowManager.GetWindow<AuthWindow>();
            maw.ShowDialog();
        }

        private void PasswordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void PasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((Control)sender).BorderBrush = new SolidColorBrush(Color.FromRgb(50, 53, 60));
            incorrectPassword_Label.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((Control)sender).BorderBrush = new SolidColorBrush(Color.FromRgb(50, 53, 60));
            incorrectPassword_Label.Visibility = Visibility.Collapsed;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logIn_Button_Click(sender, e);
            }
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logIn_Button_Click(sender, e);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            login_textBox.Focus();
        }
    }
}
