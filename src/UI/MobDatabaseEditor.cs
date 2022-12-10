using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KouCoCoa
{
    internal partial class MobDatabaseEditor : Form
    {
        #region Constructor
        public MobDatabaseEditor(MobDatabase mobDb, MobSkillDatabase mobSkillDb, 
            NpcIdentityDatabase npcIdDb, Dictionary<string, Image> images)
        {
            _mobDb = new(mobDb);
            _mobSkillDb = new(mobSkillDb);
            _npcIdDb = new(npcIdDb);
            _images = images;
            InitializeComponent();
            KouCoCoaInitialization();
        }
        #endregion

        #region Private members
        private readonly MobDatabase _mobDb = new();
        private readonly MobSkillDatabase _mobSkillDb = new();
        private readonly NpcIdentityDatabase _npcIdDb = new();
        private readonly Dictionary<string, Image> _images = new();
        private readonly List<string> _mobNames = new();
        private readonly string _defaultMobImageKey = "koucocoa_transparent.png";
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            Text = $"{_mobDb.Name} :: Mob Database Editor";

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

        private void ShowMobSprite(int mobId)
        {
            string imgKey = _defaultMobImageKey;

            if (_npcIdDb.Identities.ContainsKey(mobId)) {
                string baseKey = $"{_npcIdDb.Identities[mobId]}.gif";
                imgKey = RemapSprite(baseKey);
            }
            if (!_images.ContainsKey(imgKey)) {
                Logger.WriteLine($"Mob image not found, key was: {imgKey}");
                imgKey = _defaultMobImageKey;
            }
            mobSpritePictureBox.Image = _images[imgKey];
            return;
        }

#if DEBUG
        private void ShowMobSpriteDebug(int mobId)
        {
            string imgKey = string.Empty;
            Image image;

            if (_npcIdDb.Identities.ContainsKey(mobId)) {
                string baseKey = $"{_npcIdDb.Identities[mobId]}.gif";
                imgKey = $"data/spritedata/{RemapSprite(baseKey)}";
            }

            if (File.Exists(imgKey)) {
                image = Image.FromFile(imgKey);
            } else if (File.Exists($"data/{_defaultMobImageKey}")) {
                imgKey = $"data/{_defaultMobImageKey}";
                image = Image.FromFile(imgKey);
            } else {
                image = mobSpritePictureBox.ErrorImage;
            }
            mobSpritePictureBox.Image = image;
        }
#endif

        private static string RemapSprite(string inputKey)
        {
            string outputKey = string.Empty;
            switch (inputKey) {
                case "CHONCHON.gif":
                    outputKey = "CHOCHO.gif";
                    break;
                default:
                    outputKey = new(inputKey);
                    break;
            }
            return outputKey;
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
            string selectedItem = mobListBox.SelectedItem.ToString();
            mobNameLabel.Text = selectedItem;

            // Find the mob we've selected
            Mob mob = new();
            int mobId = int.Parse(Utilities.StringSplit(selectedItem, '[', ']'));
            foreach (Mob entry in _mobDb.Mobs) {
                if (entry.Id == mobId) {
                    mob = entry;
                }
            }

#if DEBUG
            ShowMobSpriteDebug(mob.Id);
#else
            ShowMobSprite(mob.Id);
#endif

            // Populate the Skill List
            mobSkillList.BeginUpdate();
            mobSkillList.Items.Clear();
            foreach (MobSkill skill in mob.Skills) {
                mobSkillList.Items.Add($"[{skill.SkillId}] {skill.SkillName} (Lv. {skill.SkillLv})");
            }
            mobSkillList.EndUpdate();
        }
#endregion
    }
}
