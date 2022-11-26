using System;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KouCoCoa {
    /// <summary>
    /// config.yml.
    /// </summary>
    internal class Config {
        public Config() {
            LoggingLevel = LogLevel.Info;
            YamlDbDirectoryPath = "";
            AdditionalDbPaths = new();
            SilenceLogger = false;
        }

        #region Properties
        public static string ConfigPath { get { return "config.yml"; } }
        public string YamlDbDirectoryPath { get; set; }
        public List<string> AdditionalDbPaths { get; set; }
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

    internal class ConfigManager {
        #region Static Methods
        /// <summary>
        /// Load the config file at filePath.
        /// </summary>
        public static Config GetConfig(string filePath = "config.yml") {
            Logger.WriteLine($"Loading config from {filePath}...");
            Config retConf = new();
            var deserializer = new DeserializerBuilder().Build();
            try {
                string configString = File.ReadAllText(filePath);
                retConf = deserializer.Deserialize<Config>(configString);
                Logger.WriteLine($"{filePath} loaded successfully.");
            } catch (FileNotFoundException) {
                Logger.WriteLine($"{filePath} not found. Generating new config file.");
                StoreConfig(retConf);
            } catch (Exception ex) {
                Logger.WriteLine(
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
        public static void StoreConfig(Config runningConfig, string filePath = "config.yml") {
            Logger.WriteLine($"Attempting to write config to {filePath}...");
            var serializer = new SerializerBuilder().Build();
            var yamlString = serializer.Serialize(runningConfig);
            try {
                using (StreamWriter sw = File.CreateText(filePath)) {
                    sw.Write(yamlString);
                    Logger.WriteLine($"Running config saved to {filePath}.");
                }
            } catch (Exception ex) {
                Logger.WriteLine(
                    $"Failed to save config to {filePath}. Exception thrown:\n" +
                    $"{ex.Message}\n\n" + 
                    $"{ex.StackTrace}", LogLevel.Error);
                throw;
            }
        }
        #endregion
    }
}
