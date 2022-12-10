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

            InitializeComboBoxValues();
        }

        private void InitializeComboBoxValues()
        {
            foreach (MobClass mobClass in Enum.GetValues(typeof(MobClass))) {
                mobClassComboBox.Items.Add(mobClass.ToString());
            }
            foreach (MobRace mobRace in Enum.GetValues(typeof(MobRace))) {
                mobRaceComboBox.Items.Add(mobRace.ToString());
            }
            foreach (MobSize mobSize in Enum.GetValues(typeof(MobSize))) {
                mobSizeComboBox.Items.Add(mobSize.ToString());
            }
            foreach (MobElement mobElement in Enum.GetValues(typeof(MobElement))) {
                mobElementComboBox.Items.Add(mobElement.ToString());
            }
            for (int i = 1; i < 5; i++) {
                mobEleLvlComboBox.Items.Add(i.ToString());
            }
        }

        /// <summary>
        /// Correlate mob skills with the mob
        /// </summary>
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

        /// <summary>
        /// Load sprite image from the image list DB
        /// </summary>
        private string ShowMobSprite(int mobId)
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
            return imgKey;
        }

#if DEBUG
        /// <summary>
        /// Load from directory instead of zip
        /// </summary>
        private string ShowMobSpriteDebug(int mobId)
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
            return imgKey;
        }
#endif

        /// <summary>
        /// Some sprites have names that don't match the image database, catching and resolving them 
        /// manually here because lazy
        /// </summary>
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

        /// <summary>
        /// Populate the Info TablePanelLayout of a mob
        /// </summary>
        private void ShowBasicMobInfo(Mob mob, string spriteId)
        {
            // Basic info
            mobFriendlyNameTextBox.Text = mob.Name;
            mobAegisNameTextBox.Text = mob.AegisName;
            mobJpNameTextBox.Text = mob.JapaneseName;
            mobIdTextBox.Text = mob.Id.ToString();
            mobSpriteIdTextBox.Text = spriteId;
            SetComboBoxIndex(mobClassComboBox, mob.Class.ToString());
            SetComboBoxIndex(mobRaceComboBox, mob.Race.ToString());
            SetComboBoxIndex(mobSizeComboBox, mob.Size.ToString());
            SetComboBoxIndex(mobElementComboBox, mob.Element.ToString());
            SetComboBoxIndex(mobEleLvlComboBox, mob.ElementLevel.ToString());
            mobBaseStatsStrTextBox.Text = mob.Str.ToString();
            mobBaseStatsIntTextBox.Text = mob.Int.ToString();
            mobBaseStatsAgiTextBox.Text = mob.Agi.ToString();
            mobBaseStatsDexTextBox.Text = mob.Dex.ToString();
            mobBaseStatsVitTextBox.Text = mob.Vit.ToString();
            mobBaseStatsLukTextBox.Text = mob.Luk.ToString();
            mobStatsHpTextBox.Text = mob.Hp.ToString();
            mobStatsLevelTextBox.Text = mob.Level.ToString();
            mobStatsAtkTextBox.Text = mob.Attack.ToString();
            mobStatsAtk2TextBox.Text = mob.Attack2.ToString();
            mobStatsDefTextBox.Text = mob.Defense.ToString();
            mobStatsMdefTextBox.Text = mob.MagicDefense.ToString();
            mobStatsAtkRangeTextBox.Text = mob.AttackRange.ToString();
            mobStatsSkillRangeTextBox.Text = mob.SkillRange.ToString();
            mobStatsChaseRangeTextbox.Text = mob.ChaseRange.ToString();
            mobStatsWalkSpeedTextBox.Text = mob.WalkSpeed.ToString();
            mobStatsAtkDelayTextBox.Text = mob.AttackDelay.ToString();
            mobStatsAtkMotionTextBox.Text = mob.AttackMotion.ToString();
            mobStatsDmgMotionTextBox.Text = mob.DamageMotion.ToString();

        }

        private void SetComboBoxIndex(ComboBox comboBox, string entry)
        {
            if (comboBox.Items.Contains(entry)) {
                int index = comboBox.Items.IndexOf(entry);
                comboBox.SelectedIndex = index;
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
            string spriteId;
#if DEBUG
            spriteId = ShowMobSpriteDebug(mob.Id);
#else
            spriteId = ShowMobSprite(mob.Id);
#endif
            ShowBasicMobInfo(mob, spriteId);

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
