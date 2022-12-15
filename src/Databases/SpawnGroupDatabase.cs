using System.Collections.Generic;

namespace KouCoCoa
{
    internal class SpawnGroupDatabase : IDatabase
    {
        #region Constructors
        public SpawnGroupDatabase()
        {
            Name = "SpawnGroupDb";
            FilePath = "";
            DatabaseType = RAthenaDbType.SPAWNGROUP_DB;
            SpawnGroups = new();
        }

        public SpawnGroupDatabase(SpawnGroupDatabase baseSpawnGroupDb)
        {
            Name = new(baseSpawnGroupDb.Name);
            FilePath = new(baseSpawnGroupDb.FilePath);
            DatabaseType = baseSpawnGroupDb.DatabaseType;
            SpawnGroups = new(baseSpawnGroupDb.SpawnGroups);
        }
        #endregion

        #region IDatabase Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RAthenaDbType DatabaseType { get; private set; }
        #endregion

        #region Properties
        public List<SpawnGroup> SpawnGroups { get; set; }
        #endregion
    }
}
