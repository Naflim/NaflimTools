using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NaflimTools.WPF.Model
{
    /// <summary>  
    /// 工具集配置类，用于管理工具的配置文件加载和保存。  
    /// </summary>  
    public class ToolsConfig
    {
        /// <summary>  
        /// 快速操作视图模型实例。  
        /// </summary>  
        public QuickOperationViewModel QuickOperationViewModel { get; set; }

        /// <summary>  
        /// 初始化 <see cref="ToolsConfig"/> 类的新实例。  
        /// </summary>  
        public ToolsConfig()
        {
            QuickOperationViewModel = new QuickOperationViewModel();
        }

        /// <summary>  
        /// 从配置文件加载工具集配置。  
        /// 如果配置文件不存在，则返回一个新的 <see cref="ToolsConfig"/> 实例。  
        /// </summary>  
        /// <returns>加载的 <see cref="ToolsConfig"/> 实例。</returns>  
        public static ToolsConfig Load()
        {
            string folder = "config";
            string fileName = nameof(ToolsConfig) + ".json";
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, folder, fileName);

            if (!Directory.Exists(Path.Combine(currentDirectory, folder)))
            {
                Directory.CreateDirectory(Path.Combine(currentDirectory, folder));
            }

            if (!File.Exists(filePath))
            {
                return new ToolsConfig();
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ToolsConfig>(json) ?? new ToolsConfig();
        }

        /// <summary>  
        /// 将当前工具集配置保存到配置文件。  
        /// 如果目标文件夹不存在，则会自动创建。  
        /// </summary>  
        public void Save()
        {
            string folder = "config";
            string fileName = nameof(ToolsConfig) + ".json";
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, folder, fileName);

            if (!Directory.Exists(Path.Combine(currentDirectory, folder)))
            {
                Directory.CreateDirectory(Path.Combine(currentDirectory, folder));
            }

            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
