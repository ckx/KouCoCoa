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
            sgMembersDataGridView.DragEnter += sgMembersDataGridView_DragEnter;
            sgMembersDataGridView.DragDrop += sgMembersDataGridView_DragDrop;
            sgMembersDataGridView.CellEndEdit += sgMembersDataGridView_CellEndEdit;

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
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Count", typeof(int));
            dt.Columns.Add("RewardMod", typeof(int));

            // Rows
            foreach (SpawnGroupMember member in _selectedSpawnGroup.Members) {
                DataRow row = dt.NewRow();
                row["ID"] = member.Id;
                row["Name"] = member.Name;
                row["Count"] = member.Count;
                row["RewardMod"] = member.RewardMod;
                dt.Rows.Add(row);
            }

            sgMembersDataGridView.DataSource = dt;
            sgMembersDataGridView.AllowDrop = true;
            sgMembersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void UpdateSpawnGroup()
        {
            _selectedSpawnGroup.Members = new();
            DataGridViewRowCollection rowCollection = sgMembersDataGridView.Rows;
            foreach (DataGridViewRow row in rowCollection) {
                if (row.DataBoundItem == null) {
                    continue;
                }
                DataRow dataRow = (row.DataBoundItem as DataRowView).Row;
                SpawnGroupMember sgMember = new();
                sgMember.Id = int.TryParse(dataRow[0].ToString(), out int id) ? id : int.MinValue;
                sgMember.Name = dataRow[1].ToString();
                sgMember.Count = int.TryParse(dataRow[2].ToString(), out int count) ? count : int.MinValue;
                sgMember.RewardMod = int.TryParse(dataRow[3].ToString(), out int rewardMod) ? rewardMod : int.MinValue;
                _selectedSpawnGroup.Members.Add(sgMember);
            }
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

        private void sgMembersDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text)) {
                e.Effect = DragDropEffects.Copy;
            } else {
                e.Effect = DragDropEffects.None;
            }
        }

        private void sgMembersDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] memberInfo = e.Data.GetData(DataFormats.Text).ToString().Split('|');
            if (memberInfo.Length == 2) {
                DataTable dt = (DataTable)sgMembersDataGridView.DataSource;
                DataRow row = dt.NewRow();
                row[0] = int.TryParse(memberInfo[0], out int id) ? id : 0;
                row[1] = memberInfo[1];
                row[2] = 0;
                row[3] = 0;
                dt.Rows.Add(row);
                sgMembersDataGridView.DataSource = dt;
                UpdateSpawnGroup();
            }
        }

        private void sgMembersDataGridView_CellEndEdit(object sender, EventArgs e)
        {
            UpdateSpawnGroup();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            UpdateSpawnGroup();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            DatabaseSaver.SerializeDatabase(_spawnGroupDb);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
        #endregion
    }
}
