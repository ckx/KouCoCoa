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
        private readonly List<Mob> _mobs = new();
        private readonly DataTable _dt = new();
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
                    _mobs.Add(mob);
                }
            }

            SetupDataTable();
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.DataSource = _dt;
        }

        private void SetupDataTable()
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

            foreach (Mob mob in _mobs) {
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
        #endregion

        #region Private event handlers
        #endregion
    }
}
