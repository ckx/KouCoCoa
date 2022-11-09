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
            _yamlDeserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();
        }
        #endregion

        #region Private member variables
        private readonly IDeserializer _yamlDeserializer;
        #endregion

        #region Public Methods
        /// <summary>
        /// Load a single database into this.AllDatabases
        /// </summary>
        // todo: if we move to pure yaml for databases this interface is unnecessary
        public async Task<IDatabase?> ParseDatabase(string filePath) {
            IDatabase? retDb = null;
            string fileExt = Path.GetExtension(filePath);
            switch (fileExt) {
                case ".yml":
                    retDb = await ParseYamlDatabase(filePath);
                    break;
                case ".txt":
                    GetScriptDatabase();
                    break;
                default:
                    await Logger.WriteLine($"{filePath}: Attempted to parse database but file type was unknown.", LogLevel.Warning);
                    break;
            }

            return retDb;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Load a YAML database at filePath and add it to AllDatabases.
        /// If the file is already in AllDatabases, it will be reloaded.
        /// </summary>
        private async Task<IDatabase?> ParseYamlDatabase(string filePath) {
            IDatabase? retDb = null;
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            // inputDb acts as a temporary object while we determine the structure
            dynamic inputDb;
            try {
                using (var yamlString = File.ReadAllTextAsync(filePath)) {
                    await Logger.WriteLine($"{filePath}: Identified as a YAML file, deserializing into ExpandoObject.", LogLevel.Debug);
                    inputDb = _yamlDeserializer.Deserialize<ExpandoObject>(await yamlString);
                }
            } catch (Exception) {
                throw;
            }

            DatabaseDataType dbType = await DetermineDatabaseType(inputDb);

            switch (dbType) {
                case DatabaseDataType.MOB_DB:
                    retDb = new MobDatabase();
                    break;
                case DatabaseDataType.ITEM_DB:
                    retDb = new ItemDatabase();
                    break;
                case DatabaseDataType.UNDEFINED:
                    await Logger.WriteLine($"{filePath}: Undefined database type, skipping.", LogLevel.Debug);
                    break;
                default:
                    break;
            }

            if (retDb != null) {
                retDb.FilePath = filePath;
                retDb.Name = fileName;
            }
            return retDb;
        }

        // with any luck I won't need this (if we move all databases to YAML)
        private void GetScriptDatabase() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check an ExpandoObject to determine what type of database it should deserialize into.
        /// </summary>
        private static async Task<DatabaseDataType> DetermineDatabaseType(dynamic inputDb) {
            DatabaseDataType retDbType = DatabaseDataType.UNDEFINED;

            if (!((IDictionary<string, object>)inputDb).ContainsKey("Header")) {
                await Logger.WriteLine($"Cannot determine database type, returning UNDEFINED.", LogLevel.Debug);
                return retDbType;
            }

            // All supported DatabaseDataTypes need to go here, and it needs to match the rA type.
            foreach (KeyValuePair<object, object> entry in inputDb.Header) {
                if (entry.Key.ToString() == "Type") {
                    switch (entry.Value.ToString()) {
                        case "MOB_DB":
                            retDbType = DatabaseDataType.MOB_DB;
                            break;
                        case "ITEM_DB":
                            retDbType = DatabaseDataType.ITEM_DB;
                            break;
                        default:
                            break;
                    }
                }
            }

            return retDbType;
        }
        #endregion
    }
}