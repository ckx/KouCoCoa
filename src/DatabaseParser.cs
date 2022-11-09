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
        public async Task<IDatabase?> ParseDatabaseFromFile(string filePath) {
            IDatabase? retDb = null;
            string fileExt = Path.GetExtension(filePath);
            if (fileExt != ".yml") {
                await Logger.WriteLine($"{filePath}: Attempted to parse database but file type was unknown. Right now only .yml files are supported.", LogLevel.Warning);
                return retDb;
            }
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

            retDb = await ParseDatabase(inputDb);

            if (retDb != null) {
                retDb.FilePath = filePath;
                retDb.Name = fileName;
            }

            return retDb;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<IDatabase?> ParseDatabase(dynamic inputDb) {
            IDatabase? retDb = null;
            DatabaseDataType dbType = await DetermineDatabaseType(inputDb);

            switch (dbType) {
                case DatabaseDataType.MOB_DB:
                    MobDatabase mobDb = new();
                    mobDb.Mobs = await DeserializeMobDb(inputDb);
                    retDb = new MobDatabase(mobDb);
                    break;
                case DatabaseDataType.ITEM_DB:
                    retDb = new ItemDatabase();
                    break;
                case DatabaseDataType.UNDEFINED:
                    await Logger.WriteLine($"Undefined database type, skipping.", LogLevel.Debug);
                    break;
                default:
                    break;
            }

            return retDb;
        }
        #endregion

        #region Private methods
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
            await Logger.WriteLine($"DetermineDatabaseType identified type as: {retDbType}", LogLevel.Debug);
            return retDbType;
        }

        private async Task<List<Mob>> DeserializeMobDb(dynamic mobDb) {
            List<Mob> retList = new();
            if (!((IDictionary<string, object>)mobDb).ContainsKey("Body")) {
                await Logger.WriteLine($"MobDB found, but no Body object is defined for this database. Returning empty Mob List.", LogLevel.Warning);
                return retList;
            }

            List<object> mobEntries = mobDb.Body;

            foreach (dynamic mobEntry in mobEntries) {
                Mob mob = new();
                mob.Id = UInt32.Parse(mobEntry["Id"]);
                Console.WriteLine(mob.Id);
            }

            await Logger.WriteLine($"Found {retList.Count} mobs in this mob database.", LogLevel.Debug);
            return retList;
        }
        #endregion
    }
}