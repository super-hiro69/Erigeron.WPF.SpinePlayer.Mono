using System.ComponentModel;
using System.Runtime.CompilerServices;

///This file is originated from https://github.com/damian-666/MonoGame.WpfCore
///Thanks for his/her support!

namespace Erigeron.WPF.SpinePlayer.Mono.Support;

public class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
