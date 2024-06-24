using Mist.Helper;
using Mist.Model;
using Mist.Windows;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for BuyGamePage.xaml
    /// </summary>
    public partial class BuyGamePage : Page
    {
        public Game Game;
        public BuyGamePage(Game game)
        {
            InitializeComponent();
            Game = game;
            RefreshGame();
        }

        public void RefreshGame()
        {
            game_Image.Source = ImageHelper.GetImage(Game.FrontImage);
            gameName_Label.Content = Game.Name;
            bio_TextBlock.Text = Game.Bio;
            gamePrice_Label.Content = Game.UsdPrice + " руб.";
            userBalance_Label.Content = App.CurrentUser.Balance + " руб.";
            remainingBalance_Label.Content = App.CurrentUser.Balance - Game.UsdPrice + " руб.";
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void buy_Button_Click(object sender, RoutedEventArgs e)
        {
            using (MistContext mc = new MistContext())
            {
                mc.UserGames.Add(new UserGame(App.CurrentUser.Id, Game.Id, DateTime.Now));
                mc.Users.Where(x => x.Id == App.CurrentUser.Id).First().Balance -= Game.UsdPrice;
                mc.SaveChanges();
            }
            App.Context = new MistContext();
            ((MainWindow)WindowManager.GetWindow<MainWindow>()).balance_Label.Content = App.Context.Users.Where(u => u.Id == App.CurrentUser.Id).First().Balance + " руб.";
            PageManager.MainFrame.Navigate(new BuyGameSuccessPage(Game));
        }
    }
}
