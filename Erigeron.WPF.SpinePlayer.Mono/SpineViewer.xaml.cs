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
        }


        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                if (!Locked)
                    Utils.Move_Window(new WindowInteropHelper(this).Handle);
                else
                    mSpineManager.TouchEvent();
            }
        }

        private void Window_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Locked = !Locked;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            App.ch.ForeSpine.WindowLeft = Left;
            App.ch.ForeSpine.WindowTop = Top;
            App.ch.ExportConfig(App.chPath);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mSpineManager.DieEvent();
            e.Cancel = true;
        }
    }
}
