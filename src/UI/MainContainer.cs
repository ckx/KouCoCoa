﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;

namespace KouCoCoa
{
    internal partial class MainContainer : Form
    {
        #region Constructors
        public MainContainer(Dictionary<RAthenaDbType, List<IDatabase>> databases, 
            Dictionary<string, Image> images)
        {
            _databases = new(databases);
            _images = new(images);
            InitializeComponent();
            KouCoCoaInitialization();
        }
        #endregion

        #region Private fields
        private readonly Dictionary<RAthenaDbType, List<IDatabase>> _databases = new();
        private readonly Dictionary<string, Image> _images = new();
        private readonly ContextMenuStrip _mobDbsCMS = new();
        private readonly List<Form> _openDbForms = new();
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            Text = $"{Program.ProgramName} ～ {GetVersionTagline()}";

            // MobDBs menu
            _mobDbsCMS.Opening += new CancelEventHandler(mobDbs_Opening);
            mobDBToolStripMenuItem.DropDown = _mobDbsCMS;
        }

        private static string GetVersionTagline()
        {
            List<string> taglines = new() {
                "Girls need Tao cards, too!",
                "Onii-chan, look! Another Iron Cain!",
                "The way to a girl's heart is grilled Peco!",
                "Slow Poison !!",
                "heal plz",
                "zeny plz",
                "pa baps po",
                "Son of Great Bitch !!",
                "THEN WHO WAS STRINGS!?",
                "Remember, all you need for level 99 is a Cotton Shirts!",
                "If only ASSASSIN OF THE DARK could save us!!",
                "Don't forget to remove all your ekipz!!",
                "Don't you have anything better to do? Go outside.",
                "Mou, onii-chan!! That's not where you stick a Tao card!!",
                "Ohh, time to wang some DBs?",
                "ONE... HUNDRED... ICE PICKS!?!?!?",
                "Where did I leave my Megingjard...",
                "WE ARE THE STAAAAAARS",
                "Boys are... Arrow Shower.",
                "Oh, my Drooping Cat? Yeah, it has more INT than you..."
            };

            Random rand = new();
            int index = rand.Next(taglines.Count);
            return taglines[index];
        }

        /// <summary>
        /// Checks if an existing DB form exists (based on name). Sets focus to existing window if it exists.
        /// </summary>
        private bool CheckExistingDbForm(IDatabase dbName)
        {
            foreach (Form form in _openDbForms) {
                if (form.Text == $"{dbName.Name} :: {form.Name}") {
                    form.Focus();
                    return true;
                }
            }
            return false;
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
            if (!_databases.ContainsKey(RAthenaDbType.MOB_DB)) {
                ToolStripItem noneTsi = _mobDbsCMS.Items.Add("None");
                noneTsi.Enabled = false;
                return;
            }
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
            if (CheckExistingDbForm(senderMobDb)) {
                return;
            }
            if (_databases[RAthenaDbType.MOB_DB].Contains(senderMobDb)) {
                MobDatabaseEditor mde;
                MobSkillDatabase mobSkillDb = new();
                if (_databases.ContainsKey(RAthenaDbType.MOB_SKILL_DB)) {
                    // TODO: Is there any point to all this? We will probably only ever have 1 mob skill db...
                    // https://github.com/ckx/KouCoCoa/issues/2
                    mobSkillDb = (MobSkillDatabase)_databases[RAthenaDbType.MOB_SKILL_DB][0];
                }
                NpcIdentityDatabase npcIdDb = new();
                if (_databases.ContainsKey(RAthenaDbType.NPC_IDENTITY)) {
                    // ditto the above...
                    npcIdDb = (NpcIdentityDatabase)_databases[RAthenaDbType.NPC_IDENTITY][0];
                }
                mde = new(senderMobDb, mobSkillDb, npcIdDb, _images);
                mde.MdiParent = this;
                _openDbForms.Add(mde);
                mde.FormClosed += childForm_Closed;
                mde.Show();
            }
        }
        #endregion

        private void databaseOrganizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseOrganizer dbOranizer = new(_databases);
            dbOranizer.Parent = this;
            dbOranizer.Show();
        }

        private void childForm_Closed(object sender, FormClosedEventArgs e)
        {
            Form form = (Form)sender;
            if (_openDbForms.Contains(form)) {
                _openDbForms.Remove((Form)sender);
            }
        }
    }
}
