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
    /// Interaction logic for VerticalTileUserControl.xaml
    /// </summary>
    public partial class VerticalTileUserControl : UserControl
    {
        public Game Game;
        public VerticalTileUserControl(Game game)
        {
            InitializeComponent();
            Game = game;
            RefreshImage();
        }

        public void RefreshImage()
        {
            vertical_Image.Source = ImageHelper.GetImage(Game.VerticalLibraryImage);
        }
    }
}
