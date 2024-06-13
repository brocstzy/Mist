using Mist.Helper;
using Mist.Model;
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
    /// Interaction logic for BuyPageError.xaml
    /// </summary>
    public partial class BuyPageError : Page
    {
        public bool Balance;
        public Game Game;
        public BuyPageError(bool balance, Game game)
        {
            InitializeComponent();
            Balance = balance;
            Game = game;
            RefreshPage();
        }

        public void RefreshPage() 
        {
            if (Balance)
            {
                gameOwned_Label.Visibility = Visibility.Collapsed;
                goToLibrary_Button.Visibility = Visibility.Collapsed;
            }
            else
            {
                gameOwned_Label.Content = $"Игра {Game.Name} уже имеется на вашем аккаунте Mist.";
                balance_Label.Visibility= Visibility.Collapsed;
                topUp_Button.Visibility= Visibility.Collapsed;
            }
        }


        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void goToLibrary_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new LibraryPage());
        }

        private void topUp_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
