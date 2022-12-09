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
        public MobDatabaseEditor(MobDatabase mobDb)
        {
            _mobDb = new(mobDb);
            InitializeComponent();
            KouCoCoaInitialization();
        }
        #endregion

        #region Private members
        MobDatabase _mobDb = new();
        List<string> _mobNames = new();
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            mobFilterBox.TextChanged += mobFilterBox_TextChanged;

            // Populate the mobList
            ListBox.ObjectCollection mobListBoxCollection = new(mobListBox);
            foreach (Mob mob in _mobDb.Mobs) {
                _mobNames.Add(mob.Name);
            }
            mobListBoxCollection.AddRange(_mobNames.Cast<object>().ToArray());
            mobListBox.Items.AddRange(mobListBoxCollection);
        }
        #endregion

        #region Event Handlers
        private void mobFilterBox_TextChanged(object sender, EventArgs e)
        {
            List<string> mobFilterResults = _mobNames;
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
        #endregion
    }
}
