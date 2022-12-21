using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KouCoCoa
{
    internal partial class SpawnGroupDatabaseEditor : Form
    {
        #region Constructors
        public SpawnGroupDatabaseEditor(SpawnGroupDatabase spawnGroupDb)
        {
            _spawnGroupDb = new(spawnGroupDb);
            InitializeComponent();
            KouCoCoaInitialization();
        }
        #endregion

        #region Private members
        private readonly SpawnGroupDatabase _spawnGroupDb = new();
        private SpawnGroup _selectedSpawnGroup;
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            Text = $"{_spawnGroupDb.Name} :: {Name}";

            // Event subscripes
            sgListBox.SelectedValueChanged += sgListBox_SelectedValueChanged;

            // Populate the left-docked spawnGroupListBox
            ListBox.ObjectCollection sgListBoxCollection = new(sgListBox);
            List<string> sgNames = new();
            foreach (SpawnGroup sg in _spawnGroupDb.SpawnGroups) {
                sgNames.Add(BuildSpawnGroupEntryString(Utilities.SpawnGroupToListEntry(sg), sg.Members.Count));
            }
            sgListBoxCollection.AddRange(sgNames.Cast<object>().ToArray());
            sgListBox.Items.AddRange(sgListBoxCollection);
        }

        private static string BuildSpawnGroupEntryString(string spawnGroupEntry, int mobCount)
        {
            return $"{spawnGroupEntry} ({mobCount} mobs)";
        }

        private void SetSelectedSpawnGroup(string selectedText)
        {
            SpawnGroup selectedSg = new();
            int sgId = int.Parse(Utilities.StringSplit(selectedText, '[', ']'));
            foreach (SpawnGroup sg in _spawnGroupDb.SpawnGroups) {
                if (sg.Id == sgId) {
                    _selectedSpawnGroup = sg;
                    break;
                }
            }
        }

        private void SetupDataGridView()
        {
            DataTable dt = new();

            // Columns
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Count", typeof(int));
            dt.Columns.Add("RewardMod", typeof(int));

            // Rows
            foreach (SpawnGroupMember member in _selectedSpawnGroup.Members) {
                DataRow row = dt.NewRow();
                // D I S G U S T I N G
                row[0] = member.Id;
                row[1] = member.Name;
                row[2] = member.Count;
                row[3] = member.RewardMod;
                dt.Rows.Add(row);
            }

            sgMembersDataGridView.DataSource = dt;
        }
        #endregion

        #region Event handlers
        private void sgFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            List<string> sgFilterResults = new();
            Dictionary<string, int> sgMobCount = new();
            foreach (SpawnGroup sg in _spawnGroupDb.SpawnGroups) {
                sgFilterResults.Add(Utilities.SpawnGroupToListEntry(sg));
                sgMobCount.Add(Utilities.SpawnGroupToListEntry(sg), sg.Members.Count);
            }
            sgListBox.BeginUpdate();
            sgListBox.Items.Clear();
            if (!string.IsNullOrEmpty(sgFilterTextBox.Text)) {
                foreach (string filterResult in sgFilterResults) {
                    if (filterResult.Contains(sgFilterTextBox.Text, StringComparison.CurrentCultureIgnoreCase)) {
                        string listEntry = filterResult;
                        if (sgMobCount.ContainsKey(filterResult)) {
                            listEntry = BuildSpawnGroupEntryString(listEntry, sgMobCount[listEntry]);
                        }
                        sgListBox.Items.Add(listEntry);
                    }
                }
            } else {
                foreach (string filterResult in sgFilterResults) {
                    string listEntry = filterResult;
                    if (sgMobCount.ContainsKey(filterResult)) {
                        listEntry = BuildSpawnGroupEntryString(listEntry, sgMobCount[listEntry]);
                    }
                    sgListBox.Items.Add(listEntry);
                }
            }
            sgListBox.EndUpdate();
        }

        private void sgListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sgListBox.SelectedItem == null || sgListBox.SelectedIndex == -1) {
                return;
            }
            SetSelectedSpawnGroup(sgListBox.SelectedItem.ToString());

            sgTitleLabel.Text = Utilities.SpawnGroupToListEntry(_selectedSpawnGroup);
            SetupDataGridView();
        }
        #endregion
    }
}
