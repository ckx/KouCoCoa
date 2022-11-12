using System.Collections.Generic;
using System.Reflection;
using ImGuiNET;

namespace KouCoCoa {
    internal class MobDatabaseEditor : UiElement {
        #region Constructors
        public MobDatabaseEditor(List<MobDatabase> mobDbs) {
            _mobDbs = mobDbs;
        }
        #endregion
        #region Private member variables
        private List<MobDatabase> _mobDbs = new();
        #endregion

        #region UiElement Methods
        public override void Update() {
            ImGui.Begin("Mob Database Editor");
            foreach (MobDatabase mobDb in _mobDbs) {
                if (ImGui.TreeNode(mobDb.Name)) {
                    foreach (Mob mob in mobDb.Mobs) {
                        if (ImGui.TreeNode($"[{mob.Id}] {mob.Name}")) {
                            DisplayMobProperties(mob);
                            ImGui.TreePop();
                        }
                    }
                    ImGui.TreePop();
                }
                ImGui.Separator();
            }
            ImGui.End();
        }
        #endregion

        #region Private methods
        private void DisplayMobProperties(Mob mob) {
            if (mob == null) {
                return;
            }

            foreach (PropertyInfo propertyInfo in mob.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
                ImGui.Text($"{propertyInfo.Name}: {mob.GetType().GetProperty(propertyInfo.Name).GetValue(mob)}");
            }
        }
        #endregion
    }
}
