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
    internal static class DatabaseParser {
        #region Public Methods
        public static async Task<IDatabase> ParseDatabaseFromFile(string filePath) {
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

            // inputDb acts as a temporary object while we determine the structure
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
        /// Input: rAthena YAML database |
        /// Output: KouCoCoa IDatabase |
        /// Will return an undefined database if the rA db type isn't supported by the parser.
        /// Start here if you want to add support for a new database type.
        /// </summary>
        public static async Task<IDatabase> ParseDatabase(dynamic inputDb) {
            RAthenaDbType dbType = await DetermineDatabaseType(inputDb);

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
        #endregion

        #region Private methods
        /// <summary>
        /// Check an ExpandoObject to determine what type of database it should deserialize into.
        /// </summary>
        private static async Task<RAthenaDbType> DetermineDatabaseType(dynamic inputDb) {
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
                            $"This type is unsupported and undefined by KouCoCoa, so an undefined database will be returned. " +
                            $"Undefined databases are usually skipped in loading.", LogLevel.Debug);
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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
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
                foreach (PropertyInfo propertyInfo in mob.GetType().GetProperties(BindingFlags.Instance|BindingFlags.Public)) {
                    string propName = propertyInfo.Name;
                    if (mobEntry.ContainsKey(propName)) {
                        // val will morph to become our final value for this property
                        var val = mobEntry[propName];
                        // Parse for different types
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
                        // MobDrops
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
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            await Logger.WriteLine($"Found {retList.Count} mobs in this mob database.", LogLevel.Debug);
            return retList;
        }
        #endregion
    }
}