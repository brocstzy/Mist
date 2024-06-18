using Mist.Model;
using Mist.UserControls;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {
        public int SortingValue = 0;
        public string? SearchingValue = null;
        public StorePage()
        {
            InitializeComponent();
            sort_ComboBox.SelectedIndex = 0;
            SortingValue = sort_ComboBox.SelectedIndex;
            RefreshGames(SearchingValue, SortingValue);
        }

        public void RefreshGames(string? searchValue, int sortingValue)
        {
            games_ListBox.Items.Clear();
            List<Game> games = new List<Game>();
            if (searchValue!= null)
            {
                games = App.Context.Games.Where(x => x.Name.Contains(searchValue)).ToList();
            }
            else
            {
                games = App.Context.Games.ToList();
            }
            switch (sortingValue)
            {
                case 0:
                    games = games.OrderBy(x => x.Name).ToList();
                    break;
                case 1:
                    games = games.OrderBy(x => x.UsdPrice).ToList();
                    break;
                case 2:
                    games = games.OrderByDescending(x => x.UsdPrice).ToList();
                    break;
                case 3:
                    games = games.OrderByDescending(x => x.ReleaseDate).ToList();
                    break;
                case 4:
                    games = games.OrderBy(x => x.ReleaseDate).ToList();
                    break;
                default:
                    break;
            }

            foreach (var g in games)
            {
                games_ListBox.Items.Add(new StoreGameUserControl(g));
            }
        }

        public void SetBackground()
        {
            LinearGradientBrush bg = new LinearGradientBrush();
            bg.StartPoint = new Point(0, 0);
            bg.EndPoint = new Point(1, 0);
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(28, 44, 64), 0.0));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(47, 78, 117), 0.50));
            bg.GradientStops.Add(new GradientStop(Color.FromRgb(28, 44, 64), 1.00));
            this.Background = bg;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetBackground();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void sort_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortingValue = sort_ComboBox.SelectedIndex;
            RefreshGames(SearchingValue, SortingValue);
        }

        private void search_Button_Click(object sender, RoutedEventArgs e)
        {
            SearchingValue = search_TextBox.Text;
            RefreshGames(SearchingValue,SortingValue);
        }

        private void Page_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && search_TextBox.IsKeyboardFocused)
            {
                search_Button_Click(null, null);
            }
        }
    }
}
