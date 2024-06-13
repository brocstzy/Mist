using iTextSharp.text.io;
using Mist.Helper;
using Mist.Model;
using Mist.UserControls;
using Mist.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public Button SelectedReviewButton;
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
            RefreshTiles();
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
            backgroundBlur_Image.Source = ImageHelper.GetImage(SelectedGame.BackgroundLibraryImage);
            logo_Image.Source = ImageHelper.GetImage(SelectedGame.LibraryLogo);

            verticalInfo_Image.Source = ImageHelper.GetImage(SelectedGame.VerticalLibraryImage);
            bio_textBlock.Text = SelectedGame.Bio;
            using (MistContext mc = new MistContext())
            {
                developerName_Label.Content = mc.Developers.Where(x => x.Id == SelectedGame.DeveloperId).First().Name;
            }
            releaseDate_Label.Content = SelectedGame.ReleaseDate;
            reviewWrite_Label.Content = $"Напишите отзыв для {SelectedGame.Name}";
            pfp_Image.Source = ImageHelper.GetImage(App.CurrentUser.Pfp);
            friendsWhoHave_Label.Text = $"Друзья, у которых есть {SelectedGame.Name}";

            tiles_ScrollViewer.Visibility = Visibility.Collapsed;
            gamePage_ScrollViewer.Visibility = Visibility.Visible;
            
        }

        private void info_Button_Click(object sender, RoutedEventArgs e)
        {
            if (info_Grid.Visibility == Visibility.Collapsed)
                info_Grid.Visibility = Visibility.Visible;
            else
                info_Grid.Visibility = Visibility.Collapsed;
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


            if (tiles_ScrollViewer.Visibility == Visibility.Visible)
            {
                var width = this.ActualWidth;
                var zxc = new UserControl();
                if (width > 1580)
                {
                    foreach (var item in tiles_WrapPanel.Children)
                    {
                        ((UserControl)item).Width = 222;
                        ((UserControl)item).Height = 333;
                    }
                    return;
                }
                if (width > 1280 && width < 1580)
                {
                    foreach (var item in tiles_WrapPanel.Children)
                    {
                        ((UserControl)item).Width = 170;
                        ((UserControl)item).Height = 255;
                    }
                    return;
                }
                if (width < 1280)
                {
                    foreach (var item in tiles_WrapPanel.Children)
                    {
                        ((UserControl)item).Width = 140;
                        ((UserControl)item).Height = 210;
                    }
                    return;
                }
            }
        }

        private void recommend_Button_Click(object sender, RoutedEventArgs e)
        {
            if (checkYes_Image.Visibility == Visibility.Collapsed)
            {
                if (checkNo_Image.Visibility == Visibility.Visible)
                {
                    checkNo_Image.Visibility = Visibility.Collapsed;
                    thumbsDown_Image.Opacity = 1;
                }
                checkYes_Image.Visibility = Visibility.Visible;
                thumbsUp_Image.Opacity = 0.3;
            }
            else
            {
                checkYes_Image.Visibility = Visibility.Collapsed;
                thumbsUp_Image.Opacity = 1;
            }
        }

        private void notRecommend_Button_Click(object sender, RoutedEventArgs e)
        {
            if (checkNo_Image.Visibility == Visibility.Collapsed)
            {
                if (checkYes_Image.Visibility == Visibility.Visible)
                {
                    checkYes_Image.Visibility= Visibility.Collapsed;
                    thumbsUp_Image.Opacity = 1;
                }
                checkNo_Image.Visibility = Visibility.Visible;
                thumbsDown_Image.Opacity = 0.3;
            }
            else
            {
                checkNo_Image.Visibility = Visibility.Collapsed;
                thumbsDown_Image.Opacity = 1;
            }
        }

        private void storePage_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            PageManager.MainFrame.Navigate(new GamePage(SelectedGame));
            this.Cursor = null;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().ResetLabelColor();
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().activeLabel =
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().library_Label;
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().SetLabelColor();
        }

        public void RefreshTiles()
        {
            tiles_ScrollViewer.Visibility = Visibility.Visible;
            gamePage_ScrollViewer.Visibility = Visibility.Collapsed;
            foreach (var game in GameList)
            {
                var uc = new VerticalTileUserControl(game);
                uc.PreviewMouseLeftButtonDown += Uc_PreviewMouseLeftButtonDown;
                tiles_WrapPanel.Children.Add(uc);
                
            }
        }

        private void Uc_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedGame = ((VerticalTileUserControl)sender).Game;
            RefreshSelectedGame();
        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    oldSize = new Size(this.ActualWidth, this.ActualHeight);
        //}
    }
}
