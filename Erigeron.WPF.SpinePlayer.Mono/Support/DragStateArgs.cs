using System.Windows;
using Microsoft.Xna.Framework;

///This file is originated from https://github.com/damian-666/MonoGame.WpfCore
///Thanks for his/her support!

namespace Erigeron.WPF.SpinePlayer.Mono.Support;

public class DragStateArgs
{
    private readonly IInputElement _element;
    private readonly DragEventArgs _args;

    public DragStateArgs(IInputElement element, DragEventArgs args)
    {
        _element = element;
        _args = args;
    }

    public Vector2 Position => _args.GetPosition(_element).ToVector2();
    public T? GetData<T>() where T : class => _args?.Data?.GetData(typeof(T)) as T;
}