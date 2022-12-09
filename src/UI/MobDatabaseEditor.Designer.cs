namespace KouCoCoa
{
    partial class MobDatabaseEditor
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
            this.mobListBox = new System.Windows.Forms.ListBox();
            this.leftDockTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mobFilterBox = new System.Windows.Forms.TextBox();
            this.mobSkillTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mobSkillsColumnLabel = new System.Windows.Forms.Label();
            this.mobSkillList = new System.Windows.Forms.ListBox();
            this.leftDockTableLayoutPanel.SuspendLayout();
            this.mobSkillTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mobListBox
            // 
            this.mobListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobListBox.FormattingEnabled = true;
            this.mobListBox.ItemHeight = 15;
            this.mobListBox.Location = new System.Drawing.Point(1, 28);
            this.mobListBox.Margin = new System.Windows.Forms.Padding(1);
            this.mobListBox.Name = "mobListBox";
            this.mobListBox.Size = new System.Drawing.Size(248, 421);
            this.mobListBox.TabIndex = 0;
            // 
            // leftDockTableLayoutPanel
            // 
            this.leftDockTableLayoutPanel.ColumnCount = 1;
            this.leftDockTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftDockTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.leftDockTableLayoutPanel.Controls.Add(this.mobListBox, 0, 1);
            this.leftDockTableLayoutPanel.Controls.Add(this.mobFilterBox, 0, 0);
            this.leftDockTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftDockTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.leftDockTableLayoutPanel.Name = "leftDockTableLayoutPanel";
            this.leftDockTableLayoutPanel.RowCount = 2;
            this.leftDockTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.leftDockTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.leftDockTableLayoutPanel.Size = new System.Drawing.Size(250, 450);
            this.leftDockTableLayoutPanel.TabIndex = 1;
            // 
            // mobFilterBox
            // 
            this.mobFilterBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobFilterBox.Location = new System.Drawing.Point(3, 3);
            this.mobFilterBox.Name = "mobFilterBox";
            this.mobFilterBox.Size = new System.Drawing.Size(244, 23);
            this.mobFilterBox.TabIndex = 1;
            // 
            // mobSkillTableLayoutPanel
            // 
            this.mobSkillTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mobSkillTableLayoutPanel.ColumnCount = 1;
            this.mobSkillTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mobSkillTableLayoutPanel.Controls.Add(this.mobSkillsColumnLabel, 0, 0);
            this.mobSkillTableLayoutPanel.Controls.Add(this.mobSkillList, 0, 1);
            this.mobSkillTableLayoutPanel.Location = new System.Drawing.Point(600, 0);
            this.mobSkillTableLayoutPanel.Name = "mobSkillTableLayoutPanel";
            this.mobSkillTableLayoutPanel.RowCount = 2;
            this.mobSkillTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mobSkillTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 427F));
            this.mobSkillTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mobSkillTableLayoutPanel.Size = new System.Drawing.Size(200, 450);
            this.mobSkillTableLayoutPanel.TabIndex = 2;
            // 
            // mobSkillsColumnLabel
            // 
            this.mobSkillsColumnLabel.AutoSize = true;
            this.mobSkillsColumnLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobSkillsColumnLabel.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mobSkillsColumnLabel.Location = new System.Drawing.Point(3, 0);
            this.mobSkillsColumnLabel.Name = "mobSkillsColumnLabel";
            this.mobSkillsColumnLabel.Size = new System.Drawing.Size(194, 23);
            this.mobSkillsColumnLabel.TabIndex = 0;
            this.mobSkillsColumnLabel.Text = "Skill list";
            // 
            // mobSkillList
            // 
            this.mobSkillList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mobSkillList.FormattingEnabled = true;
            this.mobSkillList.ItemHeight = 15;
            this.mobSkillList.Location = new System.Drawing.Point(3, 26);
            this.mobSkillList.Name = "mobSkillList";
            this.mobSkillList.Size = new System.Drawing.Size(194, 409);
            this.mobSkillList.TabIndex = 1;
            // 
            // MobDatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mobSkillTableLayoutPanel);
            this.Controls.Add(this.leftDockTableLayoutPanel);
            this.Name = "MobDatabaseEditor";
            this.Text = "MobDatabaseEditor";
            this.leftDockTableLayoutPanel.ResumeLayout(false);
            this.leftDockTableLayoutPanel.PerformLayout();
            this.mobSkillTableLayoutPanel.ResumeLayout(false);
            this.mobSkillTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox mobListBox;
        private System.Windows.Forms.TableLayoutPanel leftDockTableLayoutPanel;
        private System.Windows.Forms.TextBox mobFilterBox;
        private System.Windows.Forms.TableLayoutPanel mobSkillTableLayoutPanel;
        private System.Windows.Forms.Label mobSkillsColumnLabel;
        private System.Windows.Forms.ListBox mobSkillList;
    }
}