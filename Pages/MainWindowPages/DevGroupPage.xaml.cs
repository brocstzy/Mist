using Mist.Helper;
using Mist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DevGroupPage.xaml
    /// </summary>
    public partial class DevGroupPage : Page
    {
        public Developer Developer;
        public List<Game> DeveloperGames;
        public int CurrentGameIndex = 0;
        public DevGroupPage(Developer developer)
        {
            InitializeComponent();
            Developer = developer;
            RefreshGroup();
            RefreshDevGames();
        }

        public void RefreshGroup()
        {
            background_Image.Source = ImageHelper.GetImage(Developer.BackgroundImage);
            pfp_Image.Source = ImageHelper.GetImage(Developer.Pfp);
            groupName_Label.Content = Developer.Name;
            groupBio_TextBlock.Text = Developer.Bio;
            followersCount_Label.Content = App.Context.DeveloperFollowers.Where(df => df.Developer == Developer).Count();
        }

        public void RefreshDevGames()
        {
            var games = App.Context.Games.Where(g => g.Developer == Developer).ToList();
            if (games.Any())
            {
                DeveloperGames = games;
                games_Frame.Navigate(new GamePreviewPage(DeveloperGames[0]));
            }
        }

        public void SetButtonsColors()
        {
            var game = (games_Frame.Content as GamePreviewPage).Game;
            if (game == Developer.Games.Last() && game == Developer.Games.First())
            {
                previousGame_Image.Source = ImageHelper.GetImageFromPath("/Assets/left-arrow-gray.png");
                nextGame_Image.Source = ImageHelper.GetImageFromPath("/Assets/right-arrow-gray.png");
                previousGame_Button.IsEnabled = false;
                nextGame_Button.IsEnabled = false;

            }
            else if (game == DeveloperGames.First())
            {
                previousGame_Image.Source = ImageHelper.GetImageFromPath("/Assets/left-arrow-gray.png");
                nextGame_Image.Source = ImageHelper.GetImageFromPath("/Assets/right-arrow.png");
                previousGame_Button.IsEnabled = false;
                nextGame_Button.IsEnabled = true;
            }
            else if (game == Developer.Games.Last())
            {
                previousGame_Image.Source = ImageHelper.GetImageFromPath("/Assets/left-arrow.png");
                nextGame_Image.Source = ImageHelper.GetImageFromPath("/Assets/right-arrow-gray.png");
                previousGame_Button.IsEnabled = true;
                nextGame_Button.IsEnabled = false;
            }
            else
            {
                previousGame_Image.Source = ImageHelper.GetImageFromPath("/Assets/left-arrow.png");
                nextGame_Image.Source = ImageHelper.GetImageFromPath("/Assets/right-arrow.png");
                previousGame_Button.IsEnabled = true;
                nextGame_Button.IsEnabled = true;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 0, m, 50);
            main_Grid.Margin = margin;
        }

        private void follow_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void games_Frame_Navigated(object sender, NavigationEventArgs e)
        {
            SetButtonsColors();
        }

        private void previousGame_Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentGameIndex -= 1;
            games_Frame.Navigate(new GamePreviewPage(DeveloperGames[CurrentGameIndex]));
        }

        private void nextGame_Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentGameIndex += 1;
            games_Frame.Navigate(new GamePreviewPage(DeveloperGames[CurrentGameIndex]));
        }
    }
}
