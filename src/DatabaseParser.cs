using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Dynamic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

#nullable enable

namespace KouCoCoa {
    internal class DatabaseParser {
        #region Default Constructor
        internal DatabaseParser() {
            AllDatabases = new() {
                { DatabaseDataType.MOB_DB, new() },
                { DatabaseDataType.ITEM_DB, new() }
            };

            _yamlDeserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();
        }
        #endregion

        #region Properties
        internal Dictionary<DatabaseDataType, List<IDatabase>> AllDatabases { get; private set; }
        #endregion

        #region Private member variables
        //internal Dictionary<DatabaseDataType, string> _dbPrefixes;
        private readonly IDeserializer _yamlDeserializer;
        #endregion

        #region Public Methods
        public async Task ParseAllDatabases() {
            throw new NotImplementedException();
        }

        public async Task<IDatabase?> LoadYamlDatabase(string filePath) {
            IDatabase? retDb = null;
            // inputDb's header contains important metadata, and Body contains the meat of the DB (a list of some obj type, usually)
            dynamic inputDb;
            DatabaseDataType dbType = DatabaseDataType.UNDEFINED;
            try {
                using (var yamlString = File.ReadAllTextAsync(filePath)) {
                    await Logger.WriteLine($"Found yaml db at {filePath}, loading...");
                    inputDb = _yamlDeserializer.Deserialize<ExpandoObject>(await yamlString);
                }
            } catch (Exception) {
                throw;
            }

            foreach (KeyValuePair<object, object> entry in inputDb.Header) {
                if (entry.Key.ToString() == "Type") {
                    switch (entry.Value.ToString()) {
                        case "MOB_DB":
                            dbType = DatabaseDataType.MOB_DB;
                            break;
                        default:
                            break;
                    }
                }
            }

            switch (dbType) {
                case DatabaseDataType.MOB_DB:
                    retDb = new MobDatabase();
                    break;
                case DatabaseDataType.ITEM_DB:
                    retDb = new ItemDatabase();
                    break;
                default:
                    break;
            }

            if (retDb != null) {
                retDb.Name = Path.GetFileNameWithoutExtension(filePath);
                retDb.FilePath = filePath;
            }

            return retDb;
        }

        public async Task LoadScriptDatabase() {
            throw new NotImplementedException();
        }

        public async Task LoadDatabasesFromDirectory(string directoryPath) {
            // Ensure the directory is reachable
            if (!Directory.Exists(directoryPath)) {
                await Logger.WriteLine($"Directory {directoryPath} not found.", LogLevel.Warning);
                return;
            }

            // Get all files in the directory, iterate over them
            string[] fileEntries = Directory.GetFiles(directoryPath);
            foreach (string fileName in fileEntries) {
                string fileExt = Path.GetExtension(fileName);
                switch (fileExt) {
                    case ".yml":
                        await LoadYamlDatabase(fileName);
                        break;
                    case ".txt":
                        await LoadScriptDatabase();
                        break;
                    default:
                        await Logger.WriteLine($"Extension type unknown for database: {fileName}, skipping.", LogLevel.Warning);
                        break;
                }
            }
        }
        #endregion
    }
}