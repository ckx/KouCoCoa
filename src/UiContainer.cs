using ImGuiNET;
using System.Collections.Generic;

namespace KouCoCoa {
    /// <summary>
    /// A UiContainer holds many UiElements, and determines which should be displayed at any given time.
    /// </summary>
    internal class UiContainer {
        #region Constructors
        #region Default Constructor
        public UiContainer() {
        }
        #endregion

        /// <summary>
        /// Create a UiContainer with a collection of already existing databases, 
        /// usually used in application startup or a major refresh.
        /// </summary>
        public UiContainer(Dictionary<RAthenaDbType, List<IDatabase>> dbMap) {
            _databases = new(dbMap);
            if (dbMap.ContainsKey(RAthenaDbType.MOB_DB)) {
                List<MobDatabase> mobDbs = new();
                foreach (IDatabase mobDb in dbMap[RAthenaDbType.MOB_DB]) {
                    mobDbs.Add((MobDatabase)mobDb);
                }
                _mobDbEditor = new(mobDbs);
            }
        }
        #endregion

        #region Private member variables
        private Dictionary<RAthenaDbType, List<IDatabase>> _databases = new();
        private List<UiElement> _uiElements = new();
        #endregion

        #region tempdevstuff
        private MobDatabaseEditor _mobDbEditor;
        #endregion

        #region Public methods
        // TODO: sort this out
        public void Update() {
            ImGui.Begin("KouCoCoa");
            ImGui.Checkbox("Mob Database Editor", ref _mobDbEditor.Visible);
            if (_mobDbEditor.Visible) {
                _mobDbEditor.Update();
            }
            ImGui.End();
        }
        #endregion
    }
}
