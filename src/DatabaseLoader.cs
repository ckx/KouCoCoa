using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
#nullable enable

namespace KouCoCoa {
    // TODO: add built-in reference databases (vanilla rA dbs)?
    internal static class DatabaseLoader {
        #region Public Methods
        /// <summary>
        /// Load all databases in config file to this.AllDatabases
        /// </summary>
        public static async Task<Dictionary<RAthenaDbType, List<IDatabase>>> LoadConfigDatabases() {
            Dictionary<RAthenaDbType,List<IDatabase>> retDbMap = await LoadDatabasesFromDirectory(
                Globals.RunConfig.YamlDbDirectoryPath);
            foreach (string filePath in Globals.RunConfig.AdditionalDbPaths) {
                IDatabase db = await LoadDatabaseFromFile(filePath);
                if (db.DatabaseType == RAthenaDbType.UNSUPPORTED) {
                    continue;
                }
                retDbMap[db.DatabaseType].Add(db);
            }
            return retDbMap;
        }

        /// <summary>
        /// Load a single database into this.AllDatabases
        /// </summary>
        public static async Task<IDatabase> LoadDatabaseFromFile(string filePath) {
            if (!File.Exists(filePath)) {
                await Logger.WriteLine($"Loading db at {filePath} failed. File not found.", LogLevel.Warning);
                return new UndefinedDatabase();
            }
            await Logger.WriteLine($"{filePath}: Loading database...", LogLevel.Debug);
            IDatabase retDb = await DatabaseParser.ParseDatabaseFromFile(filePath);
            if (retDb.DatabaseType == RAthenaDbType.UNSUPPORTED) {
                return retDb;
            }
            await Logger.WriteLine($"{filePath}: Database load successful.");
            return retDb;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Call load function for all databases at directoryPath
        /// </summary>
        private static async Task<Dictionary<RAthenaDbType, List<IDatabase>>> LoadDatabasesFromDirectory(string directoryPath) {
            Dictionary<RAthenaDbType, List<IDatabase>> retDbMap = new();
            if (!Directory.Exists(directoryPath)) {
                await Logger.WriteLine($"Cannot load databases at {directoryPath}. Directory not found. Returning empty DatabaseMap.", LogLevel.Warning);
                return retDbMap;
            }

            await Logger.WriteLine($"Searching for databases in {directoryPath}...");
            string[] fileEntries = Directory.GetFiles(directoryPath);
            foreach (string filePath in fileEntries) {
                await Logger.WriteLine($"{filePath}: Database found", LogLevel.Debug);
                IDatabase db = await LoadDatabaseFromFile(filePath);
                if (db.DatabaseType == RAthenaDbType.UNSUPPORTED) {
                    continue;
                }
                // Add the database to the return map safely
                if (!retDbMap.ContainsKey(db.DatabaseType)) {
                    retDbMap[db.DatabaseType] = new();
                }
                retDbMap[db.DatabaseType].Add(db);
            }
            return retDbMap;
        }
        #endregion
    }
}
