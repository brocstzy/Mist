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
    /// Interaction logic for BuyGamePage.xaml
    /// </summary>
    public partial class BuyGamePage : Page
    {
        public Game Game;
        public BuyGamePage(Game game)
        {
            InitializeComponent();
            
            Game = game;
        }
    }
}
