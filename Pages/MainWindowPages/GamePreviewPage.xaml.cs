using Mist.Helper;
using Mist.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for GamePreviewPage.xaml
    /// </summary>
    public partial class GamePreviewPage : Page
    {
        public Game Game;
        public GamePreviewPage(Game game)
        {
            InitializeComponent();
            Game = game;
            RefreshGame();
        }

        private void RefreshGame()
        {
            storeFront_Image.Source = ImageHelper.GetImage(Game.FrontImage);
            gamePrice_TextBlock.Text = $"{Game.UsdPrice} руб.";
            releaseDate_Label.Content = $"Выпущена {Game.ReleaseDate.Day} {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Game.ReleaseDate.Month)}, {Game.ReleaseDate.Year}";
            gameBio_TextBlock.Text = Game.Bio;
            List<byte[]> gameScreenshots = App.Context.GameImages.Where(x => x.Game == Game).Select(x => x.Image).ToList();
            if (gameScreenshots.Count > 3 )
            {
                for (int i = 0; i < 4; i++)
                {
                    Image imageControl = (Image)this.FindName($"scr{i}_Image");
                    imageControl.Source = ImageHelper.GetImage(gameScreenshots[i]);
                }
            }
        }
    }
}
