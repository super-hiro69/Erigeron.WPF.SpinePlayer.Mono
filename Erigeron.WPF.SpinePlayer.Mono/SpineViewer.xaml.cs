using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Erigeron.WPF.SpinePlayer.Mono
{
    public partial class SpineViewer : Window
    {
        public SpineViewer()
        {
            InitializeComponent();
            mSpineManager.UpdateSize(ActualWidth, ActualHeight);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mSpineManager.UpdateSize(ActualWidth, ActualHeight);
        }
    }
}
