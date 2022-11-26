using System.Collections.Generic;

namespace KouCoCoa
{
    /// <summary>
    /// MOB_DB
    /// </summary>
    internal class MobDatabase : IDatabase
    {
        #region Constructors
        #region Default Constructor
        public MobDatabase()
        {
            Name = "MobDb";
            FilePath = "Unknown";
            DatabaseType = RAthenaDbType.MOB_DB;
            Mobs = new();
        }
        #endregion

        #region Copy Constructor
        public MobDatabase(MobDatabase baseMobDb)
        {
            Name = baseMobDb.Name;
            FilePath = baseMobDb.FilePath;
            DatabaseType = baseMobDb.DatabaseType;
            Mobs = baseMobDb.Mobs;
        }
        #endregion
        #endregion

        #region IDatabase Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RAthenaDbType DatabaseType { get; private set; }
        #endregion

        #region Properties
        public List<Mob> Mobs { get; set; }
        #endregion
    }
}
