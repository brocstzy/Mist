using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mist.Helper
{
    public class WindowManager
    {
        public static void Close<TWindow>() where TWindow : Window
        {
            GetWindow<TWindow>().Close();
        }
        public static Window GetWindow<TWindow>() where TWindow : Window
        {
            return Application.Current.Windows.OfType<TWindow>().FirstOrDefault();
        }
    }
}
