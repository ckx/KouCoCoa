using System.Collections.Generic;

namespace KouCoCoa
{
    /// <summary>
    /// MOB_DB
    /// </summary>
    internal class MobDatabase : IDatabase
    {
        #region Constructors
        public MobDatabase()
        {
            Name = "MobDb";
            FilePath = "";
            DatabaseType = RAthenaDbType.MOB_DB;
            Mobs = new();
        }

        // Copy constructor
        public MobDatabase(MobDatabase baseMobDb)
        {
            Name = new(baseMobDb.Name);
            FilePath = new(baseMobDb.FilePath);
            DatabaseType = baseMobDb.DatabaseType;
            Mobs = baseMobDb.Mobs;
        }
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
