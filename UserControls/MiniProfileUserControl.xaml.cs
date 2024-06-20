using Mist.Helper;
using Mist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mist.UserControls
{
    /// <summary>
    /// Interaction logic for MiniProfileUserControl.xaml
    /// </summary>
    public partial class MiniProfileUserControl : UserControl
    {
        public User User;

        public Storyboard fadeInStoryboard;
        public Storyboard fadeOutStoryboard;

        public MiniProfileUserControl(User user)
        {
            InitializeComponent();
            User = user;
            InitializeStoryboards();
            RefreshMiniProfile();
        }

        public void RefreshMiniProfile()
        {
            pfp_Image.Source = ImageHelper.GetImage(User.Pfp);
            nickname_Label.Text = User.Nickname;
            using (MistContext mc = new MistContext())
            {
                var gameCount = mc.UserGames.Where(x => x.User == User).Count();
                mistLevel_Label.Content = gameCount;
                mistLevel_Label.Foreground = Brushes.White;
            }
            pfpBg_Image.Source = ImageHelper.GetImage(User.Pfp);

        }

        public void InitializeStoryboards()
        {
            fadeInStoryboard = new Storyboard();
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            Storyboard.SetTarget(fadeInAnimation, this);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(UIElement.OpacityProperty));
            fadeInStoryboard.Children.Add(fadeInAnimation);

            fadeOutStoryboard = new Storyboard();
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            Storyboard.SetTarget(fadeOutAnimation, this);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(UIElement.OpacityProperty));
            fadeOutStoryboard.Children.Add(fadeOutAnimation);
        }
    }
}
