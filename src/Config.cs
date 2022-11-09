using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KouCoCoa {
    /// <summary>
    /// config.yml.
    /// </summary>
    public class Config {
        public Config() {
            AdditionalDbPaths = new();
            LoggingLevel = LogLevel.Info;
            WindowPositionXY = new int[] { 100, 100 };
            WindowResolutionXY = new int[] { 1280, 720 };
            SilenceLogger = false;
        }

        #region Properties
        public static string ConfigPath { get { return "config.yml"; } }
        public string YamlDbDirectoryPath { get; set; }
        public List<string> AdditionalDbPaths { get; set; }
        public int[] WindowPositionXY;
        public int[] WindowResolutionXY;
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

    public class ConfigManager {
        #region Static Methods
        /// <summary>
        /// Load the config at Config.ConfigPath.
        /// </summary>
        public static async Task<Config> GetConfig() {
            await Logger.WriteLine($"Loading config from {Config.ConfigPath}...");
            Config retConf = new();
            var deserializer = new DeserializerBuilder().Build();
            try {
                using (var configString = File.ReadAllTextAsync(Config.ConfigPath)) {
                    retConf = deserializer.Deserialize<Config>(await configString);
                    await Logger.WriteLine($"{Config.ConfigPath} loaded successfully.");
                }
            } catch (FileNotFoundException) {
                await Logger.WriteLine($"{Config.ConfigPath} not found. Generating new config file.");
                await StoreConfig(retConf);
            } catch (Exception ex) {
                await Logger.WriteLine(
                    $"Loading {Config.ConfigPath} failed. Exception thrown:\n" + 
                    $"{ex.Message}\n\n" + 
                    $"{ex.StackTrace}", LogLevel.Error);
                throw;
            }

            return retConf;
        }

        /// <summary>
        /// Save a config to the persistent file at Config.ConfigPath.
        /// </summary>
        public static async Task StoreConfig(Config runningConfig) {
            await Logger.WriteLine($"Attempting to write config to {Config.ConfigPath}...");
            var serializer = new SerializerBuilder().Build();
            var yamlString = serializer.Serialize(runningConfig);
            try {
                using (StreamWriter sw = File.CreateText(Config.ConfigPath)) {
                    sw.Write(yamlString);
                    await Logger.WriteLine($"Running config saved to {Config.ConfigPath}.");
                }
            } catch (Exception ex) {
                await Logger.WriteLine(
                    $"Failed to save config to {Config.ConfigPath}. Exception thrown:\n" +
                    $"{ex.Message}\n\n" + 
                    $"{ex.StackTrace}", LogLevel.Error);
                throw;
            }
        }
        #endregion
    }
}
