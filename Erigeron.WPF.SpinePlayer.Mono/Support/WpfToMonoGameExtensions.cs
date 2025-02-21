using Microsoft.Xna.Framework;

///This file is originated from https://github.com/damian-666/MonoGame.WpfCore
///Thanks for his/her support!

namespace Erigeron.WPF.SpinePlayer.Mono.Support;

public static class WpfToMonoGameExtensions
{
    public static Vector2 ToVector2(this System.Windows.Point point) => new Vector2((float)point.X, (float)point.Y);
}