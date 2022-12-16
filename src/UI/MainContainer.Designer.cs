namespace KouCoCoa
{
    partial class MainContainer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated codeahahks

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseOrganizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mobDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spawnGroupDBsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.mobDBToolStripMenuItem,
            this.spawnGroupDBsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1691, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.databaseOrganizerToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // databaseOrganizerToolStripMenuItem
            // 
            this.databaseOrganizerToolStripMenuItem.Name = "databaseOrganizerToolStripMenuItem";
            this.databaseOrganizerToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.databaseOrganizerToolStripMenuItem.Text = "&Database Organizer";
            this.databaseOrganizerToolStripMenuItem.Click += new System.EventHandler(this.databaseOrganizerToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // mobDBToolStripMenuItem
            // 
            this.mobDBToolStripMenuItem.Name = "mobDBToolStripMenuItem";
            this.mobDBToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.mobDBToolStripMenuItem.Text = "&MobDBs";
            // 
            // spawnGroupDBsToolStripMenuItem
            // 
            this.spawnGroupDBsToolStripMenuItem.Name = "spawnGroupDBsToolStripMenuItem";
            this.spawnGroupDBsToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.spawnGroupDBsToolStripMenuItem.Text = "&SpawnGroupDBs";
            // 
            // MainContainer
            // 
            this.ClientSize = new System.Drawing.Size(1691, 996);
            this.Controls.Add(this.mainMenuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainContainer";
            this.Text = "KouCoCoa";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mobDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseOrganizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spawnGroupDBsToolStripMenuItem;
    }
}