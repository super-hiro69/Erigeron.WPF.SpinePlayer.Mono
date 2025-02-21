using Erigeron.WPF.SpinePlayer.Mono.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace Erigeron.WPF.SpinePlayer.Mono
{
    public partial class SpineViewer : System.Windows.Window
    {
        private bool Locked = true;
        public SpineViewer()
        {
            InitializeComponent();
            mSpineManager.UpdateSize(ActualWidth, ActualHeight);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mSpineManager.UpdateSize(ActualWidth, ActualHeight);
        }


        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Locked)
                Utils.Move_Window(new WindowInteropHelper(this).Handle);
            else
                mSpineManager.TouchEvent();
        }

        private void Window_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Locked = !Locked;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            App.ch.WindowLeft = Margin.Left;
            App.ch.WindowTop = Margin.Top;
            App.ch.ExportConfig(App.chPath);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        { 
            mSpineManager.DieEvent();
            e.Cancel = true;
        }
    }
}
