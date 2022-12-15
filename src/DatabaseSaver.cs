using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;

namespace KouCoCoa
{
    internal static class DatabaseSaver
    {

        #region Private members
        private static readonly string _backupDir = "autobackup";
        #endregion

        #region Public Methods
        public static async Task SerializeDatabase(IDatabase db)
        {
            if (db.DatabaseType == RAthenaDbType.UNSUPPORTED) {
                await Logger.WriteLineAsync($"[{db.FilePath}] Attempt to serialize unsupported database. Skipping.", LogLevel.Warning);
                return;
            }
            if (File.Exists(db.FilePath) && Globals.RunConfig.AutoBackupDatabases) {
                await BackupDatabase(db.FilePath);
            }

            string fileExt = Path.GetExtension(db.FilePath);

            switch (fileExt) {
                case ".yml":
                    await SerializeYamlDatabase(db);
                    break;
                case ".txt":
                    await SerializeTxtDatabase(db);
                    break;
                default:
                    break;
            }

            await Logger.WriteLineAsync($"Attempting to save database {db.Name} of type {db.DatabaseType} to " +
                $"{db.FilePath}. . .", LogLevel.DebugVerbose);
        }

        public static async Task SerializeDatabase(List<IDatabase> dbList)
        {
            foreach (IDatabase db in dbList) {
                await SerializeDatabase(db);
            }
        }

        public static async Task BackupDatabase(string filePath)
        {
            if (!File.Exists(filePath)) {
                await Logger.WriteLineAsync($"Attempted to backup file at {filePath}, but it does not exist. Skipping backup.", LogLevel.DebugVerbose);
                return;
            }
            string backupFilePath = $"{_backupDir}\\{Path.GetFileName(filePath)}-{DateTime.Now:yyyy-MM-dd}-{DateTime.Now:HHmmss}";
            await Logger.WriteLineAsync($"Attempted to backup file {filePath} to {backupFilePath}. . .", LogLevel.DebugVerbose);
            try {
                File.Copy(filePath, $"{_backupDir}\\{filePath}-{Logger.Timestamp}");
            } catch (Exception ex) {
                await Logger.WriteLineAsync($"{backupFilePath}: Backup failed. Exception was thrown: {ex.Message}", LogLevel.Error);
            }
            await Logger.WriteLineAsync($"{backupFilePath}: Successfully backed up.");
            return;
        }
        #endregion

        #region Private members
        private static async Task SerializeYamlDatabase(IDatabase db)
        {
            ISerializer yamlSerializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults)
                .Build();


            string yamlString = string.Empty;
            RAthenaYamlDatabase yamlDb = new(db.DatabaseType);

            switch (db.DatabaseType) {
                case RAthenaDbType.MOB_DB:
                    MobDatabase mobDb = (MobDatabase)db;
                    yamlDb.Body = mobDb.Mobs;
                    try {
                        yamlString = yamlSerializer.Serialize(yamlDb);
                    } catch (Exception) {
                        throw;
                    }
                    break;
                default:
                    break;
            }

            await WriteDatabaseToFile(db.FilePath, yamlString);
        }

        private static async Task SerializeTxtDatabase(IDatabase db)
        {
            List<string> dbString = new();

            switch (db.DatabaseType) {
                case RAthenaDbType.MOB_SKILL_DB:
                    dbString = SerializeMobSkillDb((MobSkillDatabase)db);
                    break;
                case RAthenaDbType.NPC_IDENTITY:
                    break;
                default:
                    break;
            }
            await WriteDatabaseToFile(db.FilePath, dbString);
        }

        private static async Task WriteDatabaseToFile(string filePath, string fileContents)
        {
            try {
                await File.WriteAllTextAsync(filePath, fileContents);
            } catch (Exception ex) {
                await Logger.WriteLineAsync($"[{filePath}] Failed to serialize database. Failure was when trying to write to file. " +
                    $"Exception was thrown: {ex.Message}.", LogLevel.Error);
                // TODO: Logger UI element thing, remove throwing the exception
                throw;
            }
        }

        private static async Task WriteDatabaseToFile(string filePath, List<string> fileContents)
        {
            try {
                await File.WriteAllLinesAsync(filePath, fileContents);
            } catch (Exception ex) {
                await Logger.WriteLineAsync($"[{filePath}] Failed to serialize database. Failure was when trying to write to file. " +
                    $"Exception was thrown: {ex.Message}.", LogLevel.Error);
                // TODO: Logger UI element thing, remove throwing the exception
                throw;
            }
        }

        private static List<string> SerializeMobSkillDb(MobSkillDatabase db)
        {
            List<string> serializedDb = new();
            foreach (MobSkill skill in db.Skills) {
                // MobID,Dummy value (info only),State,SkillID,SkillLv,Rate,CastTime,Delay,Cancelable,
                // Target,Condition type,Condition value,val1,val2,val3,val4,val5,Emotion,Chat
                // 1011,Chonchon@NPC_RUN,attack,354,1,10000,0,3000,no,self,always,0,,0x81,,,,26,
                string entry =
                    $"{skill.MobId},{skill.MobName}@{skill.SkillName},{skill.State},{skill.SkillId},{skill.SkillLv}," +
                    $"{skill.Rate},{skill.CastTime},{skill.Delay},{skill.Cancelable},{skill.Target},{skill.ConditionType}," +
                    $"{skill.ConditionValue},{skill.Val1},{skill.Val2},{skill.Val3},{skill.Val4},{skill.Val5},{skill.Emotion}," +
                    $"{skill.Chat}";
                serializedDb.Add(entry);
            }


            return serializedDb;
        }
        #endregion
    }

    internal class RAthenaYamlDatabase
    {
        public RAthenaYamlDatabase(RAthenaDbType innerDbType)
        {
            Header = new();
            Header.Type = innerDbType;
        }

        public YamlHeader Header { get; set; }
        public dynamic Body { get; set; }
    }

    internal class YamlHeader
    {
        public YamlHeader()
        {
            Type = RAthenaDbType.UNSUPPORTED;
            Version = 3;
        }
        public RAthenaDbType Type { get; set; }
        public int Version { get; set; }
    }
}
