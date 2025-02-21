using Erigeron.WPF.SpinePlayer.Mono.Support;
using Microsoft.Xna.Framework;

namespace Erigeron.WPF.SpinePlayer.Mono.Spine
{
    class SpineManager : MonoGameViewModel
    {
        private SpineLoader? _spineLoader = null;

        public override void LoadContent()
        {
            _spineLoader ??= new(GraphicsDeviceInstance);
            _spineLoader?.Initialize();
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
    }
}
