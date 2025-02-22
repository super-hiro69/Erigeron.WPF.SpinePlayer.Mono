using System.IO;
using System.Runtime.Versioning;
using System.Text.Json;

namespace Erigeron.WPF.SpinePlayer.Mono.Helper
{
    [SupportedOSPlatform("Windows")]
    public class Config
    {
        public SpineConfig ForeSpine { get; set; } = new();
        /// <summary>
        /// <summary>
        /// Enable insert a window into desktop
        /// </summary>
        public bool DesktopInsert { get; set; } = false;
        /// <summary>
        /// Only static picture in desktop, spine disabled
        /// </summary>
        public bool StaticDesktop { get; set; } = true;
        public string PicPathD { get; set; } = @"C:\Example\data\wanqing_sunshine.png";
        public SpineConfig DesktopSpine { get; set; } = new();
        public bool AudioEnabled { get; set; } = false;
        public int AudioIntervalMin { get; set; } = 2500;
        public int AudioIntervalMax { get; set; } = 3500;
        public string AudioPath { get; set; } = @".\data\Audio\";
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