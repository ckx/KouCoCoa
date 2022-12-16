namespace KouCoCoa
{
    partial class SpawnGroupDatabaseEditor
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sgListBox = new System.Windows.Forms.ListBox();
            this.sgFilterTextBox = new System.Windows.Forms.TextBox();
            this.sgTitleLabel = new System.Windows.Forms.Label();
            this.spawnGroupInfoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.sgMembersDataGridView = new System.Windows.Forms.DataGridView();
            this.memberId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberRewardMod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.spawnGroupInfoTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sgMembersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.10553F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.89447F));
            this.tableLayoutPanel1.Controls.Add(this.sgListBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.sgFilterTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sgTitleLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.spawnGroupInfoTableLayoutPanel, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.429072F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.57093F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(796, 571);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // sgListBox
            // 
            this.sgListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgListBox.FormattingEnabled = true;
            this.sgListBox.ItemHeight = 15;
            this.sgListBox.Location = new System.Drawing.Point(3, 34);
            this.sgListBox.Name = "sgListBox";
            this.sgListBox.Size = new System.Drawing.Size(162, 534);
            this.sgListBox.TabIndex = 0;
            // 
            // sgFilterTextBox
            // 
            this.sgFilterTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgFilterTextBox.Location = new System.Drawing.Point(3, 3);
            this.sgFilterTextBox.Name = "sgFilterTextBox";
            this.sgFilterTextBox.Size = new System.Drawing.Size(162, 23);
            this.sgFilterTextBox.TabIndex = 1;
            this.sgFilterTextBox.TextChanged += new System.EventHandler(this.sgFilterTextBox_TextChanged);
            // 
            // sgTitleLabel
            // 
            this.sgTitleLabel.AutoSize = true;
            this.sgTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgTitleLabel.Font = new System.Drawing.Font("Yu Gothic UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sgTitleLabel.Location = new System.Drawing.Point(171, 0);
            this.sgTitleLabel.Name = "sgTitleLabel";
            this.sgTitleLabel.Size = new System.Drawing.Size(622, 31);
            this.sgTitleLabel.TabIndex = 2;
            this.sgTitleLabel.Text = "SpawnGroup Name";
            // 
            // spawnGroupInfoTableLayoutPanel
            // 
            this.spawnGroupInfoTableLayoutPanel.ColumnCount = 2;
            this.spawnGroupInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.72668F));
            this.spawnGroupInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.27331F));
            this.spawnGroupInfoTableLayoutPanel.Controls.Add(this.sgMembersDataGridView, 0, 0);
            this.spawnGroupInfoTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spawnGroupInfoTableLayoutPanel.Location = new System.Drawing.Point(171, 34);
            this.spawnGroupInfoTableLayoutPanel.Name = "spawnGroupInfoTableLayoutPanel";
            this.spawnGroupInfoTableLayoutPanel.RowCount = 2;
            this.spawnGroupInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.01124F));
            this.spawnGroupInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.988764F));
            this.spawnGroupInfoTableLayoutPanel.Size = new System.Drawing.Size(622, 534);
            this.spawnGroupInfoTableLayoutPanel.TabIndex = 3;
            // 
            // sgMembersDataGridView
            // 
            this.sgMembersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sgMembersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.memberId,
            this.memberName,
            this.memberCount,
            this.memberRewardMod});
            this.sgMembersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgMembersDataGridView.Location = new System.Drawing.Point(3, 3);
            this.sgMembersDataGridView.Name = "sgMembersDataGridView";
            this.sgMembersDataGridView.RowTemplate.Height = 25;
            this.sgMembersDataGridView.Size = new System.Drawing.Size(521, 480);
            this.sgMembersDataGridView.TabIndex = 0;
            // 
            // memberId
            // 
            this.memberId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.memberId.Frozen = true;
            this.memberId.HeaderText = "ID";
            this.memberId.MinimumWidth = 3;
            this.memberId.Name = "memberId";
            this.memberId.Width = 43;
            // 
            // memberName
            // 
            this.memberName.Frozen = true;
            this.memberName.HeaderText = "Name";
            this.memberName.Name = "memberName";
            this.memberName.ToolTipText = "Mob\'s friendly name";
            // 
            // memberCount
            // 
            this.memberCount.Frozen = true;
            this.memberCount.HeaderText = "Count";
            this.memberCount.Name = "memberCount";
            // 
            // memberRewardMod
            // 
            this.memberRewardMod.Frozen = true;
            this.memberRewardMod.HeaderText = "Reward Modifier";
            this.memberRewardMod.Name = "memberRewardMod";
            // 
            // SpawnGroupDatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 571);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SpawnGroupDatabaseEditor";
            this.Text = "SpawnGroupDatabaseEditor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.spawnGroupInfoTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sgMembersDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox sgListBox;
        private System.Windows.Forms.TextBox sgFilterTextBox;
        private System.Windows.Forms.Label sgTitleLabel;
        private System.Windows.Forms.TableLayoutPanel spawnGroupInfoTableLayoutPanel;
        private System.Windows.Forms.DataGridView sgMembersDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberId;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberRewardMod;
    }
}