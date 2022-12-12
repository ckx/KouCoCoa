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
            ISerializer yamlSerializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            await Logger.WriteLineAsync($"Attempting to save database {db.Name} of type {db.DatabaseType} to " +
                $"{db.FilePath}. . .", LogLevel.DebugVerbose);
            if (File.Exists(db.FilePath) && Globals.RunConfig.AutoBackupDatabases) {
                await BackupDatabase(db.FilePath);
            }

            string yamlString = string.Empty;

            switch (db.DatabaseType) {
                case RAthenaDbType.UNSUPPORTED:
                    break;
                case RAthenaDbType.MOB_DB:
                    MobDatabase mobDb = (MobDatabase)db;
                    try {
                        yamlString = yamlSerializer.Serialize(mobDb.Mobs);
                    } catch (Exception) {
                        throw;
                    }
                    break;
                case RAthenaDbType.MOB_AVAIL_DB:
                    break;
                case RAthenaDbType.ITEM_DB:
                    break;
                case RAthenaDbType.MOB_SKILL_DB:
                    break;
                case RAthenaDbType.NPC_IDENTITY:
                    break;
                default:
                    break;
            }

            try {
                await File.WriteAllTextAsync(db.FilePath, yamlString);
            } catch (Exception) {

                throw;
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
    }
}
