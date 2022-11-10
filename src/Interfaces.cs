namespace KouCoCoa {
    public interface IDatabase {
        #region Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RAthenaDbType DatabaseType { get; }
        #endregion
    }
}
