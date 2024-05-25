using Mist.Helper;
using Mist.Model;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mist.Pages.ManageAccountWindowPages
{
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page
    {
        private string _userCountry = "";
        public CreateAccountPage()
        {
            InitializeComponent();
            GetCountryByIP();
            RefreshCountries();
            ButtonPainter.SetButtonBackground(createAccount_Button);
            SetBackground();
        }
        public void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 1);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(12, 11, 61), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 0), 1.0));
            this.Background = bg;
        }
        public void RefreshCountries()
        {
            countries_ComboBox.Items.Clear();
            var countries = App.Context.Countries.Select(x => x.Name).ToList();
            countries_ComboBox.ItemsSource = countries;
            for (int i = 1; i < countries_ComboBox.Items.Count; i++)
            {
                if (countries_ComboBox.Items[i].ToString().Contains(_userCountry))
                {
                    countries_ComboBox.SelectedIndex = i;
                    return;
                }
            }
        }
        public List<Control> GetEmptyFields()
        {
            List<Control> allBoxes =
            [
                login_TextBox,
                email_TextBox,
                email2_TextBox,
                nickname_TextBox
            ];
            List<Control> emptyBoxes = new List<Control>();
            foreach (TextBox box in allBoxes)
            {
                if (String.IsNullOrWhiteSpace(box.Text))
                {
                    emptyBoxes.Add(box);
                }
            }
            if (String.IsNullOrEmpty(password_PasswordBox.Password))
                emptyBoxes.Add(password_PasswordBox);

            if (emptyBoxes.Count > 0)
                return emptyBoxes;
            return null;
        }
        public bool IsEmailValid()
        {
            if (new EmailAddressAttribute().IsValid(email_TextBox.Text))
                return true;
            return false;
        }
        public bool DoEmailsMatch()
        {
            if (email_TextBox.Text.Equals(email2_TextBox.Text))
                return true;
            return false;
        }
        public bool DoesUserAgree()
        {
            if (age_CheckBox.IsChecked == true)
                return true;
            return false;
        }

        private void createAccount_Button_Click(object sender, RoutedEventArgs e)
        {
            var emptyFields = GetEmptyFields();
            if (emptyFields != null)
            {
                foreach (var box in emptyFields)
                {
                    box.BorderBrush = Brushes.Red;
                }
                return;
            }
            if (!IsEmailValid())
            {
                wrongEmail_Label.Visibility = Visibility.Visible;
                email_TextBox.BorderBrush = Brushes.Red;
                return;
            }
            if (!DoEmailsMatch())
            {
                email_TextBox.BorderBrush = Brushes.Red;
                email2_TextBox.BorderBrush = Brushes.Red;
                emailsNotMatch_Label.Visibility = Visibility.Visible;
                return;
            }
            if (!DoesUserAgree())
            {
                checkBox_Label.Visibility = Visibility.Visible;
                checkBox_Border.BorderThickness = new Thickness(1);
                checkBox_Border.Padding = new Thickness(5);
                return;
            }
            using (MistContext mc = new MistContext())
            {
                mc.Users.Add(new User(login_TextBox.Text,
                                      password_PasswordBox.Password,
                                      DateTime.Now,
                                      email_TextBox.Text,
                                      nickname_TextBox.Text,
                                      Decimal.Zero,
                                      countries_ComboBox.SelectedIndex,
                                      1));
                mc.SaveChanges();
            }
            PageManager.MainFrame.Navigate(new SuccessfulRegistrationPage());
        }

        private void createAccount_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonPainter.SetButtonBackgroundHover(sender);
        }

        private void createAccount_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonPainter.SetButtonBackground(sender);
        }

        public void GetCountryByIP()
        {
            IpInfo ipInfo = new IpInfo();
            string info = new WebClient().DownloadString("http://ipinfo.io");
            ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
            RegionInfo region = new RegionInfo(ipInfo.Country);
            _userCountry = region.EnglishName;
        }

        public class IpInfo
        {
            public string Country { get; set; }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (((TextBox)sender).Name.Equals("nickname_TextBox"))
                return;
            else if (((Control)sender).Name.Contains("email"))
            {
                return;
            }
            else
            {
                if (((TextBox)sender).Name.Contains("email"))
                {
                    if (!Regex.IsMatch(e.Text, "^[a-z_@]$") || e.Text == " ")
                    {
                        e.Handled = true;
                    }
                    if (((TextBox)sender).Text.Contains("@") && e.Text == "@")
                    {
                        e.Handled = true;
                    }
                }
                if (!Regex.IsMatch(e.Text, "^[a-z_]$") && !((TextBox)sender).Text.Contains("email"))
                {
                    e.Handled = true;
                }
                if (((TextBox)sender).Text.Contains("_") && e.Text == "_")
                {
                    e.Handled = true;
                }

            }
        }

        private void PasswordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-z_]$"))
            {
                e.Handled = true;

            }
            else if (((PasswordBox)sender).Password.Contains("_") && e.Text == "_")
            {
                e.Handled = true;
            }

        }
        public void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste && ((Control)sender).Name.Contains("email2"))
            {
                string pastedText = Clipboard.GetText();
                string pattern = "^[a-z]*_?[a-z]*@?[a-z]*.?[a-z]*$";
                if (!Regex.IsMatch(pastedText, pattern))
                {
                    e.Handled = true;
                    return;
                }
            }
            else if (e.Command == ApplicationCommands.Paste)
                e.Handled = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((Control)sender).BorderBrush = new SolidColorBrush(Color.FromRgb(50, 53, 60));
            if (((Control)sender).Name.Contains("email_TextBox"))
                wrongEmail_Label.Visibility = Visibility.Collapsed;
            else if (((Control)sender).Name.Contains("email2"))
            {
                emailsNotMatch_Label.Visibility = Visibility.Collapsed;
                email_TextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(50, 53, 60));
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((Control)sender).BorderBrush = new SolidColorBrush(Color.FromRgb(50, 53, 60));
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void PasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void age_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Label.Visibility = Visibility.Collapsed;
            checkBox_Border.BorderThickness = new Thickness(0);
            checkBox_Border.Padding = new Thickness(0);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }
    }
}
