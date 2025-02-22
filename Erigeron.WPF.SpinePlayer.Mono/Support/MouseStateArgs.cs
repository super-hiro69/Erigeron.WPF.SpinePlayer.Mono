using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

///This file is originated from https://github.com/damian-666/MonoGame.WpfCore
///Thanks for his/her support!

namespace Erigeron.WPF.SpinePlayer.Mono.Support;

[SupportedOSPlatform("Windows")]
public class MouseStateArgs
{
    private readonly IInputElement _element;
    private readonly MouseEventArgs _args;

    public MouseStateArgs(IInputElement element, MouseEventArgs args)
    {
        _element = element;
        _args = args;
    }

    public Vector2 Position => _args.GetPosition(_element).ToVector2();
    public ButtonState LeftButton => ConvertButtonState(_args.LeftButton);
    public ButtonState RightButton => ConvertButtonState(_args.RightButton);
    public ButtonState MiddleButton => ConvertButtonState(_args.MiddleButton);

    private static ButtonState ConvertButtonState(MouseButtonState state) => state == MouseButtonState.Pressed ? ButtonState.Pressed : ButtonState.Released;
}