using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KouCoCoa
{
    internal partial class MainContainer : Form
    {
        private Dictionary<RAthenaDbType, List<IDatabase>> _databases = new();

        public MainContainer(Dictionary<RAthenaDbType, List<IDatabase>> databases)
        {
            _databases = new(databases);
            InitializeComponent();
            KouCoCoaInitialization();
        }
        
        private void KouCoCoaInitialization()
        {
            foreach (KeyValuePair<RAthenaDbType, List<IDatabase>> dbEntries in _databases) {
                this.menuStrip1.Items.Add(dbEntries.Key.ToString());
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        protected void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////MobDatabaseEditor newMDIChild = new MobDatabaseEditor();
            //// Set the Parent Form of the Child window.
            //newMDIChild.MdiParent = this;
            //// Display the new form.
            //newMDIChild.Show();
        }
    }
}
