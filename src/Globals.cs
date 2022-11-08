namespace KouCoCoa {
    internal class Globals {
        /// <summary>
        /// Global config for simplicity
        /// </summary>
        public static Config RunConfig = new();

#if DEBUG
        public static bool SilenceLogger = false;
#endif
    }
}
