using Mist.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
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
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Security.RightsManagement;
using System.ComponentModel.DataAnnotations;
using Mist.Model;

namespace Mist.Pages.ManageAccountWindowPages
{
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page
    {
        private string _userCountry;
        public CreateAccountPage()
        {
            InitializeComponent();
            GetCountryByIP();
            RefreshCountries();
            ButtonPainter.SetButtonBackground(createAccount_Button);
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
        public bool AreFieldsEmpty()
        {
            if (String.IsNullOrWhiteSpace(login_TextBox.Text) ||
                String.IsNullOrWhiteSpace(password_PasswordBox.Password) ||
                String.IsNullOrWhiteSpace(email_TextBox.Text) ||
                String.IsNullOrWhiteSpace(email2_TextBox.Text) ||
                String.IsNullOrWhiteSpace(nickname_TextBox.Text))
                return true;
            return false;
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
            if (AreFieldsEmpty())
            {
                return;
            }
            if (!IsEmailValid())
            {
                return;
            }
            if (!DoEmailsMatch())
            {
                return;
            }
            if (!DoesUserAgree())
            {
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
            if (((TextBox)sender).Name.Contains("email"))
            {
                if (!Regex.IsMatch(e.Text, "^[a-z_@]$"))
                { 
                    e.Handled = true;
                }
                if (((TextBox)sender).Text.Contains("@") && e.Text == "@")
                {
                    e.Handled = true;
                }
            }
            if (!Regex.IsMatch(e.Text, "^[a-z_]$") && !(((TextBox)sender).Text.Contains("email")))
            {
                e.Handled = true;
            }
            if (((TextBox)sender).Text.Contains("_") && e.Text == "_")
            {
                e.Handled = true;
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
        public void new123()
        {

        }
    }
}
