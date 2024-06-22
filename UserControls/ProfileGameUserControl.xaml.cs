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

namespace Mist.UserControls
{
    /// <summary>
    /// Interaction logic for ProfileGameUserControl.xaml
    /// </summary>
    public partial class ProfileGameUserControl : UserControl
    {
        public Game Game;
        public ProfileGameUserControl(Game game)
        {
            InitializeComponent();
            Game = game;
            RefreshGame();
        }

        public void RefreshGame()
        {
            game_Image.Source = ImageHelper.GetImage(Game.StoreSmallImage);
            gameName_Label.Content = Game.Name;
        }
    }
}
