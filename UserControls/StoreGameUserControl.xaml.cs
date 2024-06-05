using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace Mist.UserControls
{
    /// <summary>
    /// Interaction logic for StoreGameUserControl.xaml
    /// </summary>
    public partial class StoreGameUserControl : UserControl
    {
        public Game Game;
        public StoreGameUserControl(Game game)
        {
            InitializeComponent();
            Game = game;
            RefreshGame();

        }

        public void RefreshGame()
        {
            using (MemoryStream ms = new MemoryStream(Game.StoreSmallImage))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();

                game_Image.Source = image;
            }
            gameName_Label.Content = Game.Name;
            releaseDate_Label.Content = Game.ReleaseDate.Day + $" {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Game.ReleaseDate.Month).Substring(0, 3)} " + Game.ReleaseDate.Year;
            price_Label.Content = Game.UsdPrice + " руб.";
        }
            
        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new GamePage(Game));
        }
    }
}
