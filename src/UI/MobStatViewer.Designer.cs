namespace KouCoCoa
{
    partial class MobStatViewer
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.headerTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.classLabel = new System.Windows.Forms.Label();
            this.mobClassComboBox = new System.Windows.Forms.ComboBox();
            this.mobRaceLabel = new System.Windows.Forms.Label();
            this.mobRaceComboBox = new System.Windows.Forms.ComboBox();
            this.mobElementLabel = new System.Windows.Forms.Label();
            this.mobElementComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.headerTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Controls.Add(this.dataGridView, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.headerTableLayoutPanel, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.337349F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.66265F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(806, 664);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 64);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(800, 597);
            this.dataGridView.TabIndex = 0;
            // 
            // headerTableLayoutPanel
            // 
            this.headerTableLayoutPanel.ColumnCount = 4;
            this.headerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.625F));
            this.headerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.headerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.headerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.375F));
            this.headerTableLayoutPanel.Controls.Add(this.classLabel, 0, 0);
            this.headerTableLayoutPanel.Controls.Add(this.mobClassComboBox, 0, 1);
            this.headerTableLayoutPanel.Controls.Add(this.mobRaceLabel, 1, 0);
            this.headerTableLayoutPanel.Controls.Add(this.mobRaceComboBox, 1, 1);
            this.headerTableLayoutPanel.Controls.Add(this.mobElementLabel, 2, 0);
            this.headerTableLayoutPanel.Controls.Add(this.mobElementComboBox, 2, 1);
            this.headerTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.headerTableLayoutPanel.Name = "headerTableLayoutPanel";
            this.headerTableLayoutPanel.RowCount = 2;
            this.headerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.headerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.headerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.headerTableLayoutPanel.Size = new System.Drawing.Size(800, 55);
            this.headerTableLayoutPanel.TabIndex = 1;
            // 
            // classLabel
            // 
            this.classLabel.AutoSize = true;
            this.classLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classLabel.Location = new System.Drawing.Point(3, 0);
            this.classLabel.Name = "classLabel";
            this.classLabel.Size = new System.Drawing.Size(115, 19);
            this.classLabel.TabIndex = 0;
            this.classLabel.Text = "Class";
            this.classLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // mobClassComboBox
            // 
            this.mobClassComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mobClassComboBox.FormattingEnabled = true;
            this.mobClassComboBox.Location = new System.Drawing.Point(3, 22);
            this.mobClassComboBox.Name = "mobClassComboBox";
            this.mobClassComboBox.Size = new System.Drawing.Size(105, 23);
            this.mobClassComboBox.TabIndex = 1;
            this.mobClassComboBox.SelectedIndexChanged += new System.EventHandler(this.classComboBox_SelectedIndexChanged);
            // 
            // mobRaceLabel
            // 
            this.mobRaceLabel.AutoSize = true;
            this.mobRaceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobRaceLabel.Location = new System.Drawing.Point(124, 0);
            this.mobRaceLabel.Name = "mobRaceLabel";
            this.mobRaceLabel.Size = new System.Drawing.Size(124, 19);
            this.mobRaceLabel.TabIndex = 2;
            this.mobRaceLabel.Text = "Race";
            this.mobRaceLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // mobRaceComboBox
            // 
            this.mobRaceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mobRaceComboBox.FormattingEnabled = true;
            this.mobRaceComboBox.Location = new System.Drawing.Point(124, 22);
            this.mobRaceComboBox.Name = "mobRaceComboBox";
            this.mobRaceComboBox.Size = new System.Drawing.Size(121, 23);
            this.mobRaceComboBox.TabIndex = 3;
            this.mobRaceComboBox.SelectedIndexChanged += new System.EventHandler(this.mobRaceComboBox_SelectedIndexChanged);
            // 
            // mobElementLabel
            // 
            this.mobElementLabel.AutoSize = true;
            this.mobElementLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobElementLabel.Location = new System.Drawing.Point(254, 0);
            this.mobElementLabel.Name = "mobElementLabel";
            this.mobElementLabel.Size = new System.Drawing.Size(101, 19);
            this.mobElementLabel.TabIndex = 4;
            this.mobElementLabel.Text = "Element";
            this.mobElementLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // mobElementComboBox
            // 
            this.mobElementComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobElementComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mobElementComboBox.FormattingEnabled = true;
            this.mobElementComboBox.Location = new System.Drawing.Point(254, 22);
            this.mobElementComboBox.Name = "mobElementComboBox";
            this.mobElementComboBox.Size = new System.Drawing.Size(101, 23);
            this.mobElementComboBox.TabIndex = 5;
            this.mobElementComboBox.SelectedIndexChanged += new System.EventHandler(this.mobElementComboBox_SelectedIndexChanged);
            // 
            // MobStatViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 664);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MobStatViewer";
            this.Text = "MobStatViewer";
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.headerTableLayoutPanel.ResumeLayout(false);
            this.headerTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TableLayoutPanel headerTableLayoutPanel;
        private System.Windows.Forms.Label classLabel;
        private System.Windows.Forms.ComboBox mobClassComboBox;
        private System.Windows.Forms.Label mobRaceLabel;
        private System.Windows.Forms.ComboBox mobRaceComboBox;
        private System.Windows.Forms.Label mobElementLabel;
        private System.Windows.Forms.ComboBox mobElementComboBox;
    }
}