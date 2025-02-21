using Erigeron.WPF.SpinePlayer.Mono.Support;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spine;

namespace Erigeron.WPF.SpinePlayer.Mono.Spine
{
    public class SpineLoader
    {
        protected GraphicsDevice _graphicsDevice;
        SkeletonRenderer skeletonRenderer;
        List<string> _animationList;
        Atlas atlas;
        Skeleton skeleton;
        AnimationState state;
        float[] _xy = [100, 100, 50, 50, 0, 0];
        public SpineLoader(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void UpdateInput()
        {

        }

        public void Initialize(string atlasPath = "data/char_4119_wanqin_epoque_41.atlas", string skelPath = "data/char_4119_wanqin_epoque_41.skel")
        {
            skeletonRenderer = new SkeletonRenderer(_graphicsDevice);
            skeletonRenderer.PremultipliedAlpha = true;
            atlas = new Atlas(atlasPath, new XnaTextureLoader(_graphicsDevice));

            SkeletonBinary binary = new SkeletonBinary(atlas);
            binary.Scale = 0.5f;
            SkeletonData skeletonData = binary.ReadSkeletonData(skelPath);

            skeleton = new Skeleton(skeletonData);
            AnimationStateData stateData = new AnimationStateData(skeleton.Data);
            state = new AnimationState(stateData);
            skeleton.X = 1336/2;
            skeleton.Y = 513/2;
            // We want 0.2 seconds of mixing time when transitioning from
            // any animation to any other animation.
            stateData.DefaultMix = 0.2f;

            EnumAnimation();
            if (_animationList.Count > 0)
                state.SetAnimation(0, _animationList[0], false);
            else
                throw new Exception("No animation here");
            state.Complete += delegate
            {
                App.sv.Title = $"{skeleton.Data.Width}-{skeleton.Data.Height}";
                state.SetAnimation(0, _animationList[new Random().Next(0, _animationList.Count - 1)], false);
            };
        }

        private void CenterSkeleton()
        {
            var w = skeleton.Data.Width;
            var h = skeleton.Data.Height;

            float scale = Math.Min(_xy[0] / w, _xy[1] / h);

            _xy[2] = w * scale;
            _xy[3] = h * scale;

            _xy[4] = (_xy[0] - _xy[2]) / 2;
            _xy[5] = (_xy[1] - _xy[3]) / 2;
        }

        private void EnumAnimation()
        {
            _animationList = skeleton.Data.Animations.Select(x => x.Name).ToList();
        }

        public void OnMouseUp(MouseStateArgs mouseState)
        {
        }

        public void Render(float deltaTime)
        {
            state.Update(deltaTime);
            state.Apply(skeleton);
            skeleton.UpdateWorldTransform();
            ((BasicEffect)skeletonRenderer.Effect).Projection = Matrix.CreateOrthographicOffCenter(0, 1336, 513 * 2, 0, 1, 0);
            skeletonRenderer.Begin();
            skeletonRenderer.Draw(skeleton);
            skeletonRenderer.End();
        }

        internal void SizeChanged(double width, double height)
        {
            _xy[0] = (float)width;
            _xy[1] = (float)height;
            CenterSkeleton();
        }
    }
}
