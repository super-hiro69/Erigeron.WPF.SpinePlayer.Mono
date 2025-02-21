using System.Configuration;
using System.Data;
using System.Windows;

namespace Erigeron.WPF.SpinePlayer.Mono;

public partial class App : Application
{
    public static SpineViewer sv = new();
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        sv.Show();
    }
}

