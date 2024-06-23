using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Mist.UserControls
{
    /// <summary>
    /// Interaction logic for PFPUserControl.xaml
    /// </summary>
    public partial class PFPUserControl : UserControl
    {
        public User User;
        public PFPUserControl(User user)
        {
            InitializeComponent();
            User = user;
            RefreshPFP();
        }

        public void RefreshPFP()
        {
            pfp_Image.Source = ImageHelper.GetImage(User.Pfp);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            var popup = App.CurrentPage.FindName("miniProfile_Popup") as Popup;
            var mp = new MiniProfileUserControl(User);
            popup.Child = mp;
            popup.PlacementTarget = this;
            mp.fadeInStoryboard.Begin();
            popup.IsOpen = true;
            this.Cursor = Cursors.Hand;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            var popup = App.CurrentPage.FindName("miniProfile_Popup") as Popup;
            if (popup != null)
            {
                var mp = popup.Child as MiniProfileUserControl;
                if (mp != null)
                {
                    mp.fadeOutStoryboard.Completed += (s, _) =>
                    {
                        popup.IsOpen = false;
                        popup.Child = null;
                    };
                    mp.fadeOutStoryboard.Begin();
                    this.Cursor = null;
                }
            }
        }

        private void UserControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ProfilePage(User));
        }
    }
}
