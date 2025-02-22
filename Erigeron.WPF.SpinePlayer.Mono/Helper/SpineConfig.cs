namespace Erigeron.WPF.SpinePlayer.Mono.Helper
{
    public class SpineConfig
    {
        public string CharName { get; set; } = "Default";
        public string AtlasPath { get; set; } = @".\data\Model\Wanqing\char_4119_wanqin_epoque_41.atlas";
        public string SkelPath { get; set; } = @".\data\Model\Wanqing\char_4119_wanqin_epoque_41.skel";
        public double? WindowLeft { get; set; } = null;
        public double? WindowTop { get; set; } = null;
        public double? WindowWidth { get; set; } = null;
        public double? WindowHeight { get; set; } = null;
        public double? SkeletonLeft { get; set; } = null;
        public double? SkeletonTop { get; set; } = null;
        public double? SkeletonWidth { get; set; } = null;
        public double? SkeletonHeight { get; set; } = null;

        public double? SkeletonX { get; set; } = null;
        public double? SkeletonY { get; set; } = null;
        public double SkeletonMix { get; set; } = 0.2;
        public float? SkeletonScale { get; set; } = 0.5f;
        public float? SkeletonScaleX { get; set; } = 1f;
        public float? SkeletonScaleY { get; set; } = 1f;
        public bool? EnableMove { get; set; } = true;
        public double? MoveClock { get; set; } = 50;
        public double? MoveMin { get; set; } = 0.0;
        public double? MoveMax { get; set; } = null;
        public bool? AutoRevserse { get; set; } = null;
        public double? ReversePossibility { get; set; } = 50;
        public List<string> StartAnimationPool { get; set; } = new() { "Default", "Relax", "Start" };
        public List<string> IdleAnimationPool { get; set; } = new() { "Attack", "[Skill_Begin|Skill_Loop|Skill_Loop|Skill_End]", "Skill2", "Move", "Relax", "Special" };
        public List<string> TouchAnimationPool { get; set; } = new() { "Interact", "[Skill_Begin|Skill_Loop|Skill_Loop|Skill_End]" };
        public List<string> DieAnimationPool { get; set; } = new() { "Die", "Sit", "Sleep" };
        public Dictionary<string, double> MoveAnimationPool { get; set; } = new() { { "Move", 1.0 }, { "Move2", 1.5 } };
    }
}
