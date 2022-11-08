using System.Collections.Generic;

namespace KouCoCoa {
    public class MobDatabase : IDatabase {
        #region Default Constructor
        public MobDatabase() {
            Name = "MobDb";
            FilePath = "Unknown";
        }
        #endregion

        #region IDatabase Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        #endregion

        #region Properties
        public List<Mob> Mobs;
        #endregion
    }

    internal class ItemDatabase : IDatabase {
        #region Default Constructor
        internal ItemDatabase() {
            Name = "ItemDb";
            FilePath = "Unknown";
        }
        #endregion

        #region IDatabase Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        #endregion
    }
}
