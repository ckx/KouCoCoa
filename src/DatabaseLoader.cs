using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Dynamic;
using System.Reflection;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace KouCoCoa {
    // TODO: add built-in reference databases (vanilla rA dbs)?
    internal static class DatabaseLoader {
        #region Public Methods
        public static async Task<Dictionary<RAthenaDbType, List<IDatabase>>> LoadConfigDatabases() {
            // Load everything in the config dir
            Dictionary<RAthenaDbType,List<IDatabase>> retDbMap = await LoadDatabasesFromDirectory(
                Globals.RunConfig.YamlDbDirectoryPath);

            // Then load databases from the AdditionalDbPaths config list
            foreach (string filePath in Globals.RunConfig.AdditionalDbPaths) {
                IDatabase db = await LoadDatabaseFromFile(filePath);
                AddDbToMap(ref retDbMap, ref db);
            }
            return retDbMap;
        }

        public static async Task<IDatabase> LoadDatabaseFromFile(string filePath) {
            if (!File.Exists(filePath)) {
                await Logger.WriteLine($"Loading db at {filePath} failed. File not found.", LogLevel.Warning);
                return new UndefinedDatabase();
            }
            await Logger.WriteLine($"{filePath}: Loading database...", LogLevel.Debug);
            IDatabase retDb = await ParseDatabaseFromFile(filePath);
            // we're gonna return the DB either way, but logger will tell us if it's supported or not
            if (retDb.DatabaseType == RAthenaDbType.UNSUPPORTED) {
                await Logger.WriteLine($"{filePath}: Database type is unsupported.", LogLevel.Debug);
            } else {
                await Logger.WriteLine($"{filePath}: Database loaded into type: {retDb.DatabaseType}");
            }
            return retDb;
        }

        public static async Task<Dictionary<RAthenaDbType, List<IDatabase>>> LoadDatabasesFromDirectory(string directoryPath) {
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
                AddDbToMap(ref retDbMap, ref db);
            }
            return retDbMap;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Shortcut to safelty adding elements to a database map. Doesn't load unsupported types, guards against null keys, 
        /// and reloads DBs if one at the same file path was already loaded.
        /// </summary>
        private static void AddDbToMap(ref Dictionary<RAthenaDbType, List<IDatabase>> map, ref IDatabase db) {
            if (db.DatabaseType == RAthenaDbType.UNSUPPORTED) {
                return;
            }
            // Guard against empty lists
            if (!map.ContainsKey(db.DatabaseType)) {
                map[db.DatabaseType] = new();
            }
            // "Reload" the database if it's already present
            foreach  (IDatabase existingDb in map[db.DatabaseType]) {
                if (db.FilePath == existingDb.FilePath) {
                    map[db.DatabaseType].Remove(existingDb);
                }
            }
            map[db.DatabaseType].Add(db);
        }

        /// <summary>
        /// Input: rAthena YAML database |
        /// Output: KouCoCoa IDatabase |
        /// Will return an undefined database if the rA db type isn't supported by the parser.
        /// Start here if you want to add support for a new database type.
        /// </summary>
        private static async Task<IDatabase> ParseDatabase(dynamic inputDb) {
            RAthenaDbType dbType = await DetermineRAthenaDbType(inputDb);

            switch (dbType) {
                case RAthenaDbType.MOB_DB:
                    MobDatabase mobDb = new();
                    mobDb.Mobs = await DeserializeMobDb(inputDb);
                    return new MobDatabase(mobDb);
                case RAthenaDbType.ITEM_DB:
                    return new ItemDatabase();
                default:
                    return new UndefinedDatabase();
            }
        }

        private static async Task<IDatabase> ParseDatabaseFromFile(string filePath) {
            IDatabase retDb = new UndefinedDatabase();
            string fileExt = Path.GetExtension(filePath);
            if (fileExt != ".yml") {
                await Logger.WriteLine($"{filePath}: Attempted to parse database but file type was unknown. Right now only .yml files are supported.", LogLevel.Warning);
                return retDb;
            }
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            IDeserializer yamlDeserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            // inputDb acts as a temporary object while we determine the structure of the supposed database
            ExpandoObject inputDb;
            try {
                using (var yamlString = File.ReadAllTextAsync(filePath)) {
                    await Logger.WriteLine($"{filePath}: Identified as a YAML file, deserializing into ExpandoObject.", LogLevel.Debug);
                    inputDb = yamlDeserializer.Deserialize<ExpandoObject>(await yamlString);
                }
            } catch (Exception) {
                throw;
            }

            retDb = await ParseDatabase(inputDb);
            retDb.FilePath = filePath;
            retDb.Name = fileName;

            return retDb;
        }

        /// <summary>
        /// Check an ExpandoObject to determine what type of database it should deserialize into.
        /// </summary>
        private static async Task<RAthenaDbType> DetermineRAthenaDbType(dynamic inputDb) {
            if (!((IDictionary<string, object>)inputDb).ContainsKey("Header")) {
                await Logger.WriteLine($"No Header found in database, file is unsupported.", LogLevel.Debug);
                return RAthenaDbType.UNSUPPORTED;
            }

            foreach (KeyValuePair<object, object> entry in inputDb.Header) {
                if (entry.Key.ToString() == "Type") {
                    RAthenaDbType dbType = RAthenaDbType.UNSUPPORTED;
                    if (Enum.TryParse(entry.Value.ToString(), out dbType)) {
                        await Logger.WriteLine($"Database identified as supported type: {dbType}", LogLevel.Debug);
                        return dbType;
                    } else {
                        await Logger.WriteLine($"Database identified as unsupported type \"{entry.Value}\". " +
                            $"An undefined database will be returned, and loading will usually be skipped.", LogLevel.Debug);
                        return RAthenaDbType.UNSUPPORTED;
                    }
                }
            }

            await Logger.WriteLine($"Header was found for database, but no \"Type\" node was present in the header. File is unsupported.", LogLevel.Debug);
            return RAthenaDbType.UNSUPPORTED;
        }

        private static async Task<List<Mob>> DeserializeMobDb(dynamic mobDb) {
            List<Mob> retList = new();
            if (!((IDictionary<string, object>)mobDb).ContainsKey("Body")) {
                await Logger.WriteLine($"MobDB found, but no Body object is defined for this database. Returning empty Mob List.", LogLevel.Warning);
                return retList;
            }

            List<dynamic> mobEntries = mobDb.Body;

            // Oh god plz spare me from ever having to look at this again
            foreach (var mobEntry in mobEntries) {
                /* 
                 * The block below uses reflection to get all the public properties of a mob object, then checks
                 * to see if a key with the same name as the property exists in our mobEntry dynamic node. 
                 * If it does, it attempts to parse the value of the key to the appropriate type and assign it to a mob object.
                 */
                // First create a new mob object, which will get added to our retList
                Mob mob = new();
                // Get all public properties of a mob object
                foreach (PropertyInfo propertyInfo in mob.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
                    string propName = propertyInfo.Name;
                    if (mobEntry.ContainsKey(propName)) {
                        // val will morph to become our final value for this property
                        var val = mobEntry[propName];
                        // Parse for different type conversions
                        if (propertyInfo.PropertyType == typeof(int)) {
                            val = Int32.Parse(mobEntry[propName]);
                        }
                        if (propertyInfo.PropertyType == typeof(bool)) {
                            val = bool.Parse(mobEntry[propName]);
                        }
                        // Because MobModes and MobDrop are their own types, we have to go another level deep in reflection, repeating the steps above.
                        // MobModes
                        if (propertyInfo.PropertyType == typeof(MobModes)) {
                            MobModes modes = new();
                            foreach (PropertyInfo modePropInfo in mob.Modes.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
                                string modePropName = modePropInfo.Name;
                                if (val.ContainsKey(modePropName)) {
                                    var modeVal = val[modePropName];
                                    if (modePropInfo.PropertyType == typeof(bool)) {
                                        modeVal = bool.Parse(val[modePropName]);
                                    }
                                    modes.GetType().GetProperty(modePropName).SetValue(modes, modeVal);
                                }
                            }
                            val = new MobModes(modes);
                        }
                        // MobDrops
                        if (propertyInfo.PropertyType == typeof(List<MobDrop>)) {
                            List<MobDrop> mobDrops = new();
                            foreach (var dropsVal in val) {
                                MobDrop mobDrop = new MobDrop();
                                foreach (PropertyInfo dropPropInfo in mobDrop.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
                                    string dropPropName = dropPropInfo.Name;
                                    if (dropsVal.ContainsKey(dropPropName)) {
                                        var dropMemberVal = dropsVal[dropPropName];
                                        if (dropPropInfo.PropertyType == typeof(int)) {
                                            dropMemberVal = Int32.Parse(dropsVal[dropPropName]);
                                        }
                                        if (dropPropInfo.PropertyType == typeof(bool)) {
                                            dropMemberVal = bool.Parse(dropsVal[dropPropName]);
                                        }
                                        mobDrop.GetType().GetProperty(dropPropName).SetValue(mobDrop, dropMemberVal);
                                    }
                                }
                                mobDrops.Add(mobDrop);
                            }
                            val = new List<MobDrop>(mobDrops);
                        }
                        mob.GetType().GetProperty(propertyInfo.Name).SetValue(mob, val);
                    }
                }
                retList.Add(mob);
            }
            await Logger.WriteLine($"Found {retList.Count} mobs in this mob database.", LogLevel.Debug);
            return retList;
        }
        #endregion
    }
}
