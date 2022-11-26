namespace KouCoCoa
{
    /// <summary>
    /// ITEM_DB
    /// </summary>
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
}
