namespace KouCoCoa {
    //internal interface IUiElement {
    //    #region Properties
    //    public bool Visible;
    //    #endregion

    //    #region Methods
    //    public void Update();
    //    #endregion
    //}

    internal interface IDatabase {
        #region Properties
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RAthenaDbType DatabaseType { get; }
        #endregion
    }
}
