using Erigeron.WPF.SpinePlayer.Mono.Helper;
using Erigeron.WPF.SpinePlayer.Mono.Support;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spine;

namespace Erigeron.WPF.SpinePlayer.Mono.Spine
{
    public class SpineLoader
    {
        protected GraphicsDevice _graphicsDevice;
        SkeletonRenderer? skeletonRenderer;
        List<string>? _animationList;
        Atlas? atlas;
        Skeleton? skeleton;
        AnimationState? state;
        internal float[] _xy = [-1, -1];
        internal List<string> StartAnimationPool { get; set; } = new() { "Default" };
        internal List<string> IdleAnimationPool { get; set; } = new() { "Attack" };
        internal List<string> TouchAnimationPool { get; set; } = new() { "Interact" };
        internal List<string> DieAnimationPool { get; set; } = new() { "Die" };
        internal Queue<string>? AnimationQ = null;
        public SpineLoader(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void UpdateInput()
        {

        }

        public void Initialize(string atlasPath, string skelPath, float[] xy, List<string> startAnimationPool, List<string> idleAnimationPool, List<string> touchAnimationPool, List<string> dieAnimationPool)
        {
            _xy = xy;
            StartAnimationPool = startAnimationPool;
            IdleAnimationPool = idleAnimationPool;
            TouchAnimationPool = touchAnimationPool;
            DieAnimationPool = dieAnimationPool;
            skeletonRenderer = new SkeletonRenderer(_graphicsDevice);
            skeletonRenderer.PremultipliedAlpha = true;
            atlas = new Atlas(atlasPath, new XnaTextureLoader(_graphicsDevice));

            SkeletonBinary binary = new SkeletonBinary(atlas);
            binary.Scale = 0.5f;
            SkeletonData skeletonData = binary.ReadSkeletonData(skelPath);

            skeleton = new Skeleton(skeletonData);
            AnimationStateData stateData = new AnimationStateData(skeleton.Data);
            state = new AnimationState(stateData);
            skeleton.X = _xy[0] / 2;
            skeleton.Y = _xy[1];
            // We want 0.2 seconds of mixing time when transitioning from
            // any animation to any other animation.
            stateData.DefaultMix = 0.2f;
            EnumAnimation();
            if (_animationList!.Count > 0)
                SetStartAnimation();
            else
                throw new Exception("No animation here");

        }

        private void SetStartAnimation()
        {

            var animation = StartAnimationPool[new Random().Next(StartAnimationPool.Count)];
            if (animation != null)
            {
                SetAnimationQ(animation);
                if (AnimationQ!.TryDequeue(out string? s) && _animationList!.Contains(s))
                {
                    state!.SetAnimation(0, s!, false);
                }
                else
                {
                    SetIdleAnimation();
                }
            }
            else
            {
                SetIdleAnimation();
            }

            state!.Complete += delegate
            {
                if (AnimationQ.TryDequeue(out string? s) && s != null && _animationList!.Contains(s))
                {
                    state!.SetAnimation(0, s!, false);
                }
                else
                {
                    SetIdleAnimation();
                }
            };
        }
        private void SetIdleAnimation()
        {
            var animation = IdleAnimationPool[new Random().Next(IdleAnimationPool.Count)];
            if (animation == null)
            {
                SetIdleAnimation();
                return;
            }
            SetAnimationQ(animation);
            if (AnimationQ!.TryDequeue(out string? s) && _animationList!.Contains(s))
            {
                state!.SetAnimation(0, s!, false);
            }
            else
            {
                SetIdleAnimation();
            }
        }
        internal void SetTouchAnimation()
        {
            var animation = TouchAnimationPool[new Random().Next(TouchAnimationPool.Count)];
            if (animation == null)
            {
                SetIdleAnimation();
                return;
            }
            SetAnimationQ(animation);
            if (AnimationQ!.TryDequeue(out string? s) && _animationList!.Contains(s))
            {
                state!.SetAnimation(0, s!, false);
            }
            else
            {
                SetIdleAnimation();
            }
        }
        internal void SetDieAnimation()
        {
            var animation = DieAnimationPool[new Random().Next(DieAnimationPool.Count)];
            if (animation == null)
            {
                Environment.Exit(0);
                return;
            }
            SetAnimationQ(animation);
            if (AnimationQ!.TryDequeue(out string? s) && _animationList!.Contains(s))
            {
                state!.SetAnimation(0, s!, false);
                state.Complete += (e) =>
                {

                    Environment.Exit(0);
                };
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void SetAnimationQ(string s)
        {
            AnimationQ ??= new();
            AnimationQ.Clear();
            if (s.StartsWith('[') && s.EndsWith(']'))
            {
                s = s[1..^1];
                var aList = s.Split('|');
                for (int i = 0; i < aList.Length; i++)
                {
                    AnimationQ.Enqueue(aList[i]);
                }
            }
            else
            {
                AnimationQ.Enqueue(s);
            }
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
            state!.Update(deltaTime);
            state.Apply(skeleton);
            skeleton!.UpdateWorldTransform();
            ((BasicEffect)skeletonRenderer!.Effect).Projection = Matrix.CreateOrthographicOffCenter(0, _xy[0], _xy[1], 0, 1, 0);
            skeletonRenderer.Begin();
            skeletonRenderer.Draw(skeleton);
            skeletonRenderer.End();
        }

        internal void SizeChanged(double width, double height)
        {
            _xy[0] = (float)width;
            _xy[1] = (float)height;
        }
    }
}
