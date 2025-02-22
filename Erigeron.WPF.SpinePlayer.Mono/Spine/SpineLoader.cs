using Erigeron.WPF.SpinePlayer.Mono.Helper;
using Erigeron.WPF.SpinePlayer.Mono.Support;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spine;
using System.Runtime.Versioning;
using System.Windows;

namespace Erigeron.WPF.SpinePlayer.Mono.Spine
{
    [SupportedOSPlatform("Windows")]
    public class SpineLoader
    {
        protected GraphicsDevice _graphicsDevice;
        SpineConfig? _sc = null;
        SkeletonRenderer? skeletonRenderer;
        List<string>? _animationList;
        Atlas? atlas;
        Skeleton? skeleton;
        AnimationState? state;
        internal Queue<string>? AnimationQ = null;
        public SpineLoader(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void UpdateInput()
        {

        }

        public void Initialize(SpineConfig sc)
        {
            _sc = sc;
            skeletonRenderer = new SkeletonRenderer(_graphicsDevice);
            skeletonRenderer.PremultipliedAlpha = true;
            atlas = new Atlas(_sc.AtlasPath, new XnaTextureLoader(_graphicsDevice));

            SkeletonBinary binary = new SkeletonBinary(atlas);
            binary.Scale = _sc.SkeletonScale;
            SkeletonData skeletonData = binary.ReadSkeletonData(_sc.SkelPath);

            skeleton = new Skeleton(skeletonData);
            AnimationStateData stateData = new AnimationStateData(skeleton.Data);
            state = new AnimationState(stateData);
            skeleton.X = ((float)(_sc.SkeletonX ?? GetSkeletonWidth())) / 2;
            skeleton.Y = ((float)(_sc.SkeletonY ?? GetSkeletonHeight()));
            // We want 0.2 seconds of mixing time when transitioning from
            // any animation to any other animation.
            stateData.DefaultMix = ((float)sc.SkeletonMix);
            EnumAnimation();
            if (_animationList!.Count > 0)
                SetStartAnimation();
            else
                throw new Exception("No animation here");

        }

        private void SetStartAnimation()
        {

            var animation = _sc!.StartAnimationPool[new Random().Next(_sc.StartAnimationPool.Count)];
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
                if (AnimationQ!.TryDequeue(out string? s) && s != null && _animationList!.Contains(s))
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
            var animation = _sc!.IdleAnimationPool[new Random().Next(_sc.IdleAnimationPool.Count)];
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
            var animation = _sc!.TouchAnimationPool[new Random().Next(_sc.TouchAnimationPool.Count)];
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
            var animation = _sc!.DieAnimationPool[new Random().Next(_sc.DieAnimationPool.Count)];
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
            _animationList = skeleton!.Data.Animations.Select(x => x.Name).ToList();
        }

        public void OnMouseUp(MouseStateArgs mouseState)
        {
        }

        public void Render(float deltaTime)
        {
            state!.Update(deltaTime);
            state.Apply(skeleton);
            skeleton!.UpdateWorldTransform();
            ((BasicEffect)skeletonRenderer!.Effect).Projection = Matrix.CreateOrthographicOffCenter(0, ((float)GetSkeletonWidth()), ((float)GetSkeletonHeight()), 0, 1, 0);
            skeletonRenderer.Begin();
            skeletonRenderer.Draw(skeleton);
            skeletonRenderer.End();
        }

        private double GetSkeletonWidth()
        {
            return _sc!.SkeletonWidth ?? _sc.WindowWidth ?? SystemParameters.PrimaryScreenWidth;
        }


        private double GetSkeletonHeight()
        {
            return _sc!.SkeletonHeight ?? _sc.WindowHeight ?? SystemParameters.PrimaryScreenHeight;
        }
    }
}
