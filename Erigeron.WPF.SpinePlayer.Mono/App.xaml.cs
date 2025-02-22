using Erigeron.WPF.SpinePlayer.Mono.Helper;
using Erigeron.WPF.SpinePlayer.Mono.Support;
using System.IO;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Threading;

namespace Erigeron.WPF.SpinePlayer.Mono;

[SupportedOSPlatform("Windows")]
public partial class App : Application
{
    public static SpineViewer? sv = null;
    public static SpineViewer? dsv = null;
    internal static Config ch = new();
    internal static string chPath = ".\\data\\Default";
    internal static DispatcherTimer? _timer = null;
    private static int timeElapsed = 0;
    private static int timeDestination = 0;
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        if (e.Args.Length > 0)
        {
            chPath = e.Args[0];
            ch.ImportConfig(chPath);
        }
        else if (!System.IO.File.Exists(".\\data\\Default"))
        {
            System.IO.Directory.CreateDirectory("data");
            ch.ExportConfig(".\\data\\Default");
        }
        else
            ch.ImportConfig(".\\data\\Default");
        bool boot = false;
        if (System.IO.File.Exists(ch.ForeSpine.AtlasPath) && System.IO.File.Exists(ch.ForeSpine.SkelPath))
        {
            boot = true;
            sv = CreateSpineViewer(ch.ForeSpine);
            sv.Title = ch.ForeSpine.CharName ?? "SpineViewer";
            sv.mSpineManager.sc = ch.ForeSpine;
            sv.Show();
            if (ch.HideAppHost)
            {
                Helper.Window.SetAltTabInvisible(new WindowInteropHelper(sv).Handle);
            }
            sv.Topmost = true;

        }
        if (ch.DesktopInsert && !ch.StaticDesktop && File.Exists(ch.DesktopSpine.AtlasPath) && File.Exists(ch.DesktopSpine.SkelPath))
        {
            boot = true;
            dsv = CreateSpineViewer(ch.DesktopSpine);
            dsv.Title = ch.DesktopSpine.CharName ?? "SpineViewer";
            dsv.mSpineManager.sc = ch.DesktopSpine;
            dsv.Loaded += (e, args) =>
            {
                var handle = new WindowInteropHelper(dsv).Handle;
                Desktop.SendMsgToProgman();
                Desktop.SetParent(handle, Desktop.programHandle);
                Helper.Window.SetAltTabInvisible(handle);
                Helper.Window.SetBottomMostWindow(handle);
            };
            dsv.Activated += (e, args) =>
            {
                var handle = new WindowInteropHelper(dsv).Handle;
                Helper.Window.SetBottomMostWindow(handle);
            };
            dsv.StateChanged += (e, args) =>
            {
                var handle = new WindowInteropHelper(dsv).Handle;
                Helper.Window.SetBottomMostWindow(handle);
            };
            dsv.Show();

        }
        if (ch.DesktopInsert && ch.StaticDesktop && System.IO.File.Exists(ch.PicPathD))
        {
            boot = true;
            ImageWin iw = new();
            iw.DesktopImage.Source = Utils.GetBitmapImage(ch.PicPathD);
            iw.Width = ch.DesktopSpine.WindowWidth < 0 ? SystemParameters.PrimaryScreenWidth : ch.DesktopSpine.WindowWidth;
            iw.Height = ch.DesktopSpine.WindowHeight < 0 ? SystemParameters.PrimaryScreenHeight : ch.DesktopSpine.WindowHeight;
            Canvas.SetLeft(iw, ch.DesktopSpine.MarginLeft);
            Canvas.SetTop(iw, ch.DesktopSpine.MarginTop);
            iw.Loaded += (e, args) =>
            {
                var handle = new WindowInteropHelper(iw).Handle;
                Desktop.SendMsgToProgman();
                Desktop.SetParent(handle, Desktop.programHandle);
                Helper.Window.SetAltTabInvisible(handle);
                Helper.Window.SetBottomMostWindow(handle);
            };
            iw.Activated += (e, args) =>
            {
                var handle = new WindowInteropHelper(iw).Handle;
                Helper.Window.SetBottomMostWindow(handle);
            };
            iw.StateChanged += (e, args) =>
            {
                var handle = new WindowInteropHelper(iw).Handle;
                Helper.Window.SetBottomMostWindow(handle);
            };
            iw.Show();
        }
        if (!boot)
            Environment.Exit(0);
        if (ch.AudioEnabled)
        {
            timeDestination = new Random().Next(Math.Min(ch.AudioIntervalMin, ch.AudioIntervalMax), Math.Max(ch.AudioIntervalMin, ch.AudioIntervalMax));
            PlaySound(Utils.GetRandomFile(Path.Combine(ch.AudioPath, "Start")));
        }
        StartTimer();
    }

    private SpineViewer CreateSpineViewer(SpineConfig sc)
    {
        var s = new SpineViewer();
        Canvas.SetLeft(s, sc.WindowLeft);
        Canvas.SetTop(s, sc.WindowTop);
        if (sc.WindowWidth >= 0)
            s.Width = sc.WindowWidth;
        else
            s.Width = SystemParameters.PrimaryScreenWidth;
        if (sc.WindowHeight >= 0)
            s.Height = sc.WindowHeight;
        else
            s.Height = SystemParameters.PrimaryScreenHeight;
        s.MGControl.Width = sc.SkeletonWidth <= 0 ? s.Width : sc.SkeletonWidth;
        s.MGControl.Height = sc.SkeletonHeight <= 0 ? s.Height : sc.SkeletonHeight;
        s.MGControl.Margin = new(sc.MarginLeft, sc.MarginTop, 0, 0);
        return s;
    }

    private void StartTimer()
    {
        _timer ??= new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += (e, args) =>
        {
            timeElapsed++;
            if (timeElapsed >= timeDestination)
                PlaySound(Utils.GetRandomFile(Path.Combine(ch.AudioPath, "Idle")));
            if (sv == null)
                return;
            if (Utils.IsForegroundFullScreen() && sv.Visibility != Visibility.Collapsed)
            {
                var s = Animation.AddDoubleAnimaton(0, 250, sv, "Opacity", null);
                s.Completed += (e, args) =>
                {
                    sv.Opacity = 0;
                    sv.Visibility = Visibility.Collapsed;
                };
                s.Begin();
            }
            if (!Utils.IsForegroundFullScreen() && sv.Visibility != Visibility.Visible)
            {
                sv.Visibility = Visibility.Visible;
                var s = Animation.AddDoubleAnimaton(1, 250, sv, "Opacity", null);
                s.Completed += (e, args) =>
                {
                    sv.Opacity = 1;
                };
                s.Begin();
            }
        };
        _timer.Start();
    }

    internal static void PlaySound(string? path)
    {
        if (path == null || !System.IO.File.Exists(path))
        {
            return;
        }
        _timer?.Stop();
        timeDestination = new Random().Next(Math.Min(ch.AudioIntervalMin, ch.AudioIntervalMax), Math.Max(ch.AudioIntervalMin, ch.AudioIntervalMax));
        timeElapsed = 0;
        new Thread((ThreadStart)delegate
        {
            try
            {
                System.Media.SoundPlayer sndPlayer = new(path);
                sndPlayer.PlaySync();
            }
            catch { }
            Current.Dispatcher.Invoke(() =>
            {
                _timer?.Start();
            });
        }).Start();

    }
}

