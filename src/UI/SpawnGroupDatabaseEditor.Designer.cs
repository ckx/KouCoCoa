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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sgListBox = new System.Windows.Forms.ListBox();
            this.sgFilterTextBox = new System.Windows.Forms.TextBox();
            this.sgTitleLabel = new System.Windows.Forms.Label();
            this.spawnGroupInfoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.sgMembersDataGridView = new System.Windows.Forms.DataGridView();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(796, 555);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // sgListBox
            // 
            this.sgListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgListBox.FormattingEnabled = true;
            this.sgListBox.ItemHeight = 15;
            this.sgListBox.Location = new System.Drawing.Point(3, 33);
            this.sgListBox.Name = "sgListBox";
            this.sgListBox.Size = new System.Drawing.Size(162, 519);
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
            this.sgTitleLabel.Size = new System.Drawing.Size(622, 30);
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
            this.spawnGroupInfoTableLayoutPanel.Location = new System.Drawing.Point(171, 33);
            this.spawnGroupInfoTableLayoutPanel.Name = "spawnGroupInfoTableLayoutPanel";
            this.spawnGroupInfoTableLayoutPanel.RowCount = 2;
            this.spawnGroupInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.01124F));
            this.spawnGroupInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.988764F));
            this.spawnGroupInfoTableLayoutPanel.Size = new System.Drawing.Size(622, 519);
            this.spawnGroupInfoTableLayoutPanel.TabIndex = 3;
            // 
            // sgMembersDataGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.sgMembersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.sgMembersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.sgMembersDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.sgMembersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgMembersDataGridView.Location = new System.Drawing.Point(3, 3);
            this.sgMembersDataGridView.Name = "sgMembersDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.sgMembersDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.sgMembersDataGridView.RowTemplate.Height = 25;
            this.sgMembersDataGridView.Size = new System.Drawing.Size(521, 466);
            this.sgMembersDataGridView.TabIndex = 0;
            // 
            // SpawnGroupDatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 555);
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
    }
}