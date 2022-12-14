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
        private readonly List<CheckBox> _mobModeCheckBoxes = new();
        private readonly Dictionary<string, string> _aegisAiModes = new();
        private readonly List<SkillEditor> _openSkillEditors = new();
        private readonly string _defaultMobImageKey = "koucocoa_transparent.png";
        private Mob _selectedMob;
        private readonly ContextMenuStrip mobListContextMenuStrip = new();
        private readonly ContextMenuStrip skillListContextMenuStrip = new();
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            Text = $"{_mobDb.Name} :: {Name}";
            // Event subscriptions
            mobListBox.SelectedValueChanged += mobListBox_SelectedValueChanged;
            mobFilterBox.TextChanged += mobFilterBox_TextChanged;

            AssignMobSkillsToMobs();
            mobSkillList.MouseDoubleClick += mobSkillList_MouseDoubleClick;
            mobSkillList.MouseDown += mobSkillList_MouseDown;

            // Populate the left-docked mobList 
            ListBox.ObjectCollection mobListBoxCollection = new(mobListBox);
            List<string> mobNames = new();
            foreach (Mob mob in _mobDb.Mobs) {
                mobNames.Add(MobToListEntry(mob));
            }
            mobListBoxCollection.AddRange(mobNames.Cast<object>().ToArray());
            mobListBox.Items.AddRange(mobListBoxCollection);
            mobListBox.MouseDown += mobListBox_MouseDown;
            InitializeUpDownControls();
            InitializeComboBoxValues();
            InitializeCheckBoxes();
        }

        private void mobListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                mobListContextMenuStrip.Items.Clear();
                mobListBox.SelectedIndex = mobListBox.IndexFromPoint(e.Location);
                if (mobListBox.SelectedIndex != -1) {
                    ToolStripMenuItem addMob = new("Add new mob...");
                    addMob.Click += delegate (object sender, EventArgs e) { AddNewMob_Event(sender, e, null); };
                    ToolStripMenuItem duplicateMob = new($"Duplicate '{_selectedMob.AegisName}...'");
                    duplicateMob.Click += delegate (object sender, EventArgs e) { AddNewMob_Event(sender, e, _selectedMob); };
                    ToolStripMenuItem deleteMob = new($"Delete '{_selectedMob.AegisName}...'");
                    deleteMob.Click += delegate (object sender, EventArgs e) { DeleteMob_Event(sender, e); };
                    mobListContextMenuStrip.Items.Add(addMob);
                    mobListContextMenuStrip.Items.Add(duplicateMob);
                    mobListContextMenuStrip.Items.Add(deleteMob);
                    mobListContextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void mobSkillList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                skillListContextMenuStrip.Items.Clear();
                mobSkillList.SelectedIndex = mobSkillList.IndexFromPoint(e.Location);
                ToolStripMenuItem addSkill = new("Add new skill...");
                addSkill.Click += delegate (object sender, EventArgs e) { AddNewSkill_Event(sender, e); };
                skillListContextMenuStrip.Items.Add(addSkill);
                if (mobSkillList.SelectedIndex != -1) {
                    ToolStripMenuItem duplicateSkill = new($"Duplicate '{mobSkillList.SelectedItem}'...");
                    ToolStripMenuItem deleteSkill = new($"Delete '{mobSkillList.SelectedItem}'...");
                    skillListContextMenuStrip.Items.Add(duplicateSkill);
                    skillListContextMenuStrip.Items.Add(deleteSkill);
                }
                skillListContextMenuStrip.Show(Cursor.Position);
            }
        }

        private void InitializeUpDownControls()
        {
            List<NumericUpDown> upDowns = new() {
                mobBaseStatsStrUpDown,
                mobBaseStatsIntUpDown,
                mobBaseStatsAgiUpDown,
                mobBaseStatsDexUpDown,
                mobBaseStatsVitUpDown,
                mobBaseStatsLukUpDown,
                mobIdNumericUpDown,
                mobStatsHpUpDown,
                mobStatsLevelUpDown,
                mobStatsAttackUpDown,
                mobStatsAttack2UpDown,
                mobStatsDefenseUpDown,
                mobStatsMdefUpDown,
                mobStatsAtkRangeUpDown,
                mobStatsSkillRangeUpDown,
                mobStatsChaseRangeUpDown,
                mobStatsMoveSpeedUpDown,
                mobStatsAtkDelayUpDown,
                mobStatsAtkMotionUpDown,
                mobStatsDmgMotionUpDown
            };
            foreach (NumericUpDown upDown in upDowns) {
                upDown.Minimum = 0;
                upDown.Maximum = int.MaxValue;
            }
        }

        /// <summary>
        /// Set all possible values for comboboxes
        /// </summary>
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

            _aegisAiModes.Add("01", "Passive");
            _aegisAiModes.Add("02", "Passive | Looter");
            _aegisAiModes.Add("03", "Passive | Assist | Change target Melee");
            _aegisAiModes.Add("04", "Angry | Change target melee | Change target chase");
            _aegisAiModes.Add("05", "Aggressive | Change target chase");
            _aegisAiModes.Add("06", "Passive | Immobile | Can't Attack");
            _aegisAiModes.Add("07", "Passive | Looter | Assist | Change target Melee");
            _aegisAiModes.Add("08", "Aggressive | Change target melee | Change target chase | Target weak");
            _aegisAiModes.Add("09", "Aggressive | Change target melee | Change target chase | Cast sense idle");
            _aegisAiModes.Add("10", "Aggressive | Immobile");
            _aegisAiModes.Add("11", "Aggressive | Immobile [Guardian]");
            _aegisAiModes.Add("12", "Aggressive | Charge target chase");
            _aegisAiModes.Add("13", "Aggressive Change target melee | Change target chase | Assist");
            _aegisAiModes.Add("17", "Passive | Case sensor idle");
            _aegisAiModes.Add("19", "Aggressive | Change target melee | Change target chase | Cast sense idle");
            _aegisAiModes.Add("20", "Aggressive | Change target melee | Change target chase | Cast sense idle | Cast sense chase");
            _aegisAiModes.Add("21", "Aggressive | Change target melee+chase | Cast sense idle | Cast sense chase | Chase change target");
            _aegisAiModes.Add("24", "Passive | No random walk");
            _aegisAiModes.Add("25", "Passive | Can't attack");
            _aegisAiModes.Add("26", "Aggressive | Change target melee | Change target chase | Case sense idle | Cast sense chase | Change chase target | Random target");
            _aegisAiModes.Add("27", "Aggressive | Immobile | Random target");

            foreach (KeyValuePair<string, string> aegisModes in _aegisAiModes) {
                mobAegisAiComboBox.Items.Add($"{aegisModes.Key} - {aegisModes.Value}");
            }
        }

        /// <summary>
        /// Adds all checkboxes to _mobModeCheckBoxes for usage in the AI Summary
        /// </summary>
        private void InitializeCheckBoxes()
        {
            _mobModeCheckBoxes.Add(mobModesCanMoveCheckBox);
            _mobModeCheckBoxes.Add(mobModesCanAttackCheckBox);
            _mobModeCheckBoxes.Add(mobModesNoRandomWalkCheckBox);
            _mobModeCheckBoxes.Add(mobModesNoCastCheckBox);
            _mobModeCheckBoxes.Add(mobModesDetectorCheckBox);
            _mobModeCheckBoxes.Add(mobModesLooterCheckBox);
            _mobModeCheckBoxes.Add(mobModesAggressiveCheckBox);
            _mobModeCheckBoxes.Add(mobModesAssistAggroCheckBox);
            _mobModeCheckBoxes.Add(mobModesCastSensorIdleCheckBox);
            _mobModeCheckBoxes.Add(mobModesCastSensorChaseCheckBox);
            _mobModeCheckBoxes.Add(mobModesRandomTargetCheckBox);
            _mobModeCheckBoxes.Add(mobModesTargetWeakCheckBox);
            _mobModeCheckBoxes.Add(mobModesAngryCheckBox);
            _mobModeCheckBoxes.Add(mobModesMvpCheckBox);
            _mobModeCheckBoxes.Add(mobModesIgnoreMeleeCheckBox);
            _mobModeCheckBoxes.Add(mobModesIgnoreRangedCheckBox);
            _mobModeCheckBoxes.Add(mobModesIgnoreMagicCheckBox);
            _mobModeCheckBoxes.Add(mobModesStatusImmuneCheckBox);
            _mobModeCheckBoxes.Add(mobModesSkillImmuneCheckBox);
            _mobModeCheckBoxes.Add(mobModesKnockbackImmuneCheckBox);
            _mobModeCheckBoxes.Add(mobModesFixedItemDropCheckBox);
            _mobModeCheckBoxes.Add(mobModesIgnoreMiscCheckBox);
            _mobModeCheckBoxes.Add(mobModesTeleportBlockCheckBox);
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

        private void PopulateSkills()
        {
            mobSkillList.BeginUpdate();
            mobSkillList.Items.Clear();
            foreach (MobSkill skill in _selectedMob.Skills) {
                mobSkillList.Items.Add(SkillToListEntry(skill));
            }
            mobSkillList.EndUpdate();
        }

        private void mobSkillList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int skillIndex = mobSkillList.SelectedIndex;
            if (skillIndex > _selectedMob.Skills.Count || skillIndex == -1) {
                return;
            }
            MobSkill skill = _selectedMob.Skills[skillIndex];
            string skillLine = $"{skill.MobName} :: [{skill.State}] {skill.SkillName} {skill.SkillLv}";
            foreach (SkillEditor openSkillEditor in _openSkillEditors) {
                if (openSkillEditor.Text.Contains(skillLine)) {
                    openSkillEditor.Focus();
                    return;
                }
            }

            SkillEditor skillEditor = new(skill);
            skillEditor.TopLevel = false;
            skillEditor.MdiParent = MdiParent;
            skillEditor.FormClosed += skillEditor_Closed;
            _openSkillEditors.Add(skillEditor);
            skillEditor.Show();
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
            mobIdNumericUpDown.Value = mob.Id;
            mobSpriteIdTextBox.Text = spriteId;
            SetAiComboBoxIndex(mobAegisAiComboBox, mob.Ai.ToString());
            Utilities.SetComboBoxIndex(mobClassComboBox, mob.Class.ToString());
            Utilities.SetComboBoxIndex(mobRaceComboBox, mob.Race.ToString());
            Utilities.SetComboBoxIndex(mobSizeComboBox, mob.Size.ToString());
            Utilities.SetComboBoxIndex(mobElementComboBox, mob.Element.ToString());
            Utilities.SetComboBoxIndex(mobEleLvlComboBox, mob.ElementLevel.ToString());
            mobBaseStatsStrUpDown.Value = mob.Str;
            mobBaseStatsIntUpDown.Value = mob.Int;
            mobBaseStatsAgiUpDown.Value = mob.Agi;
            mobBaseStatsDexUpDown.Value = mob.Dex;
            mobBaseStatsVitUpDown.Value = mob.Vit;
            mobBaseStatsLukUpDown.Value = mob.Luk;
            mobStatsHpUpDown.Value = mob.Hp;
            mobStatsLevelUpDown.Value = mob.Level;
            mobStatsAttackUpDown.Value = mob.Attack;
            mobStatsAttack2UpDown.Value = mob.Attack2;
            mobStatsDefenseUpDown.Value = mob.Defense;
            mobStatsMdefUpDown.Value = mob.MagicDefense;
            mobStatsAtkRangeUpDown.Value = mob.AttackRange;
            mobStatsSkillRangeUpDown.Value = mob.SkillRange;
            mobStatsChaseRangeUpDown.Value = mob.ChaseRange;
            mobStatsMoveSpeedUpDown.Value = mob.WalkSpeed;
            mobStatsAtkDelayUpDown.Value = mob.AttackDelay;
            mobStatsAtkMotionUpDown.Value = mob.AttackMotion;
            mobStatsDmgMotionUpDown.Value = mob.DamageMotion;

            // rA Mode Checkboxes
            mobModesCanMoveCheckBox.Checked = mob.Modes.CanMove;
            mobModesCanAttackCheckBox.Checked = mob.Modes.CanAttack;
            mobModesNoRandomWalkCheckBox.Checked = mob.Modes.NoRandomWalk;
            mobModesNoCastCheckBox.Checked = mob.Modes.NoCast;
            mobModesDetectorCheckBox.Checked = mob.Modes.Detector;
            mobModesLooterCheckBox.Checked = mob.Modes.Looter;
            mobModesAggressiveCheckBox.Checked = mob.Modes.Aggressive;
            mobModesAssistAggroCheckBox.Checked = mob.Modes.Assist;
            mobModesCastSensorIdleCheckBox.Checked = mob.Modes.CastSensorIdle;
            mobModesCastSensorChaseCheckBox.Checked = mob.Modes.CastSensorChase;
            mobModesChangeTargetMelee.Checked = mob.Modes.ChangeTargetMelee;
            mobModesChangeTargetChase.Checked = mob.Modes.ChangeTargetChase;
            mobModesRandomTargetCheckBox.Checked = mob.Modes.RandomTarget;
            mobModesTargetWeakCheckBox.Checked = mob.Modes.TargetWeak;
            mobModesAngryCheckBox.Checked = mob.Modes.Angry;
            mobModesMvpCheckBox.Checked = mob.Modes.Mvp;
            mobModesIgnoreMeleeCheckBox.Checked = mob.Modes.IgnoreMelee;
            mobModesIgnoreRangedCheckBox.Checked = mob.Modes.IgnoreRanged;
            mobModesIgnoreMagicCheckBox.Checked = mob.Modes.IgnoreMagic;
            mobModesStatusImmuneCheckBox.Checked = mob.Modes.StatusImmune;
            mobModesSkillImmuneCheckBox.Checked = mob.Modes.SkillImmune;
            mobModesKnockbackImmuneCheckBox.Checked = mob.Modes.KnockbackImmune;
            mobModesFixedItemDropCheckBox.Checked = mob.Modes.FixedItemDrop;
            mobModesIgnoreMiscCheckBox.Checked = mob.Modes.IgnoreMisc;
            mobModesTeleportBlockCheckBox.Checked = mob.Modes.TeleportBlock;

            PrintAiSummary(mob);
        }

        private void SetAiComboBoxIndex(ComboBox comboBox, string entry)
        {
            // Aegis AI combo boxes
            if (_aegisAiModes.ContainsKey(entry)) {
                string fullEntry = $"{entry} - {_aegisAiModes[entry]}";
                if (comboBox.Items.Contains(fullEntry)) {
                    int index = comboBox.Items.IndexOf(fullEntry);
                    comboBox.SelectedIndex = index;
                    return;
                }
            }
        }

        private void PrintAiSummary(Mob mob)
        {
            // AI Summary
            mobModesEnabledTextBox.Text = string.Empty;
            mobModesEnabledTextBox.AppendText("Aegis AI Modes" + Environment.NewLine + "-------" + Environment.NewLine);
            string aegisModesTrimmed = _aegisAiModes[mob.Ai].Trim();
            string[] aegisModes = aegisModesTrimmed.Split("|");
            foreach (string mode in aegisModes) {
                mobModesEnabledTextBox.AppendText($" * {mode}" + Environment.NewLine);
            }
            mobModesEnabledTextBox.AppendText(Environment.NewLine + "rA AI Modes" + Environment.NewLine + "-------" + Environment.NewLine);
            foreach (CheckBox checkBox in _mobModeCheckBoxes) {
                if (checkBox.Checked) {
                    mobModesEnabledTextBox.AppendText($" * {checkBox.Text}" + Environment.NewLine);
                }
            }
            mobModesEnabledTextBox.SelectionStart = 0;
            mobModesEnabledTextBox.ScrollToCaret();
        }

        private static string MobToListEntry(Mob mob)
        {
            return $"[{mob.Id}] {mob.Name} ({mob.AegisName})";
        }

        private static string SkillToListEntry(MobSkill skill)
        {
            return $"[{skill.State}] {skill.SkillName} (Lv. {skill.SkillLv})";
        }

        private void SetSelectedMob(string selectedText)
        {
            // Find the mob we've selected
            Mob selectedMob = new();
            int mobId = int.Parse(Utilities.StringSplit(selectedText, '[', ']'));
            foreach (Mob entry in _mobDb.Mobs) {
                if (entry.Id == mobId) {
                    _selectedMob = entry;
                    break;
                }
            }
        }
        #endregion

        #region Event Handlers
        private void AddNewMob_Event(object sender, EventArgs e, Mob baseMob)
        {
            Mob mob = new();
            string identity = string.Empty;
            if (baseMob != null) {
                mob = new(baseMob);
                mob.AegisName += "_";
                if (_npcIdDb.Identities.ContainsKey(baseMob.Id)) {
                    identity = _npcIdDb.Identities[baseMob.Id];
                }
            }
            // set new mob ID, increment by 1 of whatever max is on current db
            List<int> allIds = new();
            foreach (Mob existingMob in _mobDb.Mobs) {
                allIds.Add(existingMob.Id);
            }
            int maxId = allIds.Max();
            mob.Id = maxId+1;
            
            // set the selected mob to the newly created mob
            _selectedMob = mob;

            // Catch empty identities and change them to some default
            if (identity == string.Empty) {
                identity = "PORING";
            }
            // Catch existing keys in the id db, just remove them
            if (_npcIdDb.Identities.ContainsKey(mob.Id)) {
                _npcIdDb.Identities.Remove(mob.Id);
            }
            _npcIdDb.Identities.Add(mob.Id, identity);
            _mobDb.Mobs.Add(mob);
            mobListBox.Items.Add(MobToListEntry(mob));
        }


        private void AddNewSkill_Event(object sender, EventArgs e)
        {
            MobSkill skill = new();
            skill.MobId = _selectedMob.Id;
            skill.MobName = _selectedMob.Name;
            _selectedMob.Skills.Add(skill);
            mobSkillList.Items.Add(SkillToListEntry(skill));
        }

        private void DeleteMob_Event(object sender, EventArgs e)
        {
            _mobDb.Mobs.Remove(_selectedMob);
            if (_npcIdDb.Identities.ContainsKey(_selectedMob.Id)) {
                _npcIdDb.Identities.Remove(_selectedMob.Id);
            }
            object mobToRemove = mobListBox.SelectedItem;
            int newIndex = mobListBox.SelectedIndex+1;
            if (mobListBox.Items.Count <= newIndex) {
                newIndex = mobListBox.SelectedIndex - 1;
            }
            if (mobListBox.Items.Count == 1) {
                mobListBox.SelectedIndex = -1;
            } else {
                mobListBox.SelectedIndex = newIndex;
            }
            mobListBox.Items.Remove(mobToRemove);
        }

        private void mobFilterBox_TextChanged(object sender, EventArgs e)
        {
            List<string> mobFilterResults = new();
            foreach (Mob mob in _mobDb.Mobs) {
                mobFilterResults.Add(MobToListEntry(mob));
            }
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
            if (mobListBox.SelectedItem == null) {
                return;
            }
            string selectedItem = mobListBox.SelectedItem.ToString();
            mobNameLabel.Text = selectedItem;

            SetSelectedMob(selectedItem);

            string spriteId;
#if DEBUG
            spriteId = ShowMobSpriteDebug(_selectedMob.Id);
#else
            spriteId = ShowMobSprite(mob.Id);
#endif
            ShowBasicMobInfo(_selectedMob, spriteId); ;

            // Populate the Skill List
            PopulateSkills();

            // Populate the Drop list
            mobDropsListBox.BeginUpdate();
            mobDropsListBox.Items.Clear();
            foreach (MobDrop drop in _selectedMob.Drops) {
                mobDropsListBox.Items.Add($"{drop.Item} ({drop.Rate})");
            }
            mobDropsListBox.EndUpdate();
        }
        #endregion

        private void mobSaveChangesButton_Click(object sender, EventArgs e)
        {
            _selectedMob.Name = mobFriendlyNameTextBox.Text;
            _selectedMob.AegisName = mobAegisNameTextBox.Text;
            _selectedMob.JapaneseName = mobJpNameTextBox.Text;
            _selectedMob.Id = (int)mobIdNumericUpDown.Value;
            //TODO: sprite selector
            string aegisAiModeFullString = mobAegisAiComboBox.SelectedItem.ToString();
            string[] aegisAiSplitString = aegisAiModeFullString.Split(" ");
            _selectedMob.Ai = aegisAiSplitString[0];
            _selectedMob.Class = Enum.TryParse(mobClassComboBox.SelectedItem.ToString(), out MobClass mobClass) 
                ? mobClass : _selectedMob.Class;
            _selectedMob.Size = Enum.TryParse(mobSizeComboBox.SelectedItem.ToString(), out MobSize mobSize)
                ? mobSize : _selectedMob.Size;
            _selectedMob.Race = Enum.TryParse(mobRaceComboBox.SelectedItem.ToString(), out MobRace mobRace)
                ? mobRace : _selectedMob.Race;
            _selectedMob.Element = Enum.TryParse(mobElementComboBox.SelectedItem.ToString(), out MobElement mobElement)
                ? mobElement : _selectedMob.Element;
            _selectedMob.ElementLevel = Utilities.GetIntFromControl(mobEleLvlComboBox);
            _selectedMob.Str = (int)mobBaseStatsStrUpDown.Value;
            _selectedMob.Int = (int)mobBaseStatsIntUpDown.Value;
            _selectedMob.Agi = (int)mobBaseStatsAgiUpDown.Value;
            _selectedMob.Dex = (int)mobBaseStatsDexUpDown.Value;
            _selectedMob.Vit = (int)mobBaseStatsVitUpDown.Value;
            _selectedMob.Luk = (int)mobBaseStatsLukUpDown.Value;
            _selectedMob.Hp = (int)mobStatsHpUpDown.Value;
            _selectedMob.Level = (int)mobStatsLevelUpDown.Value;
            _selectedMob.Attack = (int)mobStatsAttackUpDown.Value;
            _selectedMob.Attack2 = (int)mobStatsAttack2UpDown.Value;
            _selectedMob.Defense = (int)mobStatsDefenseUpDown.Value;
            _selectedMob.MagicDefense = (int)mobStatsMdefUpDown.Value;
            _selectedMob.AttackRange = (int)mobStatsAtkRangeUpDown.Value;
            _selectedMob.SkillRange = (int)mobStatsSkillRangeUpDown.Value;
            _selectedMob.ChaseRange = (int)mobStatsChaseRangeUpDown.Value;
            _selectedMob.WalkSpeed = (int)mobStatsAtkDelayUpDown.Value;
            _selectedMob.AttackDelay = (int)mobStatsAtkDelayUpDown.Value;
            _selectedMob.AttackMotion = (int)mobStatsDmgMotionUpDown.Value;
            _selectedMob.DamageMotion = (int)mobStatsDmgMotionUpDown.Value;

            _selectedMob.Modes.CanMove = mobModesCanMoveCheckBox.Checked;
            _selectedMob.Modes.CanAttack = mobModesCanAttackCheckBox.Checked;
            _selectedMob.Modes.NoRandomWalk = mobModesNoRandomWalkCheckBox.Checked;
            _selectedMob.Modes.NoCast = mobModesNoCastCheckBox.Checked;
            _selectedMob.Modes.Detector = mobModesDetectorCheckBox.Checked;
            _selectedMob.Modes.Looter = mobModesLooterCheckBox.Checked;
            _selectedMob.Modes.Aggressive = mobModesAggressiveCheckBox.Checked;
            _selectedMob.Modes.Assist = mobModesAssistAggroCheckBox.Checked;
            _selectedMob.Modes.CastSensorIdle = mobModesCastSensorIdleCheckBox.Checked;
            _selectedMob.Modes.CastSensorChase = mobModesCastSensorChaseCheckBox.Checked;
            _selectedMob.Modes.ChangeTargetMelee = mobModesChangeTargetMelee.Checked;
            _selectedMob.Modes.ChangeTargetChase = mobModesChangeTargetChase.Checked;
            _selectedMob.Modes.RandomTarget = mobModesRandomTargetCheckBox.Checked;
            _selectedMob.Modes.TargetWeak = mobModesTargetWeakCheckBox.Checked;
            _selectedMob.Modes.Angry = mobModesAngryCheckBox.Checked;
            _selectedMob.Modes.Mvp = mobModesMvpCheckBox.Checked;
            _selectedMob.Modes.IgnoreMelee = mobModesIgnoreMeleeCheckBox.Checked;
            _selectedMob.Modes.IgnoreRanged = mobModesIgnoreRangedCheckBox.Checked;
            _selectedMob.Modes.IgnoreMagic = mobModesIgnoreMagicCheckBox.Checked;
            _selectedMob.Modes.IgnoreMisc = mobModesIgnoreMiscCheckBox.Checked;
            _selectedMob.Modes.SkillImmune = mobModesSkillImmuneCheckBox.Checked;
            _selectedMob.Modes.StatusImmune = mobModesStatusImmuneCheckBox.Checked;
            _selectedMob.Modes.KnockbackImmune = mobModesFixedItemDropCheckBox.Checked;
            _selectedMob.Modes.FixedItemDrop = mobModesFixedItemDropCheckBox.Checked;
            _selectedMob.Modes.TeleportBlock = mobModesTeleportBlockCheckBox.Checked;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            DatabaseSaver.SerializeDatabase(_mobDb);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void skillEditor_Closed(object sender, FormClosedEventArgs e)
        {
            SkillEditor editor = (SkillEditor)sender;
            if (_openSkillEditors.Contains(editor)) {
                _openSkillEditors.Remove((SkillEditor)sender);
            }
        }
    }
}
