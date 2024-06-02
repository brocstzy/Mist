using Mist.Helper;
using Mist.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public Game Game;
        public List<byte[]> GameImages;
        public List<byte[]> GameVideos;
        public GamePage(Game game)
        {
            InitializeComponent();
            Game = game;
            GameImages = App.Context.GameImages.Where(x => x.GameId == Game.Id).Select(x => x.Image).ToList();
            GameVideos = App.Context.GameVideos.Where(x => x.GameId == Game.Id).Select(x => x.Video).ToList();
            RefreshGame();
        }

        public void RefreshGame()
        {
            gameName_Label.Content = Game.Name;
            front_Image.Source = ImageHelper.GetImage(Game.FrontImage);
            bio_TextBlock.Text = Game.Bio;
            releaseDate_Label.Content = Game.ReleaseDate;
            developer_Label.Content = App.Context.Developers.Where(x => x.Id == Game.DeveloperId).First().Name;

            string videoFilePath = SaveVideoToFile(GameVideos.Last(), "video.mp4");
            PlayVideo(videoFilePath);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }
        public string SaveVideoToFile(byte[] videoData, string fileName)
        {
            string filePath = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllBytes(filePath, videoData);

            return filePath;
        }
        private void PlayVideo(string videoFilePath)
        {
            video_MediaElement.Source = new Uri(videoFilePath);
            video_MediaElement.LoadedBehavior = MediaState.Manual;
            video_MediaElement.UnloadedBehavior = MediaState.Manual;
            video_MediaElement.MediaEnded += (sender, e) => video_MediaElement.Stop();
            video_MediaElement.Volume = 1;
            video_MediaElement.Play();
        }

        private void pause_Button_Click(object sender, RoutedEventArgs e)
        {
            video_MediaElement.Pause();
        }

        private void play_Button_Click(object sender, RoutedEventArgs e)
        {
            video_MediaElement.Play();
        }
    }
}
