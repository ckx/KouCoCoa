
namespace KouCoCoa
{
    /// <summary>
    /// A type that says "we found a viable database here, but we don't have a class to deserialize it into"
    /// </summary>
    internal class UndefinedDatabase : IDatabase
    {
        #region Default Constructor
        public UndefinedDatabase()
        {
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
