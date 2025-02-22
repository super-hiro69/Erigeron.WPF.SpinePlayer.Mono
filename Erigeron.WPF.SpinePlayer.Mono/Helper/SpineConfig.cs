namespace Erigeron.WPF.SpinePlayer.Mono.Helper
{
    public class SpineConfig
    {
        public string CharName { get; set; } = "Default";
        /// <summary>
        /// Specifiy the path of the altas file
        /// </summary>
        public string AtlasPath { get; set; } = @".\data\Model\Wanqing\char_4119_wanqin_epoque_41.atlas";
        /// <summary>
        /// Specifiy the path of the skel file
        /// </summary>
        public string SkelPath { get; set; } = @".\data\Model\Wanqing\char_4119_wanqin_epoque_41.skel";
        /// <summary>
        /// Left of the skeleton
        /// </summary>
        public double? MarginLeft { get; set; } = null;
        /// <summary>
        /// Top of the skeleton
        /// </summary>
        public double? MarginTop { get; set; } = null;
        public double? SkeletonX { get; set; } = null;
        public double? SkeletonY { get; set; } = null;
        public double SkeletonMix { get; set; } = 0.2;
        /// <summary>
        /// Width of the skeleton
        /// </summary>
        public double? SkeletonWidth { get; set; } = null;
        /// <summary>
        /// Height of the skeleton
        /// </summary>
        public double? SkeletonHeight { get; set; } = null;
        public float SkeletonScale { get; set; } = 0.5f;
        /// <summary>
        /// Left of the Viewer Window
        /// </summary>
        public double? WindowLeft { get; set; } = null;
        /// <summary>
        /// Top of the Viewer Window
        /// </summary>
        public double? WindowTop { get; set; } = null;
        /// <summary>
        /// Width of the Viewer Window
        /// </summary>
        public double? WindowWidth { get; set; } = null;
        /// <summary>
        /// Height of the Viewer Window
        /// </summary>
        public double? WindowHeight { get; set; } = null;
        public List<string> StartAnimationPool { get; set; } = new() { "Default", "Relax", "Start" };
        public List<string> IdleAnimationPool { get; set; } = new() { "Attack", "[Skill_Begin|Skill_Loop|Skill_Loop|Skill_End]", "Skill2", "Move", "Relax", "Special" };
        public List<string> TouchAnimationPool { get; set; } = new() { "Interact", "[Skill_Begin|Skill_Loop|Skill_Loop|Skill_End]" };
        public List<string> DieAnimationPool { get; set; } = new() { "Die", "Sit", "Sleep" };
    }
}
