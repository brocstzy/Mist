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
using System.Windows.Shapes;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Management;

namespace Mist.Windows
{
    /// <summary>
    /// Interaction logic for PcSpecsWindow.xaml
    /// </summary>
    public partial class PcSpecsWindow : Window
    {
        public PcSpecsWindow()
        {
            InitializeComponent();
            RefreshSpecs();
        }

        private void RefreshSpecs()
        {
            cpu_TextBlock.Text = GetProcessorName();
            ram_TextBlock.Text = FormatBytes(GetTotalRAM());
            gpu_TextBlock.Text = GetGraphicsCardName();
            var diskSpace = GetDiskSpace();
            disk_TextBlock.Text = FormatBytes(diskSpace.Item1) + $" ({FormatBytes(diskSpace.Item2)} свободно)";
            win_TextBlock.Text = Environment.OSVersion.Version.ToString();
            winLanguage_TextBlock.Text = System.Globalization.CultureInfo.CurrentCulture.DisplayName;
            monitor_TextBlock.Text = GetPrimaryMonitorResolution() + $"@{GetPrimaryMonitorRefreshRate()}";
        }

        private void resize_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                this.DragMove();
            }
        }

        private static string GetProcessorName()
        {
            using (var managementObject = new ManagementObject("Win32_Processor.DeviceID='CPU0'"))
            {
                return managementObject["Name"].ToString();
            }
        }

        private static ulong GetTotalRAM()
        {
            using (var managementObject = new ManagementObject("Win32_OperatingSystem=@"))
            {
                return (ulong)managementObject["TotalVisibleMemorySize"] * 1024;
            }
        }

        private static string GetGraphicsCardName()
        {
            using (var managementObject = new ManagementObject("Win32_VideoController.DeviceID='VideoController1'"))
            {
                return managementObject["Name"].ToString();
            }
        }

        private static (ulong, ulong) GetDiskSpace()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.DriveType == DriveType.Fixed)
                {
                    return ((ulong)drive.TotalSize, (ulong)drive.TotalFreeSpace);
                }
            }
            return (0, 0);
        }

        private static string FormatBytes(ulong bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int suffixIndex = 0;
            double doubleBytes = bytes;

            while (doubleBytes >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                doubleBytes /= 1024;
                suffixIndex++;
            }

            return $"{doubleBytes:0.##} {suffixes[suffixIndex]}";
        }

        private static string GetPrimaryMonitorResolution()
        {
            var primaryScreen = System.Windows.SystemParameters.PrimaryScreenWidth + "x" + System.Windows.SystemParameters.PrimaryScreenHeight;
            return primaryScreen;
        }

        private static string GetPrimaryMonitorRefreshRate()
        {
            DEVMODE devMode = new DEVMODE();
            EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode);
            return devMode.dmDisplayFrequency + " Hz";
        }

        private const int ENUM_CURRENT_SETTINGS = -1;

        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [StructLayout(LayoutKind.Sequential)]
        private struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;
            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmNup;
            public int dmDisplayFrequency;
        }
    }
}
