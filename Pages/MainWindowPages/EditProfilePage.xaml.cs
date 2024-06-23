using Microsoft.Win32;
using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages.CommunityPagePages;
using Mist.Windows;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page
    {
        public User User;

        public Country Country;
        public State State;
        public City City;

        public Country? SelectedCountry = null;
        public State? SelectedState = null;
        public City? SelectedCity = null;

        public byte[] Pfp = null;

        public EditProfilePage(User user)
        {
            InitializeComponent();
            User = user;
            Country = User.DisplayCountry;
            State = User.DisplayState;
            City = User.DisplayCity;
            RefreshThis();
        }

        private void RefreshThis()
        {
            pfp_Image.Source = ImageHelper.GetImage(User.Pfp);
            nickName_Label.Content = User.Nickname;
            if (!String.IsNullOrWhiteSpace(User.Nickname))
            {
                nickname_TextBox.Text = User.Nickname;
            }
            FillComboBoxes();
        }

        private void selectPfp_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите изображение";
            ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
            {
                Pfp = ImageHelper.CreateImage(ofd.FileName);
                ((Button)sender).Content = ofd.FileName.Split('\\').Last();
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 50);
            main_Grid.Margin = margin;
        }

        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ProfilePage(App.CurrentUser));
        }

        private void confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(nickname_TextBox.Text))
            {
                nickname_TextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            var user = App.Context.Users.Where(u => u.Id == User.Id).First();
            if (Pfp != null) user.Pfp = Pfp;
            user.Nickname = nickname_TextBox.Text;
            user.DisplayCountry = SelectedCountry;
            user.DisplayState = SelectedState;
            user.DisplayCity = SelectedCity;
            user.Bio = bio_TextBox.Text;
            App.Context.SaveChanges();
            App.Context = new MistContext();
            PageManager.MainFrame.Navigate(new ProfilePage(user));
        }

        private void FillComboBoxes()
        {
            states_ComboBox.Visibility = Visibility.Collapsed;
            cities_ComboBox.Visibility = Visibility.Collapsed;
            countries_ComboBox.Items.Add("< не выбрано >");
            states_ComboBox.Items.Add("< не выбрано >");
            cities_ComboBox.Items.Add("< не выбрано >");
            var allCountries = App.Context.Countries.Select(c => c.Name).ToList();
            foreach (var item in allCountries)
            {
                countries_ComboBox.Items.Add(item);
            }
            if (User.DisplayCountry != null)
            {
                countries_ComboBox.SelectedItem = User.DisplayCountry.Name;
                if (User.DisplayState != null)
                {
                    states_ComboBox.SelectedItem = User.DisplayState.Name;
                    if (User.DisplayCity != null)
                    {
                        cities_ComboBox.SelectedItem = User.DisplayCity.Name;
                    }
                }
            }
        }

        private void countries_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem;
            if (!selectedItem.Equals("< не выбрано >"))
            {
                var states = App.Context.States.Where(s => s.Country.Name.Equals(selectedItem)).Select(s => s.Name).OrderBy(name => name).ToList();
                states_ComboBox.Items.Clear();
                states_ComboBox.Items.Add("< не выбрано >");
                foreach (var state in states)
                {
                    states_ComboBox.Items.Add(state);
                }
                states_ComboBox.SelectedIndex = 0;
                ShowState();
                SelectedCountry = App.Context.Countries.Where(c => c.Name.Equals(selectedItem)).First();
            }
            else
            {
                HideState();
                HideCity();
                SelectedCountry = null;
                SelectedState = null;
                SelectedCity = null;
            }
        }

        private void states_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem;
            if (selectedItem != null)
            {
                if (!selectedItem.Equals("< не выбрано >"))
                {
                    var cities = App.Context.Cities.Where(s => s.State.Name.Equals(selectedItem)).Select(s => s.Name).ToList();
                    cities_ComboBox.Items.Clear();
                    cities_ComboBox.Items.Add("< не выбрано >");
                    foreach (var city in cities)
                    {
                        cities_ComboBox.Items.Add(city);
                    }
                    cities_ComboBox.SelectedIndex = 0;
                    ShowCity();
                    SelectedState = App.Context.States.Where(s => s.Name.Equals(selectedItem)).First();
                }
                else
                {
                    HideCity();
                    SelectedState = null;
                    SelectedCity = null;
                }
            }
        }

        private void cities_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem;
            if (selectedItem != null)
            {
                if (!selectedItem.Equals("< не выбрано >"))
                {
                    SelectedCity = App.Context.Cities.Where(c => c.Name.Equals(selectedItem)).First();
                }
                else
                {
                    SelectedCity = null;
                }
            }
        }

        private void HideState()
        {
            state_Label.Visibility = Visibility.Collapsed;
            states_ComboBox.Visibility = Visibility.Collapsed;
        }
        private void HideCity()
        {
            city_Label.Visibility = Visibility.Collapsed;
            cities_ComboBox.Visibility = Visibility.Collapsed;
        }

        private void ShowState()
        {
            state_Label.Visibility = Visibility.Visible;
            states_ComboBox.Visibility = Visibility.Visible;
        }
        private void ShowCity()
        {
            city_Label.Visibility = Visibility.Visible;
            cities_ComboBox.Visibility = Visibility.Visible;
        }

        private void nickname_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = System.Windows.Media.Brushes.Black;
        }
    }
}
