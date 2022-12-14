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
    internal partial class DatabaseOrganizer : Form
    {
        public DatabaseOrganizer(Dictionary<RAthenaDbType, List<IDatabase>> allDatabases)
        {
            _allDatabases = allDatabases;
            InitializeComponent();
            KoucocoaInitialize();
        }

        private readonly Dictionary<RAthenaDbType, List<IDatabase>> _allDatabases = new();

        private void KoucocoaInitialize()
        {
            TopLevel = false;
            foreach (KeyValuePair<RAthenaDbType, List<IDatabase>> dbCollection in _allDatabases) {
                if (dbCollection.Key == RAthenaDbType.UNSUPPORTED) {
                    continue;
                }
                databaseTypeSelectorComboBox.Items.Add(dbCollection.Key);
            }
        }

        private void databaseTypeSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RAthenaDbType selectedIndex = (RAthenaDbType)databaseTypeSelectorComboBox.SelectedItem;

            foreach (IDatabase db in _allDatabases[selectedIndex]) {
                dbOrangizerListBox.Items.Add(db.Name);
            }
        }
    }
}
