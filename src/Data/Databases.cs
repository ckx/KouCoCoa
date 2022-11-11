using System.Collections.Generic;

namespace KouCoCoa {
    internal class MobDatabase : IDatabase {
        #region Constructors
        #region Default Constructor
        public MobDatabase() {
            Name = "MobDb";
            FilePath = "Unknown";
            DatabaseType = RAthenaDbType.MOB_DB;
        }
        #endregion

        #region Copy Constructor
        public MobDatabase(MobDatabase baseMobDb) {
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

    internal class ItemDatabase : IDatabase {
        #region Default Constructor
        public ItemDatabase() {
            Name = "ItemDb";
            FilePath = "Unknown";
            DatabaseType = RAthenaDbType.ITEM_DB;
        }
        #endregion

        #region IDatabase Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RAthenaDbType DatabaseType { get; private set; }
        #endregion

        #region Properties
        //public List<Item> Items { get; set; }
        #endregion
    }

    /// <summary>
    /// A type that says "we found a viable database here, but we don't know how to handle it".
    /// </summary>
    internal class UndefinedDatabase : IDatabase {
        #region Default Constructor
        public UndefinedDatabase() {
            Name = "UndefinedDb";
            FilePath = "Unknown";
            DatabaseType = RAthenaDbType.UNSUPPORTED;
        }
        #endregion

        #region IDatabase Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RAthenaDbType DatabaseType { get; private set; }
        #endregion
    }
}
