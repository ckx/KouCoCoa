using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KouCoCoa {
    /// <summary>
    /// config.yml.
    /// </summary>
    internal class Config 
    {
        public Config() 
        {
            LoggingLevel = LogLevel.Info;
            YamlDbDirectoryPath = "";
            AdditionalDbPaths = new();
            SilenceLogger = false;
            NpcIdentityLub = "data/default_npcidentity.lub";
        }

        #region Properties
        public static string ConfigPath { get { return "config.yml"; } }
        public string YamlDbDirectoryPath { get; set; }
        public List<string> AdditionalDbPaths { get; set; }
        public string NpcIdentityLub { get; set; }
        public bool SilenceLogger;

        /// <summary>
        /// 0 = Error
        /// 1 = Warning
        /// 2 = Information
        /// 3 = Debug
        /// </summary>
        public LogLevel LoggingLevel { get; set; }
        #endregion
    }

    internal class ConfigIO 
    {
        #region Static Methods
        /// <summary>
        /// Load the config file at filePath.
        /// </summary>
        public static async Task<Config> GetConfig(string filePath = "config.yml") {
            await Logger.WriteLineAsync($"Loading config from {filePath}...");
            Config retConf = new();
            var deserializer = new DeserializerBuilder().Build();
            try {
                string configString = await File.ReadAllTextAsync(filePath);
                retConf = deserializer.Deserialize<Config>(configString);
                await Logger.WriteLineAsync($"{filePath} loaded successfully.");
            } catch (FileNotFoundException) {
                await Logger.WriteLineAsync($"{filePath} not found. Generating new config file.");
                await StoreConfig(retConf);
            } catch (Exception ex) {
                await Logger.WriteLineAsync(
                    $"Loading {filePath} failed. Exception thrown:\n" + 
                    $"{ex.Message}\n\n" + 
                    $"{ex.StackTrace}", LogLevel.Error);
                throw;
            }

            return retConf;
        }

        /// <summary>
        /// Save a config to the persistent file at Config.ConfigPath.
        /// </summary>
        public static async Task StoreConfig(Config runningConfig, string filePath = "config.yml") 
        {
            await Logger.WriteLineAsync($"Attempting to write config to {filePath}...");
            var serializer = new SerializerBuilder().Build();
            var yamlString = serializer.Serialize(runningConfig);
            try {
                using (StreamWriter sw = File.CreateText(filePath)) {
                    await sw.WriteAsync(yamlString);
                    await Logger.WriteLineAsync($"Running config saved to {filePath}.");
                }
            } catch (Exception ex) {
                await Logger.WriteLineAsync(
                    $"Failed to save config to {filePath}. Exception thrown:\n" +
                    $"{ex.Message}\n\n" + 
                    $"{ex.StackTrace}", LogLevel.Error);
                throw;
            }
        }
        #endregion
    }
}
