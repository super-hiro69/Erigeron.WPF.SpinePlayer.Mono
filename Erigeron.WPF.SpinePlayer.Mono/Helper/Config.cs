using System.IO;
using System.Text.Json;

namespace Erigeron.WPF.SpinePlayer.Mono.Helper
{
    public class Config
    {
        /// <summary>
        /// Show in App.Title
        /// </summary>
        public string CharName { get; set; } = "Default";
        /// <summary>
        /// Specifiy the path of the altas file
        /// </summary>
        public string AtlasPath { get; set; } = @".\data\Model\char_4119_wanqin_epoque_41.atlas";
        /// <summary>
        /// Specifiy the path of the skel file
        /// </summary>
        public string SkelPath { get; set; } = @".\data\Model\char_4119_wanqin_epoque_41.skel";
        /// <summary>
        /// Left of the skeleton
        /// </summary>
        public double MarginLeft { get; set; } = -1;
        /// <summary>
        /// Top of the skeleton
        /// </summary>
        public double MarginTop { get; set; } = -1;
        /// <summary>
        /// Width of the skeleton
        /// </summary>
        public double SkeletonWidth { get; set; } = -1;
        /// <summary>
        /// Height of the skeleton
        /// </summary>
        public double SkeletonHeight { get; set; } = -1;
        /// <summary>
        /// Left of the Viewer Window
        /// </summary>
        public double WindowLeft { get; set; } = 0;
        /// <summary>
        /// Top of the Viewer Window
        /// </summary>
        public double WindowTop { get; set; } = 0;
        /// <summary>
        /// Width of the Viewer Window
        /// </summary>
        public double WindowWidth { get; set; } = -1;
        /// <summary>
        /// Height of the Viewer Window
        /// </summary>
        public double WindowHeight { get; set; } = -1;
        /// <summary>
        /// <summary>
        /// Enable insert a window into desktop
        /// </summary>
        public bool DesktopInsert { get; set; } = false;
        /// <summary>
        /// Only static picture in desktop, spine disabled
        /// </summary>
        public bool StaticDesktop { get; set; } = true;
        /// <summary>
        /// Desktop atlas path
        /// </summary>
        public string AtlasPathD { get; set; } = "";
        /// <summary>
        /// Desktop skel path
        /// </summary>
        public string SkelPathD { get; set; } = "";
        public string PicPathD { get; set; } = @"C:\Application\Ark\data\Pic\wanqing_sunshine.png";
        public double MarginLeftD { get; set; } = -1;
        public double MarginTopD { get; set; } = -1;
        public double SkeletonWidthD { get; set; } = -1;
        public double SkeletonHeightD { get; set; } = -1;
        /// <summary>
        /// Left of the Viewer Window
        /// </summary>
        public double WindowLeftD { get; set; } = 0;
        /// <summary>
        /// Top of the Viewer Window
        /// </summary>
        public double WindowTopD { get; set; } = 0;
        public double WindowWidthD { get; set; } = -1;
        public double WindowHeightD { get; set; } = -1;
        public bool AudioEnabled { get; set; } = false;
        public int AudioIntervalMin { get; set; } = 2500;
        public int AudioIntervalMax { get; set; } = 3500;
        public string AudioPath { get; set; } = @".\data\Audio\";
        public List<string> StartAnimationPool { get; set; } = new() { "Default", "Relax", "Start" };
        public List<string> IdleAnimationPool { get; set; } = new() { "Attack", "[Skill_Begin|Skill_Loop|Skill_Loop|Skill_End]","Skill2","Move", "Relax","Special" };
        public List<string> TouchAnimationPool { get; set; } = new() { "Interact", "[Skill_Begin|Skill_Loop|Skill_Loop|Skill_End]" };
        public List<string> DieAnimationPool { get; set; } = new() { "Die","Sit","Sleep" };
        public List<string> StartAnimationPoolD { get; set; } = new() { "Default", "Relax", "Start" };
        public List<string> IdleAnimationPoolD { get; set; } = new() { "Attack", "[Skill_Begin|Skill_Loop|Skill_Loop|Skill_End]", "Skill2", "Move", "Relax", "Special" };
        public List<string> TouchAnimationPoolD { get; set; } = new() { "Interact", "[Skill_Begin|Skill_Loop|Skill_Loop|Skill_End]" };
        public List<string> DieAnimationPoolD { get; set; } = new() { "Die", "Sit", "Sleep" };
        public bool FullscreenHide { get; set; } = true;
        public bool HideAppHost { get; set; } = true;
        public void ExportConfig(string filePath)
        {
            // 将当前对象序列化为 JSON 格式
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true  // 使 JSON 格式易于阅读
            };
            string jsonString = JsonSerializer.Serialize(this, jsonOptions);

            // 输出调试信息，检查序列化结果
            Console.WriteLine(jsonString);  // 调试输出

            // 将 JSON 字符串写入文件
            File.WriteAllText(filePath, jsonString);
        }

        /// <summary>
        /// 从指定文件路径导入配置
        /// </summary>
        public bool ImportConfig(string filePath)
        {
            try
            {
                // 读取文件内容
                string jsonString = File.ReadAllText(filePath);

                // 将 JSON 字符串反序列化为当前对象
                var updatedConfig = JsonSerializer.Deserialize<Config>(jsonString);
                if (updatedConfig != null)
                {
                    App.ch = updatedConfig;

                    return true;
                }
            }
            catch (Exception ex)
            {
                // 处理可能的错误
                Console.WriteLine($"导入配置失败: {ex.Message}");
            }

            return false;
        }
    }
}