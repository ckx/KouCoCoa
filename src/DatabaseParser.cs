using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
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
            ExpandoObject inputDb;
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

            List<dynamic> mobEntries = mobDb.Body;

            // Oh god plz spare me from ever having to look at this again
            foreach (var mobEntry in mobEntries) {
                // Create a new mob object, which will get added to our retList
                Mob mob = new();
                /* 
                 * The block below uses reflection to get all the public properties of a mob object, then checks
                 * to see if that a key with the same name as the property exists in our mobEntry dynamic node. 
                 * If it does, it attempts to parse the value of the key to an apporpriate type.
                 */
                foreach (PropertyInfo propertyInfo in mob.GetType().GetProperties(BindingFlags.Instance|BindingFlags.Public)) {
                    string propName = propertyInfo.Name;
                    if (mobEntry.ContainsKey(propName)) {
                        // Val will morph to become our final value for this property
                        var val = mobEntry[propName];
                        if (propertyInfo.PropertyType == typeof(int)) {
                            val = Int32.Parse(mobEntry[propName]);
                        }
                        if (propertyInfo.PropertyType == typeof(bool)) {
                            val = bool.Parse(mobEntry[propName]);
                        }
                        // Because MobModes and MobDrop are their own types, we have to go another level deep in reflection.
                        if (propertyInfo.PropertyType == typeof(MobModes)) {
                            MobModes modes = new();
                            foreach (PropertyInfo modePropInfo in mob.Modes.GetType().GetProperties(BindingFlags.Instance|BindingFlags.Public)) {
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
                        if (propertyInfo.PropertyType == typeof(List<MobDrop>)) {
                            List<MobDrop> mobDrops = new();
                            foreach (var dropsVal in val) {
                                MobDrop mobDrop = new MobDrop();
                                foreach (PropertyInfo dropPropInfo in mobDrop.GetType().GetProperties(BindingFlags.Instance|BindingFlags.Public)) {
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