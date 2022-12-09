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
            this.mobSkillList = new System.Windows.Forms.ListBox();
            this.mobSkillsColumnLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mobNameLabel = new System.Windows.Forms.Label();
            this.mobInfoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mobSpritePictureBox = new System.Windows.Forms.PictureBox();
            this.leftDockTableLayoutPanel.SuspendLayout();
            this.mobSkillTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.mobInfoTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mobSpritePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mobListBox
            // 
            this.mobListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobListBox.FormattingEnabled = true;
            this.mobListBox.ItemHeight = 15;
            this.mobListBox.Location = new System.Drawing.Point(1, 30);
            this.mobListBox.Margin = new System.Windows.Forms.Padding(1);
            this.mobListBox.Name = "mobListBox";
            this.mobListBox.Size = new System.Drawing.Size(248, 607);
            this.mobListBox.TabIndex = 0;
            // 
            // leftDockTableLayoutPanel
            // 
            this.leftDockTableLayoutPanel.ColumnCount = 1;
            this.leftDockTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftDockTableLayoutPanel.Controls.Add(this.mobListBox, 0, 1);
            this.leftDockTableLayoutPanel.Controls.Add(this.mobFilterBox, 0, 0);
            this.leftDockTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftDockTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.leftDockTableLayoutPanel.Name = "leftDockTableLayoutPanel";
            this.leftDockTableLayoutPanel.RowCount = 2;
            this.leftDockTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.leftDockTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.leftDockTableLayoutPanel.Size = new System.Drawing.Size(250, 638);
            this.leftDockTableLayoutPanel.TabIndex = 1;
            // 
            // mobFilterBox
            // 
            this.mobFilterBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobFilterBox.Location = new System.Drawing.Point(3, 3);
            this.mobFilterBox.MaximumSize = new System.Drawing.Size(244, 23);
            this.mobFilterBox.Name = "mobFilterBox";
            this.mobFilterBox.Size = new System.Drawing.Size(244, 23);
            this.mobFilterBox.TabIndex = 1;
            // 
            // mobSkillTableLayoutPanel
            // 
            this.mobSkillTableLayoutPanel.ColumnCount = 1;
            this.mobSkillTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mobSkillTableLayoutPanel.Controls.Add(this.mobSkillList, 0, 1);
            this.mobSkillTableLayoutPanel.Controls.Add(this.mobSkillsColumnLabel, 0, 0);
            this.mobSkillTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.mobSkillTableLayoutPanel.Location = new System.Drawing.Point(658, 0);
            this.mobSkillTableLayoutPanel.Name = "mobSkillTableLayoutPanel";
            this.mobSkillTableLayoutPanel.RowCount = 2;
            this.mobSkillTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mobSkillTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mobSkillTableLayoutPanel.Size = new System.Drawing.Size(200, 638);
            this.mobSkillTableLayoutPanel.TabIndex = 2;
            // 
            // mobSkillList
            // 
            this.mobSkillList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobSkillList.FormattingEnabled = true;
            this.mobSkillList.ItemHeight = 15;
            this.mobSkillList.Location = new System.Drawing.Point(3, 26);
            this.mobSkillList.Name = "mobSkillList";
            this.mobSkillList.Size = new System.Drawing.Size(194, 611);
            this.mobSkillList.TabIndex = 1;
            // 
            // mobSkillsColumnLabel
            // 
            this.mobSkillsColumnLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobSkillsColumnLabel.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mobSkillsColumnLabel.Location = new System.Drawing.Point(3, 0);
            this.mobSkillsColumnLabel.MaximumSize = new System.Drawing.Size(194, 23);
            this.mobSkillsColumnLabel.Name = "mobSkillsColumnLabel";
            this.mobSkillsColumnLabel.Size = new System.Drawing.Size(194, 23);
            this.mobSkillsColumnLabel.TabIndex = 0;
            this.mobSkillsColumnLabel.Text = "Skill list";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.mobNameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mobInfoTableLayoutPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(250, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.388715F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.61128F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(408, 638);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // mobNameLabel
            // 
            this.mobNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobNameLabel.Font = new System.Drawing.Font("Yu Gothic UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mobNameLabel.Location = new System.Drawing.Point(3, 0);
            this.mobNameLabel.MaximumSize = new System.Drawing.Size(10000, 23);
            this.mobNameLabel.Name = "mobNameLabel";
            this.mobNameLabel.Size = new System.Drawing.Size(402, 23);
            this.mobNameLabel.TabIndex = 0;
            this.mobNameLabel.Text = "Mob name";
            // 
            // mobInfoTableLayoutPanel
            // 
            this.mobInfoTableLayoutPanel.ColumnCount = 2;
            this.mobInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mobInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mobInfoTableLayoutPanel.Controls.Add(this.mobSpritePictureBox, 0, 0);
            this.mobInfoTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobInfoTableLayoutPanel.Location = new System.Drawing.Point(3, 31);
            this.mobInfoTableLayoutPanel.Name = "mobInfoTableLayoutPanel";
            this.mobInfoTableLayoutPanel.RowCount = 2;
            this.mobInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mobInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mobInfoTableLayoutPanel.Size = new System.Drawing.Size(402, 604);
            this.mobInfoTableLayoutPanel.TabIndex = 1;
            // 
            // mobSpritePictureBox
            // 
            this.mobSpritePictureBox.Location = new System.Drawing.Point(3, 3);
            this.mobSpritePictureBox.Name = "mobSpritePictureBox";
            this.mobSpritePictureBox.Size = new System.Drawing.Size(145, 170);
            this.mobSpritePictureBox.TabIndex = 0;
            this.mobSpritePictureBox.TabStop = false;
            // 
            // MobDatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 638);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mobSkillTableLayoutPanel);
            this.Controls.Add(this.leftDockTableLayoutPanel);
            this.Name = "MobDatabaseEditor";
            this.Text = "MobDatabaseEditor";
            this.leftDockTableLayoutPanel.ResumeLayout(false);
            this.leftDockTableLayoutPanel.PerformLayout();
            this.mobSkillTableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.mobInfoTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mobSpritePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox mobListBox;
        private System.Windows.Forms.TableLayoutPanel leftDockTableLayoutPanel;
        private System.Windows.Forms.TextBox mobFilterBox;
        private System.Windows.Forms.TableLayoutPanel mobSkillTableLayoutPanel;
        private System.Windows.Forms.Label mobSkillsColumnLabel;
        private System.Windows.Forms.ListBox mobSkillList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label mobNameLabel;
        private System.Windows.Forms.TableLayoutPanel mobInfoTableLayoutPanel;
        private System.Windows.Forms.PictureBox mobSpritePictureBox;
    }
}