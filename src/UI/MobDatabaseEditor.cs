using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KouCoCoa
{
    internal partial class MobDatabaseEditor : Form
    {
        #region Constructor
        public MobDatabaseEditor(MobDatabase mobDb, MobSkillDatabase mobSkillDb)
        {
            _mobDb = new(mobDb);
            _mobSkillDb = new(mobSkillDb);
            InitializeComponent();
            KouCoCoaInitialization();
        }
        #endregion

        #region Private members
        private readonly MobDatabase _mobDb = new();
        private readonly MobSkillDatabase _mobSkillDb = new();
        private readonly List<string> _mobNames = new();
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            this.Text = $"{_mobDb.Name} :: Mob Database Editor";

            // Event subscriptions
            mobListBox.SelectedValueChanged += mobListBox_SelectedValueChanged;
            mobFilterBox.TextChanged += mobFilterBox_TextChanged;

            AssignMobSkillsToMobs();

            // Populate the left-docked mobList 
            ListBox.ObjectCollection mobListBoxCollection = new(mobListBox);
            foreach (Mob mob in _mobDb.Mobs) {
                string mobName = $"[{mob.Id}] {mob.Name} ({mob.AegisName})";
                _mobNames.Add(mobName);
            }
            mobListBoxCollection.AddRange(_mobNames.Cast<object>().ToArray());
            mobListBox.Items.AddRange(mobListBoxCollection);
        }

        private void AssignMobSkillsToMobs()
        {
            foreach (MobSkill skill in _mobSkillDb.Skills) {
                foreach (Mob mob in _mobDb.Mobs) {
                    if (skill.MobId == mob.Id) {
                        mob.Skills.Add(skill);
                        break;
                    }
                }
            }
        }
        #endregion

        #region Event Handlers
        private void mobFilterBox_TextChanged(object sender, EventArgs e)
        {
            List<string> mobFilterResults = new(_mobNames);
            mobListBox.BeginUpdate();
            mobListBox.Items.Clear();
            if (!string.IsNullOrEmpty(mobFilterBox.Text)) {
                foreach (string name in mobFilterResults) {
                    if (name.Contains(mobFilterBox.Text, StringComparison.CurrentCultureIgnoreCase)) {
                        mobListBox.Items.Add(name);
                    }
                }
            } else {
                mobListBox.Items.AddRange(mobFilterResults.Cast<object>().ToArray());
            }
            mobListBox.EndUpdate();
        }

        private void mobListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            // Find the mob we've selected
            Mob selectedMob = new();
            int mobId = int.Parse(Utilities.StringSplit(mobListBox.SelectedItem.ToString(), '[', ']'));
            foreach (Mob mob in _mobDb.Mobs) {
                if (mob.Id == mobId) {
                    selectedMob = mob;
                }
            }

            // Populate the Skill List
            mobSkillList.BeginUpdate();
            mobSkillList.Items.Clear();
            foreach (MobSkill skill in selectedMob.Skills) {
                mobSkillList.Items.Add($"[{skill.SkillId}] {skill.SkillName} (Lv. {skill.SkillLv})");
            }
            mobSkillList.EndUpdate();
        }
        #endregion
    }
}
