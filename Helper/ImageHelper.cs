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
        public static BitmapImage GetImage(byte[] image)
        {
            if (image == null)
                return new BitmapImage(new Uri(("pack://application:,,,/Assets/default-pfp.png")));
            else
            {
                using (var stream = new MemoryStream(image))
                {
                    var outputImage = new BitmapImage();
                    outputImage.BeginInit();
                    outputImage.StreamSource = stream;
                    outputImage.CacheOption = BitmapCacheOption.OnLoad;
                    outputImage.EndInit();
                    return outputImage;
                }
            }
        }
    }
}
