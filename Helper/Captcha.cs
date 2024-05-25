using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mist.Helper
{
    public class Captcha
    {
        public string Value { get; private set; }
        public Bitmap Image { get; private set; }
        public static Captcha Create()
        {
            string chars = "qwertyuopasdfghjkzxcvbnm" +
                                  "QWERTYUOPASDFGHJKZXCVBNM" +
                                  "1234567890";
            string captcha = String.Empty;
            for (int i = 0; i < 5; i++)
            {
                captcha += chars[new Random().Next(0, chars.Length)];
            }
            Bitmap bmp = new Bitmap(100, 30);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(System.Drawing.Color.White);
            g.DrawString(captcha, new Font("Segoe UI", 16), new SolidBrush(System.Drawing.Color.Black), 2, 2);
            return new Captcha
            {
                Value = captcha,
                Image = bmp
            };
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        // If you get 'dllimport unknown'-, then add 'using System.Runtime.InteropServices;'
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

    }
}
