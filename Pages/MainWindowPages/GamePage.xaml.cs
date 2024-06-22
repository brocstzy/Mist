using Mist.Helper;
using Mist.Model;
using System.Globalization;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Mist.Pages.MainWindowPages
{
    public partial class GamePage : Page
    {
        public Game Game;

        public List<byte[]> GameImages;
        public List<byte[]> GameVideos;

        public GameSysReq GameMinSysReqs;
        public GameSysReq GameMaxSysReqs;

        private System.Timers.Timer timer;
        private bool isDragging = false;

        public List<string> videosPath = new List<string>();
        public List<string> videosPathMini = new List<string>();
        public GamePage(Game game)
        {
            InitializeComponent();
            Game = game;
            GameImages = App.Context.GameImages.Where(x => x.GameId == Game.Id).Select(x => x.Image).ToList();
            GameVideos = App.Context.GameVideos.Where(x => x.GameId == Game.Id).Select(x => x.Video).ToList();
            GameMinSysReqs = App.Context.GameSysReqs.Where(x => x.GameId == Game.Id && x.Type == "Минимальные").FirstOrDefault();
            GameMaxSysReqs = App.Context.GameSysReqs.Where(x => x.GameId == Game.Id && x.Type == "Рекомендованные").FirstOrDefault();

            RefreshGame();

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;

            volume_Slider.Value = video_MediaElement.Volume * 100;

            RefreshMedia();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (!isDragging)
            {
                Dispatcher.Invoke(() =>
                {
                    video_Slider.Value = video_MediaElement.Position.TotalSeconds;
                });
            }
        }

        public void RefreshGame()
        {
            gameName_Label.Text = Game.Name;
            front_Image.Source = ImageHelper.GetImage(Game.FrontImage);
            bio_TextBlock.Text = Game.Bio;
            description_TextBlock.Text = Game.Description;
            releaseDate_Label.Content = Game.ReleaseDate.Day + $" {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Game.ReleaseDate.Month).Substring(0, 3)}, " + Game.ReleaseDate.Year;
            developer_Label.Content = App.Context.Developers.Where(x => x.Id == Game.DeveloperId).First().Name;

            if (GameMinSysReqs != null)
            {
                osMin_TextBlock.Text = GameMinSysReqs.Os;
                cpuMin_TextBlock.Text = GameMinSysReqs.Cpu;
                memMin_TextBlock.Text = GameMinSysReqs.Memory;
                gpuMin_TextBlock.Text = GameMinSysReqs.Gpu;
                directxMin_TextBlock.Text = GameMinSysReqs.Directx;
                storageMin_TextBlock.Text = GameMinSysReqs.Storage;
            }

            if (GameMaxSysReqs != null)
            {
                osMax_TextBlock.Text = GameMaxSysReqs.Os;
                cpuMax_TextBlock.Text = GameMaxSysReqs.Cpu;
                memMax_TextBlock.Text = GameMaxSysReqs.Memory;
                gpuMax_TextBlock.Text = GameMaxSysReqs.Gpu;
                directxMax_TextBlock.Text = GameMaxSysReqs.Directx;
                storageMax_TextBlock.Text = GameMaxSysReqs.Storage;
            }

            buyGame_Label.Content = $"Купить {Game.Name}";
            buyGame_Button.Content = $"{Game.UsdPrice} руб.";

            string videoFilePath = SaveVideoToFile(GameVideos.First(), "video.mp4");
            PlayVideo(videoFilePath);
            video_MediaElement.Play();
            Task.Delay(3).Wait();
            video_MediaElement.Pause();
        }

        public void RefreshMedia()
        {
            foreach (var video in GameVideos)
            {
                var theGrid = new Grid();
                var theVideo = new MediaElement();
                var playImage = new Image();
                playImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/preview-play.png"));
                playImage.Height = 30;
                playImage.Width = 30;
                playImage.VerticalAlignment = VerticalAlignment.Center;
                playImage.HorizontalAlignment = HorizontalAlignment.Center;
                theGrid.Children.Add(theVideo);
                theGrid.Children.Add(playImage);
                theGrid.MouseEnter += Screenshot_MouseEnter;
                theGrid.MouseLeave += Screenshot_MouseLeave;
                theVideo.PreviewMouseLeftButtonDown += VideoSmall_PreviewMouseLeftButtonDown;
                playImage.PreviewMouseLeftButtonDown += (sender, e) => PlayImage_PreviewMouseLeftButtonDown(sender, e, theVideo);

                media_StackPanel.Children.Add(theGrid);

                videosPathMini.Add(SaveVideoToFile(video, $"{Guid.NewGuid()}.mp4"));
                theVideo.Source = new Uri(videosPathMini.Last());
                theVideo.LoadedBehavior = MediaState.Manual;
                theVideo.UnloadedBehavior = MediaState.Manual;
                theVideo.Play();
                Task.Delay(3).Wait();
                theVideo.Pause();
                theVideo.Margin = new Thickness(0);

                if (video == GameVideos.First())
                    theGrid.Margin = new Thickness(0, 10, 0, 10);

            }
            foreach (var image in GameImages)
            {
                var theImage = new Image();
                theImage.Source = ImageHelper.GetImage(image);
                RenderOptions.SetBitmapScalingMode(theImage, BitmapScalingMode.HighQuality);

                theImage.PreviewMouseLeftButtonDown += Screenshot_PreviewMouseLeftButtonDown;
                theImage.MouseEnter += Screenshot_MouseEnter;
                theImage.MouseLeave += Screenshot_MouseLeave;

                media_StackPanel.Children.Add(theImage);
            }
        }

        private void TheGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PlayImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e, MediaElement me)
        {
            VideoSmall_PreviewMouseLeftButtonDown(me, e);
        }

        private void VideoSmall_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            video_MediaElement.Source = ((MediaElement)sender).Source;
            screenshot_Image.Visibility = Visibility.Collapsed;
            video_MediaElement.Visibility = Visibility.Visible;
            video_Slider.Visibility = Visibility.Visible;
            videoControls_Grid.Visibility = Visibility.Visible;
            play_Button_Click(null, null);
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
        }

        private void pause_Button_Click(object sender, RoutedEventArgs e)
        {
            video_MediaElement.Pause();
            timer.Stop();
            play_Button.Visibility = Visibility.Visible;
            pause_Button.Visibility = Visibility.Collapsed;
        }

        private void play_Button_Click(object sender, RoutedEventArgs e)
        {
            video_MediaElement.Play();
            timer.Start();
            pause_Button.Visibility = Visibility.Visible;
            play_Button.Visibility = Visibility.Collapsed;
        }

        private void video_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isDragging)
            {
                video_MediaElement.Position = TimeSpan.FromSeconds(video_Slider.Value);
            }
        }

        private void video_Slider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            timer.Stop();
            video_MediaElement.Pause();
        }

        private void video_Slider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            video_MediaElement.Position = TimeSpan.FromSeconds(video_Slider.Value);
            video_MediaElement.Play();
            timer.Start();
        }

        private void video_MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            video_Slider.Maximum = video_MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void video_MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            video_MediaElement.Stop();
            timer.Stop();
            video_Slider.Value = 0;
            play_Button.Visibility = Visibility.Visible;
            pause_Button.Visibility = Visibility.Collapsed;
        }

        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            video_MediaElement.Stop();
            video_MediaElement.Close();
            timer.Stop();
            timer.Dispose();

            await Task.Run(() =>
            {
                Parallel.ForEach(videosPathMini, (file) =>
                {
                    File.Delete(file);
                });

                File.Delete(Path.Combine(Path.GetTempPath(), "video.mp4"));

                Dispatcher.Invoke(() =>
                {
                    foreach (var child in media_StackPanel.Children.OfType<Grid>())
                    {
                        child.Children.OfType<MediaElement>().First().Close();
                    }
                });
            });


        }

        private void volume_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            volume_Slider.Visibility = Visibility.Visible;
        }

        private void volume_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            video_MediaElement.Volume = volume_Slider.Value / 100;
        }

        private void volume_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!volume_Slider.IsMouseOver)
                volume_Slider.Visibility = Visibility.Collapsed;
        }

        private void volume_Slider_MouseLeave(object sender, MouseEventArgs e)
        {
            volume_Slider.Visibility = Visibility.Collapsed;
        }
        private void Screenshot_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = null;
        }

        private void Screenshot_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void Screenshot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            video_MediaElement.Visibility = Visibility.Collapsed;
            video_MediaElement.Close();
            videoControls_Grid.Visibility = Visibility.Collapsed;
            video_Slider.Visibility = Visibility.Collapsed;
            screenshot_Image.Visibility = Visibility.Visible;
            screenshot_Image.Source = ((Image)sender).Source;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void media_ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                media_ScrollViewer.LineLeft();
                media_ScrollViewer.LineLeft();
            }
            else
            {
                media_ScrollViewer.LineRight();
                media_ScrollViewer.LineRight();
            }

            e.Handled = true;
        }

        private void buyGame_Button_Click(object sender, RoutedEventArgs e)
        {
            using (MistContext mc = new MistContext()) 
            {
                var ownedGame = mc.UserGames.Where(x => x.GameId == Game.Id && x.UserId == App.CurrentUser.Id).FirstOrDefault();
                if (ownedGame != null)
                {
                    PageManager.MainFrame.Navigate(new BuyPageError(false, Game));
                    return;
                }
                if (Game.UsdPrice > App.CurrentUser.Balance)
                {
                    PageManager.MainFrame.Navigate(new BuyPageError(true, Game));
                    return;
                }
            }
            PageManager.MainFrame.Navigate(new BuyGamePage(Game));
        }

        private void developer_Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new DevGroupPage(Game.Developer));
        }
    }
}
