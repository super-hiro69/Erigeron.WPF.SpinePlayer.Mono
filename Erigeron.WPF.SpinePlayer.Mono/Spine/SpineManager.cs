using Erigeron.WPF.SpinePlayer.Mono.Helper;
using Erigeron.WPF.SpinePlayer.Mono.Support;
using Microsoft.Xna.Framework;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Threading;

namespace Erigeron.WPF.SpinePlayer.Mono.Spine
{
    [SupportedOSPlatform("Windows")]
    class SpineManager : MonoGameViewModel
    {
        private SpineLoader? _spineLoader = null;
        internal SpineConfig? sc = null;
        private SpineViewer? _parent = null;

        public override void LoadContent()
        {
            _spineLoader ??= new(GraphicsDeviceInstance);
            if (sc!.SkeletonWidth <= 0)
                sc.SkeletonWidth = sc.WindowWidth <= 0 ? SystemParameters.PrimaryScreenWidth : sc.WindowWidth;
            if (sc!.SkeletonHeight <= 0)
                sc.SkeletonHeight = sc.WindowHeight <= 0 ? SystemParameters.PrimaryScreenHeight : sc.WindowHeight;
            _spineLoader.Initialize(sc!, _parent);
        }

        public override void OnMouseUp(MouseStateArgs mouseState)
        {
            _spineLoader?.OnMouseUp(mouseState);
        }

        public override void Update(GameTime gameTime)
        {
            _spineLoader?.UpdateInput();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDeviceInstance.Clear(Color.Transparent);
            _spineLoader?.Render((float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0));
        }

        public void UpdateSize(double Width, double Height)
        {

        }

        internal void SetParent(SpineViewer? sp)
        {
            _parent = sp;
        }

        internal void TouchEvent()
        {
            if (App.ch.AudioEnabled)
            {
                App.PlaySound(Utils.GetRandomFile(System.IO.Path.Combine(App.ch.AudioPath, "Touch")));
            }
            _spineLoader?.SetTouchAnimation();
        }

        internal void DieEvent()
        {
            if (App.ch.AudioEnabled)
            {
                App.PlaySound(Utils.GetRandomFile(System.IO.Path.Combine(App.ch.AudioPath, "Die")));
            }
            _spineLoader?.SetDieAnimation();
        }
    }
}
