using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Erigeron.WPF.SpinePlayer.Mono.Helper
{
    class Utils
    {
        #region 窗口拖动
        public static void Move_Window(IntPtr handle)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(handle, 0x0112, 0xF010 + 0x0002, 0);
        }
        #endregion

        #region 窗口拖动
        [DllImport("user32.dll")]//拖动无窗体的控件
        static extern bool ReleaseCapture();
        public static bool ReleaseCaptureImpl()
        {
            return ReleaseCapture();
        }
        [DllImport("user32.dll")]
        static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        #endregion

        #region 非占用读取图片
        public static BitmapImage? GetBitmapImage(string fileName)
        {
            if (System.IO.File.Exists(fileName) == false)
                return null;
            BitmapImage bitmapimage = new();
            bitmapimage.BeginInit();
            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapimage.UriSource = new Uri(fileName);
            bitmapimage.EndInit();
            bitmapimage.Freeze();
            return bitmapimage;
        }

        #endregion
        #region 检测全屏程序
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

        static bool IsDesktopWindow(IntPtr hWnd)
        {
            uint processId;
            GetWindowThreadProcessId(hWnd, out processId);
            if (processId == 0)
                return true;

            StringBuilder className = new StringBuilder(256);
            GetClassName(hWnd, className, className.Capacity);
            return className.ToString() == "Progman" || className.ToString() == "WorkerW";
        }

        public static bool IsForegroundFullScreen()
        {
            RECT rect = new();
            IntPtr hWnd = (IntPtr)GetForegroundWindow();
            if (IsDesktopWindow(hWnd) || hWnd == GetDesktopWindow())
                return false;
            GetWindowRect(new HandleRef(null, hWnd), ref rect);
            return SystemParameters.PrimaryScreenWidth <= (rect.right - rect.left) && SystemParameters.PrimaryScreenHeight <= (rect.bottom - rect.top);
        }
        #endregion
        
        public static string? GetRandomFile(string folderPath)
        {
            // 检查文件夹是否存在
            if (!Directory.Exists(folderPath))
            {
                return null;
            }

            // 获取文件夹及子文件夹下的所有文件
            var allFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

            // 如果没有找到文件，则返回 null
            if (allFiles.Length == 0)
            {
                return null;
            }

            // 随机选择一个文件
            Random random = new Random();
            int randomIndex = random.Next(allFiles.Length);
            return allFiles[randomIndex];
        }
    }
}
