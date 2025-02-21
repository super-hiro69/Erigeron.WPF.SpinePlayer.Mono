using Erigeron.WPF.SpinePlayer.Mono.Helper;
using Erigeron.WPF.SpinePlayer.Mono.Support;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Threading;

namespace Erigeron.WPF.SpinePlayer.Mono;

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
        var dpiScale = Helper.Window.GetScreenScalingFactor();
        if (System.IO.File.Exists(ch.AtlasPath) && System.IO.File.Exists(ch.SkelPath))
        {
            boot = true;
            sv = CreateSpineViewer(ch.AtlasPath, ch.SkelPath, ch.WindowLeft, ch.WindowTop, ch.WindowWidth, ch.WindowHeight);
            sv.Title = ch.CharName ?? "SpineViewer";
            SetMGControl(sv.MGControl, ch.MarginLeft, ch.MarginTop, ch.SkeletonWidth, ch.SkeletonHeight, sv.Width, sv.Height);
            sv.mSpineManager._xy = [((float)(sv.MGControl.Width / dpiScale)), ((float)(sv.MGControl.Height / dpiScale))];
            sv.mSpineManager.StartAnimationPool = ch.StartAnimationPool;
            sv.mSpineManager.IdleAnimationPool = ch.IdleAnimationPool;
            sv.mSpineManager.TouchAnimationPool = ch.TouchAnimationPool;
            sv.mSpineManager.DieAnimationPool = ch.DieAnimationPool;
            sv.Show();
            if (ch.HideAppHost)
            {
                Helper.Window.SetAltTabInvisible(new WindowInteropHelper(sv).Handle);
            }
            sv.Topmost = true;

        }
        if (ch.DesktopInsert && !ch.StaticDesktop && System.IO.File.Exists(ch.AtlasPathD) && System.IO.File.Exists(ch.SkelPathD))
        {
            boot = true;
            dsv = CreateSpineViewer(ch.AtlasPathD, ch.SkelPathD, ch.WindowLeftD, ch.WindowTopD, ch.WindowWidthD, ch.WindowHeightD);
            SetMGControl(dsv.MGControl, ch.MarginLeftD, ch.MarginTopD, ch.SkeletonWidthD, ch.SkeletonHeightD, dsv.Width, dsv.Height);
            dsv.mSpineManager._xy = [((float)(dsv.MGControl.Width / dpiScale)), ((float)(dsv.MGControl.Height / dpiScale))];
            dsv.mSpineManager.StartAnimationPool = ch.StartAnimationPoolD;
            dsv.mSpineManager.IdleAnimationPool = ch.IdleAnimationPoolD;
            dsv.mSpineManager.TouchAnimationPool = ch.TouchAnimationPoolD;
            dsv.mSpineManager.DieAnimationPool = ch.DieAnimationPoolD;
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
            iw.Width = ch.WindowWidthD < 0 ? SystemParameters.PrimaryScreenWidth : ch.WindowWidthD;
            iw.Height = ch.WindowHeightD < 0 ? SystemParameters.PrimaryScreenHeight : ch.WindowHeightD;
            Canvas.SetLeft(iw, ch.MarginLeftD);
            Canvas.SetTop(iw, ch.MarginTopD);
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

    private void SetMGControl(MonoGameContentControl mgc, double left, double top, double width, double height, double pWidth, double pHeight)
    {
        if (width <= 0)
            width = pWidth;
        if (height <= 0)
            height = pHeight;
        if (left <= 0)
            left = pWidth / 2 - width / 2;
        if (top <= 0)
            top = pHeight / 2 - height / 2;
        mgc.Margin = new(left, top, 0, 0);
        mgc.Width = width;
        mgc.Height = height;
    }

    private SpineViewer CreateSpineViewer(string atlasPath, string skelPath, double winLeft, double winTop, double winWidth, double winHeight)
    {
        var s = new SpineViewer();
        s.mSpineManager._atlasPath = atlasPath;
        s.mSpineManager._skelPath = skelPath;
        Canvas.SetLeft(s, winLeft);
        Canvas.SetTop(s, winTop);
        if (winWidth >= 0)
            s.Width = winWidth;
        else
            s.Width = SystemParameters.PrimaryScreenWidth;
        if (winHeight >= 0)
            s.Height = winHeight;
        else
            s.Height = SystemParameters.PrimaryScreenHeight;
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

