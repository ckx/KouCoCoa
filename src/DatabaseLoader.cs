using System;
using System.Collections.Generic;
using System.IO;
using System.Dynamic;
using System.Reflection;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Linq;

namespace KouCoCoa
{
    // TODO: add built-in reference databases (vanilla rA dbs)?
    internal static class DatabaseLoader {
        #region Private Fields
        private static readonly List<string> SupportedFileTypes = new() {
            ".yml", ".txt"
        };
        #endregion

        #region Public Methods
        /// <summary>
        /// (Re)loads all databases from the YamlDbDirectoryPath & AdditionDbPaths in the provided config.
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        public static Dictionary<RAthenaDbType, List<IDatabase>> LoadDatabasesFromConfig(Config conf) {
            // Load everything in the config dir
            Dictionary<RAthenaDbType,List<IDatabase>> retDbMap = LoadDatabasesFromDirectory(conf.YamlDbDirectoryPath);

            // Then load databases from the AdditionalDbPaths config list
            foreach (string filePath in conf.AdditionalDbPaths) {
                IDatabase db = LoadDatabaseFromFile(filePath);
                AddDbToMap(ref retDbMap, ref db);
            }
            return retDbMap;
        }

        /// <summary>
        /// Load a plain text database from file at filePath into an IDatabase.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IDatabase LoadDatabaseFromFile(string filePath) {
            if (!File.Exists(filePath)) {
                Logger.WriteLine($"Loading db at {filePath} failed. File not found.", LogLevel.Warning);
                return new UndefinedDatabase();
            }
            Logger.WriteLine($"{filePath}: Loading database...", LogLevel.Debug);
            IDatabase retDb = ParseDatabaseFromFile(filePath);
            return retDb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static Dictionary<RAthenaDbType, List<IDatabase>> LoadDatabasesFromDirectory(string directoryPath) {
            Dictionary<RAthenaDbType, List<IDatabase>> retDbMap = new();
            if (!Directory.Exists(directoryPath)) {
                Logger.WriteLine($"Cannot load databases at {directoryPath}. Directory not found. Returning empty DatabaseMap.", LogLevel.Warning);
                return retDbMap;
            }

            Logger.WriteLine($"Searching for databases in {directoryPath}...");
            string[] fileEntries = Directory.GetFiles(directoryPath);
            foreach (string filePath in fileEntries) {
                if (!CheckSupportedFileType(filePath)) {
                    continue;
                }
                Logger.WriteLine($"{filePath}: Potential database found.", LogLevel.Debug);
                IDatabase db = LoadDatabaseFromFile(filePath);
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
                Logger.WriteLine($"{db.FilePath}: Skipping unsupported database.", LogLevel.Debug);
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
            Logger.WriteLine($"{db.FilePath}: Database loaded.");
        }

        /// <summary>
        /// Input: rAthena YAML database |
        /// Output: KouCoCoa IDatabase |
        /// Will return an undefined database if the rA db type isn't supported by the parser.
        /// Start here if you want to add support for a new database type.
        /// </summary>
        private static IDatabase ParseDatabase(ExpandoObject inputDb) {
            RAthenaDbType dbType = DetermineRAthenaDbType(inputDb);

            switch (dbType) {
                case RAthenaDbType.MOB_DB:
                    MobDatabase mobDb = new();
                    mobDb.Mobs = ParseMobDb(inputDb);
                    return new MobDatabase(mobDb);
                case RAthenaDbType.ITEM_DB:
                    return new ItemDatabase();
                default:
                    return new UndefinedDatabase();
            }
        }

        private static IDatabase ParseDatabase(List<string> inputDb)
        {
            RAthenaDbType dbType = DetermineRAthenaDbType(inputDb);

            switch (dbType) {
                case RAthenaDbType.MOB_SKILL_DB:
                    MobSkillDatabase mobSkillDb = new();
                    mobSkillDb.Skills = ParseMobSkillDb(inputDb);
                    return new MobSkillDatabase(mobSkillDb);
                default:
                    return new UndefinedDatabase();
            }
        }

        /// <summary>
        /// Attempt to load a database from file at filePath
        /// </summary>
        private static IDatabase ParseDatabaseFromFile(string filePath) {
            IDatabase retDb = new UndefinedDatabase();
            if (!CheckSupportedFileType(filePath)) {
                return retDb;
            }

            string fileExt = Path.GetExtension(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            switch (fileExt) {
                case ".yml":
                    IDeserializer yamlDeserializer = new DeserializerBuilder()
                        .WithNamingConvention(PascalCaseNamingConvention.Instance)
                        .Build();

                    // inputDb acts as a temporary object while we determine the structure of the supposed database
                    ExpandoObject yamlDb = new();
                    try {
                        string yamlString = File.ReadAllText(filePath);
                        Logger.WriteLine($"{filePath}: Identified as a YAML file, attempting deserialization into ExpandoObject.", LogLevel.DebugVerbose);
                        yamlDb = yamlDeserializer.Deserialize<ExpandoObject>(yamlString);
                    } catch (Exception) {
                        throw;
                    }

                    retDb = ParseDatabase(yamlDb);
                    break;
                case ".txt":
                    List<string> txtDb = new();
                    try {
                        txtDb = File.ReadAllLines(filePath).ToList();
                        Logger.WriteLine($"{filePath}: Identified as TXT file, loaded into a List<string>", LogLevel.DebugVerbose);
                    } catch (Exception) {
                        throw;
                    }
                    retDb = ParseDatabase(txtDb);
                    break;
                default:
                    Logger.WriteLine($"The database {fileName}{fileExt} is unsupported.", LogLevel.Warning);
                    break;
            }

            retDb.FilePath = filePath;
            retDb.Name = fileName;

            return retDb;
        }

        /// <summary>
        /// Returns true if the file extension of a file at filePath is contained within SupportedFileTypes.
        /// </summary>
        private static bool CheckSupportedFileType(string filePath)
        {
            string fileExt = Path.GetExtension(filePath);
            if (SupportedFileTypes.Contains(fileExt)) {
                return true;
            }

            string fileTypesMsg = "";
            for (int i = 0; i < SupportedFileTypes.Count; i++) {
                if (i == SupportedFileTypes.Count - 1) {
                    fileTypesMsg += $"{SupportedFileTypes[i]}";
                }
                fileTypesMsg += $"{SupportedFileTypes[i]}, ";
            }
            Logger.WriteLine($"{filePath}: Attempted to parse file, but file's extension was unknown. " +
                $"Supported extensions:{fileTypesMsg}", LogLevel.Warning);
            return false;
        }

        /// <summary>
        /// Check an ExpandoObject to determine what type of database it should deserialize into.
        /// </summary>
        private static RAthenaDbType DetermineRAthenaDbType(ExpandoObject inputDb) {
            RAthenaDbType dbType = RAthenaDbType.UNSUPPORTED;

            var inputDbDict = (IDictionary<string, object>)inputDb;
            if (!inputDbDict.ContainsKey("Header")) {
                Logger.WriteLine($"No Header found in database, file is unsupported or malformed.", LogLevel.Debug);
                return RAthenaDbType.UNSUPPORTED;
            }
            var headerEntries = (IDictionary<object, object>)inputDbDict["Header"];

            foreach (KeyValuePair<object, object> entry in headerEntries) {
                if (entry.Key.ToString() == "Type") {
                    if (Enum.TryParse(entry.Value.ToString(), out dbType)) {
                        Logger.WriteLine($"Database identified as supported type: {dbType}", LogLevel.Debug);
                        return dbType;
                    } else {
                        Logger.WriteLine($"Database identified as unsupported type \"{entry.Value}\". " +
                            $"An undefined database will be returned, and loading will usually be skipped.", LogLevel.Debug);
                        return RAthenaDbType.UNSUPPORTED;
                    }
                }
            }

            Logger.WriteLine($"Header was found for database, but no \"Type\" node was present in the header. File is unsupported.", LogLevel.Debug);
            return RAthenaDbType.UNSUPPORTED;
        }

        /// <summary>
        /// Check a list of strings (lines) to determine what type of database we should parse it into.
        /// </summary>
        private static RAthenaDbType DetermineRAthenaDbType(List<string> inputDb)
        {
            RAthenaDbType dbType = RAthenaDbType.UNSUPPORTED;
            if (inputDb[0] == "// Mob Skill Database") {
                dbType = RAthenaDbType.MOB_SKILL_DB;
                Logger.WriteLine($"Database identified as supported type: {dbType}", LogLevel.Debug);
                return dbType;
            }

            Logger.WriteLine($"Database type is unsupported. " +
                             $"An undefined database will be returned, and loading will usually be skipped.", LogLevel.Debug);
            return RAthenaDbType.UNSUPPORTED;

        }

        private static List<MobSkill> ParseMobSkillDb(List<string> mobSkillDb)
        {
            List<MobSkill> retList = new();
            int expectedMinimumValueCount = 19;

            foreach (string skill in mobSkillDb) {
                if (skill.StartsWith("//") || skill.Length <= 0) {
                    // Skip comments & empty lines
                    continue;
                }
                string[] skillFields = skill.Split(",");
                // Awful, but no real way for me to enforce mob_skill_db field length, so this checks to see if we are operating on a line
                // with "probably correct" number of values.
                if (skillFields.Length != expectedMinimumValueCount) {
                    Logger.WriteLine($"Failed to parse line in mob_skill_db, skipping. Found {skillFields.Length} values, but expected at least {expectedMinimumValueCount}. " +
                        $"Line is:\n{skill}", LogLevel.Warning);
                    continue;
                }

                MobSkill mobSkill = new();

                // Property assignments of a MobSkill
                // MobID,Dummy value (info only),State,SkillID,SkillLv,Rate,CastTime,Delay,Cancelable,Target,Condition type,
                // Condition value,val1,val2,val3,val4,val5,Emotion,Chat
                mobSkill.MobId = int.TryParse(skillFields[0], out int id) ? id : 0;

                mobSkill.DummyValue = skillFields[1];
                string[] mobSkillInfo = mobSkill.DummyValue.Split("@");
                if (mobSkillInfo.Length == 2) {
                    // The DummyValue is probably the standard "mobName@skillName" format, so assign them:
                    mobSkill.MobName = mobSkillInfo[0];
                    mobSkill.SkillName = mobSkillInfo[1];
                }

                mobSkill.State = Enum.TryParse(skillFields[2], true, out MobState state) ? state : MobState.idle;
                mobSkill.SkillId = int.TryParse(skillFields[3], out int skillId) ? skillId : 0;
                mobSkill.SkillLv = int.TryParse(skillFields[4], out int skillLv) ? skillLv : 0;
                mobSkill.Rate = int.TryParse(skillFields[5], out int rate) ? rate : 0;
                mobSkill.CastTime = int.TryParse(skillFields[6], out int castTime) ? castTime : 0;
                mobSkill.Delay = int.TryParse(skillFields[7], out int delay) ? delay : 0;
                mobSkill.Cancelable = Enum.TryParse(skillFields[8], out MobSkillCancelable cancelable) ? cancelable : MobSkillCancelable.yes;
                mobSkill.Target = Enum.TryParse(skillFields[9], out MobSkillTarget target) ? target : MobSkillTarget.target;
                mobSkill.ConditionType = Enum.TryParse(skillFields[10], out MobSkillConditionType conditionType) ? conditionType : MobSkillConditionType.always;
                mobSkill.ConditionValue = skillFields[11];
                mobSkill.Val1 = skillFields[12];
                mobSkill.Val2 = skillFields[13];
                mobSkill.Val3 = skillFields[14];
                mobSkill.Val4 = skillFields[15];
                mobSkill.Val5 = skillFields[16];
                mobSkill.Emotion = skillFields[17];
                mobSkill.Chat = skillFields[18];

                MobSkill newMobSkill = new(mobSkill);
                retList.Add(newMobSkill);
            }

            return retList;
        }

        // TODO: move this away from dynamic typing/reflection and do a plain old property-by-property assign.
        private static List<Mob> ParseMobDb(dynamic mobDb) {
            List<Mob> retList = new();
            if (!((IDictionary<string, object>)mobDb).ContainsKey("Body")) {
                Logger.WriteLine($"MobDB found, but no Body object is defined for this database. Returning empty Mob List.", LogLevel.Warning);
                return retList;
            }

            List<dynamic> mobEntries = mobDb.Body;

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
            Logger.WriteLine($"Found {retList.Count} mobs in this mob database.", LogLevel.Debug);
            return retList;
        }
        #endregion
    }
}
