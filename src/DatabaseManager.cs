using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace KouCoCoa {
    internal class DatabaseManager {
        #region Default Constructor
        internal DatabaseManager() {
            AllDatabases = new() {
                { DatabaseDataType.MOB_DB, new() },
                { DatabaseDataType.ITEM_DB, new() }
            };
            _dbParser = new();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// All currently loaded databases
        /// </summary>
        public Dictionary<DatabaseDataType, List<IDatabase>> AllDatabases { get; private set; }
        #endregion

        #region Private member variables
        private DatabaseParser _dbParser;
        #endregion

        #region Public Methods
        /// <summary>
        /// Load all databases in config file to this.AllDatabases
        /// </summary>
        public async Task LoadStartupDatabases() {
            await LoadDatabasesFromDirectory(Globals.RunConfig.YamlDbDirectoryPath);
            foreach (string filePath in Globals.RunConfig.AdditionalDbPaths) {
                await LoadDatabase(filePath);
            }
        }

        /// <summary>
        /// Load a single database into this.AllDatabases
        /// </summary>
        public async Task LoadDatabase(string filePath) {
            if (!File.Exists(filePath)) {
                await Logger.WriteLine($"Loading db at {filePath} failed. File not found.", LogLevel.Warning);
                return;
            }
            await Logger.WriteLine($"{filePath}: Loading database...", LogLevel.Debug);
            IDatabase db = await _dbParser.ParseDatabase(filePath);
            if (db == null) {
                await Logger.WriteLine($"{filePath}: Parse failed. Skipping database.", LogLevel.Debug);
                return;
            }
            AddDatabase(db);
            await Logger.WriteLine($"{filePath}: Database load successful.");
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Call load function for all databases at directoryPath
        /// </summary>
        private async Task LoadDatabasesFromDirectory(string directoryPath) {
            if (!Directory.Exists(directoryPath)) {
                await Logger.WriteLine($"Cannot load databases at {directoryPath}. Directory not found.", LogLevel.Warning);
                return;
            }

            await Logger.WriteLine($"Searching for databases in {directoryPath}...");
            string[] fileEntries = Directory.GetFiles(directoryPath);
            foreach (string filePath in fileEntries) {
                await Logger.WriteLine($"{filePath}: Database found", LogLevel.Debug);
                await LoadDatabase(filePath);
            }
        }

        /// <summary>
        /// Adds a database to AllDatabases. Reloads the database if it's already present at its defined filepath.
        /// </summary>
        /// <param name="db"></param>
        private async void AddDatabase(IDatabase db) {
            if (AllDatabases.ContainsKey(db.DatabaseType)) {
                foreach (IDatabase loadedDb in AllDatabases[db.DatabaseType]) {
                    if (db.FilePath == loadedDb.FilePath) {
                        await Logger.WriteLine($"Database {db.Name} at {db.FilePath} was already loaded. Removing old entry to reload the database.", LogLevel.Debug);
                        AllDatabases[loadedDb.DatabaseType].Remove(loadedDb);
                    }
                }

                AllDatabases[db.DatabaseType].Add(db);
            }
        }
        #endregion
    }
}
