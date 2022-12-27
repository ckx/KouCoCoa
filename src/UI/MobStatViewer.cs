using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KouCoCoa
{
    internal partial class MobStatViewer : Form
    {
        #region Constructors
        public MobStatViewer(List<IDatabase> mobDbs)
        {
            foreach (IDatabase db in mobDbs) {
                _mobDbs.Add((MobDatabase)db);
            }
            InitializeComponent();
            KouCoCoaInitialization();
        }
        #endregion

        #region Private members
        private readonly List<MobDatabase> _mobDbs = new();
        private readonly List<Mob> _allMobs = new();
        private readonly DataTable _dt = new();
        private readonly List<Mob> _displayedMobs = new();
        private bool _dtInitialized = false;
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            Text = $"{Name}";

            foreach (MobDatabase mobDb in _mobDbs) {
                foreach (Mob mob in mobDb.Mobs) {
                    mob.JapaneseName = mobDb.Name;
                    _allMobs.Add(mob);
                }
            }

            InitializeComboBoxValues();
            InitializeDataTable();
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.DataSource = _dt;
        }

        private void InitializeComboBoxValues()
        {
            mobClassComboBox.Items.Add("ANY");
            mobClassComboBox.SelectedItem = "ANY";
            foreach (MobClass mobClass in Enum.GetValues(typeof(MobClass))) {
                mobClassComboBox.Items.Add(mobClass.ToString());
            }

            mobRaceComboBox.Items.Add("ANY");
            mobRaceComboBox.SelectedItem = "ANY";
            foreach (MobRace mobRace in Enum.GetValues(typeof(MobRace))) {
                mobRaceComboBox.Items.Add(mobRace.ToString());
            }

            mobElementComboBox.Items.Add("ANY");
            mobElementComboBox.SelectedItem = "ANY";
            foreach (MobElement mobElement in Enum.GetValues(typeof(MobElement))) {
                mobElementComboBox.Items.Add(mobElement.ToString());
            }
        }

        private void InitializeDataTable()
        {
            _dt.Columns.Add("ID", typeof(int));
            _dt.Columns.Add("Name", typeof(string));
            _dt.Columns.Add("HP", typeof(int));
            _dt.Columns.Add("AtkMin", typeof(int));
            _dt.Columns.Add("AtkMax", typeof(int));
            _dt.Columns.Add("Def", typeof(int));
            _dt.Columns.Add("Mdef", typeof(int));
            _dt.Columns.Add("Size", typeof(MobSize));
            _dt.Columns.Add("Race", typeof(MobRace));
            _dt.Columns.Add("Element", typeof(MobElement));
            _dt.Columns.Add("ElementLvl", typeof(int));
            _dt.Columns.Add("Class", typeof(MobClass));
            _dt.Columns.Add("DB", typeof(string));
            _dtInitialized = true;
            UpdateDataTable();
        }

        private void UpdateDataTable()
        {
            if (!_dtInitialized) {
                return;
            }
            _dt.Rows.Clear();
            FilterMobs();
            foreach (Mob mob in _displayedMobs) {
                DataRow row = _dt.NewRow();
                row["ID"] = mob.Id;
                row["Name"] = mob.Name;
                row["HP"] = mob.Hp;
                row["AtkMin"] = mob.Attack;
                row["AtkMax"] = mob.Attack2;
                row["Def"] = mob.Defense;
                row["Mdef"] = mob.MagicDefense;
                row["Size"] = mob.Size;
                row["Race"] = mob.Race;
                row["Element"] = mob.Element;
                row["ElementLvl"] = mob.ElementLevel;
                row["Class"] = mob.Class;
                row["DB"] = mob.JapaneseName;
                _dt.Rows.Add(row);
            }
        }

        // silly dumbo filter
        private void FilterMobs()
        {
            MobClass? mobClass = Enum.TryParse(mobClassComboBox.SelectedItem.ToString(), out MobClass mc) ? mc : null;
            MobRace? mobRace = Enum.TryParse(mobRaceComboBox.SelectedItem.ToString(), out MobRace mr) ? mr : null;
            MobElement? mobElement = Enum.TryParse(mobElementComboBox.SelectedItem.ToString(), out MobElement me) ? me : null;

            _displayedMobs.Clear();
            foreach (Mob mob in _allMobs) {
                if (mobClass != null && mob.Class != mobClass) {
                    continue;
                }
                if (mobRace != null && mob.Race != mobRace) {
                    continue;
                }
                if (mobElement != null && mob.Element != mobElement) {
                    continue;
                }
                _displayedMobs.Add(new(mob));
            }
        }
        #endregion

        #region Event handlers
        private void classComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataTable();
        }

        private void mobRaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataTable();
        }

        private void mobElementComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataTable();
        }
        #endregion
    }
}
