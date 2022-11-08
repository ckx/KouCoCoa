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
            DbPaths = new() { 
                { "mob_db", new List<string>() { @"c:\repos\roguenarok\roguenarok\db\mob_db_rogue.yml" } },
                { "item_db", new List<string>() { @"c:\repos\roguenarok\roguarnok\db\item_db_rogue_testing.yml" } }
            };
            LoggingLevel = LogLevel.Info;
            WindowXY = new int[] { 100, 100 };
            ResolutionXY = new int[] { 1280, 720 };
        }

        #region Properties
        public static string ConfigPath { get { return "config.yml"; } }
        public Dictionary<string, List<string>> DbPaths { get; set; }
        public int[] WindowXY;
        public int[] ResolutionXY;

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
        public static async Task<Config> GetConfig() {
            Config retConf = new();
            var deserializer = new DeserializerBuilder().Build();
            try {
                using (var configString = File.ReadAllTextAsync(Config.ConfigPath)) {
                    retConf = deserializer.Deserialize<Config>(await configString);
                }
            } catch (FileNotFoundException) {
                StoreConfig(retConf);
            }

            return retConf;
        }

        public static void StoreConfig(Config runningConfig) {
            var serializer = new SerializerBuilder().Build();
            var yamlString = serializer.Serialize(runningConfig);
            try {
                using (StreamWriter sw = File.CreateText(Config.ConfigPath)) {
                    sw.Write(yamlString);
                }
            } catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}
