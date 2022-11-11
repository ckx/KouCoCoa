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
            _databases = new();
        }
        #endregion

        /// <summary>
        /// Create a UiContainer with a collection of already existing databases, 
        /// usually used in application startup or a major refresh.
        /// </summary>
        public UiContainer(Dictionary<RAthenaDbType, List<IDatabase>> dbMap) {
            _databases = new(dbMap);
        }
        #endregion

        #region Private member variables
        private Dictionary<RAthenaDbType, List<IDatabase>> _databases;
        #endregion

        #region Public methods
        public void Update() {
            ImGui.ShowDemoWindow();
        }
        #endregion
    }
}
