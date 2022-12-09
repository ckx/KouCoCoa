using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace KouCoCoa
{
    internal partial class MainContainer : Form
    {
        #region Constructors
        public MainContainer(Dictionary<RAthenaDbType, List<IDatabase>> databases)
        {
            _databases = new(databases);
            InitializeComponent();
            KouCoCoaInitialization();
        }
        #endregion

        #region Private fields
        private readonly Dictionary<RAthenaDbType, List<IDatabase>> _databases = new();
        private readonly ContextMenuStrip _mobDbsCMS = new();
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            foreach (KeyValuePair<RAthenaDbType, List<IDatabase>> dbEntries in _databases) {
                mainMenuStrip.Items.Add(dbEntries.Key.ToString());
            }

            // MobDBs menu
            _mobDbsCMS.Opening += new CancelEventHandler(mobDbs_Opening);
            mobDBToolStripMenuItem.DropDown = _mobDbsCMS;
            ToolStripMenuItem mobDbMenuItem = new("ROGUENAROK DB", null, null, "ROGUENAROK");
        }
        #endregion


        #region Event Handlers
        /// <summary>
        /// Triggers on _mobDbsCMS, populates the list of MobDbs.
        /// </summary>
        private void mobDbs_Opening(object sender, CancelEventArgs e)
        {
            // Clear the old list of entries, re-populate them
            _mobDbsCMS.Items.Clear();
            foreach (MobDatabase mobDb in _databases[RAthenaDbType.MOB_DB]) {
                ToolStripItem entryTsi = _mobDbsCMS.Items.Add(mobDb.Name);
                entryTsi.Click += delegate(object sender, EventArgs e) { mobDbs_Selection(sender, e, mobDb); };
            }
            e.Cancel = false;
        }

        /// <summary>
        /// Triggers on selection of an entry _mobDbsCMS, creates a new MobDatabaseEditor based on the appropriate mobDb
        /// </summary>
        private void mobDbs_Selection(object sender, EventArgs e, MobDatabase senderMobDb) 
        {
            if (_databases[RAthenaDbType.MOB_DB].Contains(senderMobDb)) {
                MobSkillDatabase mobSkillDb = new();
                if (_databases.ContainsKey(RAthenaDbType.MOB_SKILL_DB)) {
                    // TODO: Is there any point to all this? We will probably only ever have 1 mob skill db...
                    // https://github.com/ckx/KouCoCoa/issues/2
                    mobSkillDb = (MobSkillDatabase)_databases[RAthenaDbType.MOB_SKILL_DB][0];
                }
                MobDatabaseEditor mde = new(senderMobDb, mobSkillDb);
                mde.MdiParent = this;
                mde.Show();
            }
        }
        #endregion
    }
}
