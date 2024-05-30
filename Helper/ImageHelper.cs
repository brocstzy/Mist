using Mist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mist.Helper
{
    public static class ImageHelper
    {
        public static BitmapImage GetUserPFP(User user)
        {
            if (user.Pfp == null)
                return new BitmapImage(new Uri(("pack://application:,,,/Assets/default-pfp.png")));
            else
            {
                using (var stream = new MemoryStream(user.Pfp))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    return image;
                }
            }
        }
    }
}
