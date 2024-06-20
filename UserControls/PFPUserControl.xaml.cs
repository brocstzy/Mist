using Mist.Helper;
using Mist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
}
