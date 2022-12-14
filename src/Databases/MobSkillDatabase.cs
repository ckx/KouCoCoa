using System.Collections.Generic;

namespace KouCoCoa
{
    /// <summary>
    /// mob_skill_db.txt
    /// </summary>
    internal class MobSkillDatabase : IDatabase
    {
        #region Constructors
        public MobSkillDatabase()
        {
            Name = "MobSkillDb";
            FilePath = "Unknown";
            DatabaseType = RAthenaDbType.MOB_SKILL_DB;
            Skills = new();
        }

        // Copy constructor
        public MobSkillDatabase(MobSkillDatabase baseMobSkillDb)
        {
            Name = baseMobSkillDb.Name;
            FilePath = baseMobSkillDb.FilePath;
            DatabaseType = baseMobSkillDb.DatabaseType;
            Skills = new List<MobSkill>(baseMobSkillDb.Skills);
        }
        #endregion

        #region IDatabase Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RAthenaDbType DatabaseType { get; private set; }
        #endregion

        #region Properties
        public List<MobSkill> Skills { get; set; }
        #endregion
    }
}