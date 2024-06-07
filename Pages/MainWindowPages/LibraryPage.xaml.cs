using Mist.Helper;
using Mist.Model;
using Mist.UserControls;
using System.Windows;
using System.Windows.Controls;

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for LibraryPage.xaml
    /// </summary>
    public partial class LibraryPage : Page
    {
        public List<Game> GameList;

        public Game SelectedGame;
        private Size oldSize;
        private Thickness logoMargin;
        public LibraryPage()
        {
            InitializeComponent();
            using (MistContext ms = new MistContext())
            {
                GameList = (from ug in ms.UserGames
                            join g in ms.Games on ug.GameId equals g.Id
                            where ug.UserId == App.CurrentUser.Id
                            select g).ToList();
            }
            RefreshGameList();
            logoMargin = logo_Image.Margin;
            logo_Image.Height = 200;
        }

        public void RefreshGameList()
        {
            games_ListBox.Items.Clear();
            foreach (var game in GameList)
            {
                games_ListBox.Items.Add(new LibraryGameUserControl(game));
            }
        }

        public void RefreshSelectedGame()
        {
            background_Image.Source = ImageHelper.GetImage(SelectedGame.BackgroundLibraryImage);
            logo_Image.Source = ImageHelper.GetImage(SelectedGame.LibraryLogo);

            reviewWrite_Label.Content = $"Напишите отзыв для {SelectedGame.Name}";
        }

        private void info_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void games_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGame = ((LibraryGameUserControl)games_ListBox.SelectedItem).Game;
            RefreshSelectedGame();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (e.NewSize.Width > oldSize.Width && e.NewSize.Width < 1210)
            //{
            //    images_Grid.Height++;
            //    logo_Image.Height++;
            //}

            //else if (e.NewSize.Width < oldSize.Width && e.NewSize.Width < 1210)
            //{
            //    images_Grid.Height--;
            //    logo_Image.Height--;
            //}

            //oldSize = new Size(this.ActualWidth, this.ActualHeight);

        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    oldSize = new Size(this.ActualWidth, this.ActualHeight);
        //}
    }
}
