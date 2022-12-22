using System;
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
            foreach (KeyValuePair<RAthenaDbType, List<IDatabase>> dbCollection in databases) {
                Databases.Add(dbCollection.Key, dbCollection.Value);
            }
            _images = new(images);
            InitializeComponent();
            KouCoCoaInitialization();

            // Log Console
            LogConsole = new();
            LogConsole.MdiParent = this;
            LogConsoleInitialized = true;
            LogConsole.AddLogLine(string.Empty);
        }
        #endregion

        #region Public properties
        public static bool LogConsoleInitialized { get; private set; }
        #endregion

        #region Public fields
        public static readonly Dictionary<RAthenaDbType, List<IDatabase>> Databases = new();
        public static LoggerConsole LogConsole;
        #endregion

        #region Private fields
        private readonly Dictionary<string, Image> _images = new();
        private readonly ContextMenuStrip _windowCMS = new();
        private readonly ContextMenuStrip _mobDbsCMS = new();
        private readonly ContextMenuStrip _spawnGroupDbsCMS = new();
        private readonly List<Form> _openChildForms = new();
        #endregion

        #region Private methods
        private void KouCoCoaInitialization()
        {
            Text = $"{Program.ProgramName} ～ {GetVersionTagline()}";

            // Window menu
            _windowCMS.Opening += new CancelEventHandler(window_Opening);
            windowToolStripMenuItem.DropDown = _windowCMS;

            // MobDBs menu
            _mobDbsCMS.Opening += new CancelEventHandler(mobDbs_Opening);
            mobDBToolStripMenuItem.DropDown = _mobDbsCMS;

            // SpawnGroupDBs menu
            _spawnGroupDbsCMS.Opening += new CancelEventHandler(spawnGroupDbs_Opening);
            spawnGroupDBsToolStripMenuItem.DropDown = _spawnGroupDbsCMS;
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
        private bool CheckExistingDbForm(IDatabase db)
        {
            foreach (Form form in _openChildForms) {
                if (form.Text == $"{db.Name} :: {form.Name}") {
                    form.Focus();
                    return true;
                }
            }
            return false;
        }

        private bool ValidateNewDatabaseSelection(IDatabase db)
        {
            if (CheckExistingDbForm(db)) {
                return false;
            }
            if (!Databases.ContainsKey(db.DatabaseType)) {
                Logger.WriteLine($"Something went wrong. You tried to open a database of " +
                    $"type {db.DatabaseType}, but no databases of that type are currently loaded.", LogLevel.Error);
                return false;
            }
            return true;
        }
        #endregion


        #region Event Handlers
        private void window_Opening(object sender, CancelEventArgs e)
        {
            _windowCMS.Items.Clear();
            ToolStripItem mobViewerTsi = _windowCMS.Items.Add("Mob Stat Viewer");
            if (Databases.ContainsKey(RAthenaDbType.MOB_DB)) {
                mobViewerTsi.Click += delegate (object sender, EventArgs e) { mobStatViewer_Selection(sender, e, Databases[RAthenaDbType.MOB_DB]); };
            } else {
                mobViewerTsi.Enabled = false;
            }

            ToolStripItem logConsoleTsi = _windowCMS.Items.Add("Log Console");
            logConsoleTsi.Click += delegate (object sender, EventArgs e) { logConsole_Selection(sender, e); };
        }

        private void logConsole_Selection(object sender, EventArgs e)
        {
            LogConsole.Show();
        }

        /// <summary>
        /// Triggers on _mobDbsCMS, populates the list of MobDbs.
        /// </summary>
        private void mobDbs_Opening(object sender, CancelEventArgs e)
        {
            // Clear the old list of entries, re-populate them
            _mobDbsCMS.Items.Clear();
            if (!Databases.ContainsKey(RAthenaDbType.MOB_DB)) {
                ToolStripItem noneTsi = _mobDbsCMS.Items.Add("None");
                noneTsi.Enabled = false;
                return;
            }
            foreach (MobDatabase mobDb in Databases[RAthenaDbType.MOB_DB]) {
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
            if (!ValidateNewDatabaseSelection(senderMobDb)) {
                return;
            }
            if (Databases[RAthenaDbType.MOB_DB].Contains(senderMobDb)) {
                MobDatabaseEditor mde;
                MobSkillDatabase mobSkillDb = new();
                if (Databases.ContainsKey(RAthenaDbType.MOB_SKILL_DB)) {
                    // TODO: Is there any point to all this? We will probably only ever have 1 mob skill db...
                    // https://github.com/ckx/KouCoCoa/issues/2
                    mobSkillDb = (MobSkillDatabase)Databases[RAthenaDbType.MOB_SKILL_DB][0];
                }
                NpcIdentityDatabase npcIdDb = new();
                if (Databases.ContainsKey(RAthenaDbType.NPC_IDENTITY)) {
                    // ditto the above...
                    npcIdDb = (NpcIdentityDatabase)Databases[RAthenaDbType.NPC_IDENTITY][0];
                }
                mde = new(senderMobDb, mobSkillDb, npcIdDb, _images);
                mde.MdiParent = this;
                _openChildForms.Add(mde);
                mde.FormClosed += childForm_Closed;
                mde.Show();
            }
        }

        /// <summary>
        /// Triggers on _spawnGroupDbsCMS, populates the menu list of SpawnGroupDbs
        /// </summary>
        private void spawnGroupDbs_Opening(object sender, CancelEventArgs e)
        {
            _spawnGroupDbsCMS.Items.Clear();
            if (!Databases.ContainsKey(RAthenaDbType.SPAWNGROUP_DB)) {
                ToolStripItem noneTsi = _mobDbsCMS.Items.Add("None");
                noneTsi.Enabled = false;
                return;
            }
            foreach (SpawnGroupDatabase spawnGroupDb in Databases[RAthenaDbType.SPAWNGROUP_DB]) {
                ToolStripItem entryTsi = _spawnGroupDbsCMS.Items.Add(spawnGroupDb.Name);
                entryTsi.Click += delegate (object sender, EventArgs e) { spawnGroupDbs_Selection(sender, e, spawnGroupDb); };
            }
        }

        /// <summary>
        /// Triggers on selectio of an entry in _spawnGroupCMS, 
        /// creates a new SpawnGroupDatabaseEditor based on the approrpiate spawnGroupDb
        /// </summary>
        private void spawnGroupDbs_Selection(object sender, EventArgs e, SpawnGroupDatabase senderSpawnGroupDb)
        {
            if (!ValidateNewDatabaseSelection(senderSpawnGroupDb)) {
                return;
            }
            if (Databases[RAthenaDbType.SPAWNGROUP_DB].Contains(senderSpawnGroupDb)) {
                SpawnGroupDatabaseEditor sgde = new(senderSpawnGroupDb);
                sgde.MdiParent = this;
                _openChildForms.Add(sgde);
                sgde.FormClosed += childForm_Closed;
                sgde.Show();
            }
        }

        private void mobStatViewer_Selection(object sender, EventArgs e, List<IDatabase> mobDbs)
        {
            MobStatViewer mobStatViewer = new(mobDbs);
            mobStatViewer.MdiParent = this;
            _openChildForms.Add(mobStatViewer);
            mobStatViewer.FormClosed += childForm_Closed;
            mobStatViewer.Show();
        }

        private void databaseOrganizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseOrganizer dbOranizer = new(Databases);
            dbOranizer.Parent = this;
            dbOranizer.Show();
        }

        private void childForm_Closed(object sender, FormClosedEventArgs e)
        {
            Form form = (Form)sender;
            if (_openChildForms.Contains(form)) {
                _openChildForms.Remove((Form)sender);
            }
        }
        #endregion
    }
}
