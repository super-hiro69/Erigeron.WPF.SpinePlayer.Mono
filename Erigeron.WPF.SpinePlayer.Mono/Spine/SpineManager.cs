using Erigeron.WPF.SpinePlayer.Mono.Helper;
using Erigeron.WPF.SpinePlayer.Mono.Support;
using Microsoft.Xna.Framework;

namespace Erigeron.WPF.SpinePlayer.Mono.Spine
{
    class SpineManager : MonoGameViewModel
    {
        private SpineLoader? _spineLoader = null;
        public string _atlasPath = "data/char_4119_wanqin_epoque_41.atlas";
        public string _skelPath = "data/char_4119_wanqin_epoque_41.skel";
        internal List<string> StartAnimationPool { get; set; } = new() { "Default" };
        internal List<string> IdleAnimationPool { get; set; } = new() { "Attack" };
        internal List<string> TouchAnimationPool { get; set; } = new() { "Interact" };
        internal List<string> DieAnimationPool { get; set; } = new() { "Die" };
        internal float[] _xy = [-1, -1];

        public override void LoadContent()
        {
            _spineLoader ??= new(GraphicsDeviceInstance);
            _spineLoader.Initialize(_atlasPath, _skelPath, _xy, StartAnimationPool, IdleAnimationPool, TouchAnimationPool, DieAnimationPool);
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
            _spineLoader?.SizeChanged(Width, Height);
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
