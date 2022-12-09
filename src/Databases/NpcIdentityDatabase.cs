using System.Collections.Generic;

namespace KouCoCoa
{
    /// <summary>
    /// npc_identity.lub
    /// </summary>
    internal class NpcIdentityDatabase : IDatabase
    {
        #region Constructors
        public NpcIdentityDatabase()
        {
            Name = "NpcIdentityDb";
            FilePath = "Unknown";
            DatabaseType = RAthenaDbType.NPC_IDENTITY;
            Identities = new();
        }

        public NpcIdentityDatabase(NpcIdentityDatabase baseNpcIdDb)
        {
            Name = baseNpcIdDb.Name;
            FilePath = baseNpcIdDb.FilePath;
            DatabaseType = baseNpcIdDb.DatabaseType;
            Identities = new(baseNpcIdDb.Identities);
        }
        #endregion

        #region IDatabase Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RAthenaDbType DatabaseType { get; private set; }
        #endregion

        #region Properties
        public Dictionary<int, string> Identities { get; set; }
        #endregion
    }
}
