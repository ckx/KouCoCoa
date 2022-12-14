namespace KouCoCoa
{
    internal partial class DatabaseOrganizer
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
            this.dbOrganizerTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.databaseTypeSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.dbOrangizerListBox = new System.Windows.Forms.ListBox();
            this.dbOrganizerTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbOrganizerTableLayoutPanel
            // 
            this.dbOrganizerTableLayoutPanel.ColumnCount = 3;
            this.dbOrganizerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.5F));
            this.dbOrganizerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.5F));
            this.dbOrganizerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 352F));
            this.dbOrganizerTableLayoutPanel.Controls.Add(this.databaseTypeSelectorComboBox, 0, 0);
            this.dbOrganizerTableLayoutPanel.Controls.Add(this.dbOrangizerListBox, 0, 1);
            this.dbOrganizerTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbOrganizerTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.dbOrganizerTableLayoutPanel.Name = "dbOrganizerTableLayoutPanel";
            this.dbOrganizerTableLayoutPanel.RowCount = 3;
            this.dbOrganizerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.17318F));
            this.dbOrganizerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.82681F));
            this.dbOrganizerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.dbOrganizerTableLayoutPanel.Size = new System.Drawing.Size(800, 450);
            this.dbOrganizerTableLayoutPanel.TabIndex = 0;
            // 
            // databaseTypeSelectorComboBox
            // 
            this.databaseTypeSelectorComboBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.databaseTypeSelectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databaseTypeSelectorComboBox.FormattingEnabled = true;
            this.databaseTypeSelectorComboBox.Location = new System.Drawing.Point(9, 14);
            this.databaseTypeSelectorComboBox.Name = "databaseTypeSelectorComboBox";
            this.databaseTypeSelectorComboBox.Size = new System.Drawing.Size(207, 23);
            this.databaseTypeSelectorComboBox.TabIndex = 0;
            this.databaseTypeSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.databaseTypeSelectorComboBox_SelectedIndexChanged);
            // 
            // dbOrangizerListBox
            // 
            this.dbOrangizerListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbOrangizerListBox.FormattingEnabled = true;
            this.dbOrangizerListBox.ItemHeight = 15;
            this.dbOrangizerListBox.Location = new System.Drawing.Point(3, 43);
            this.dbOrangizerListBox.Name = "dbOrangizerListBox";
            this.dbOrangizerListBox.Size = new System.Drawing.Size(220, 312);
            this.dbOrangizerListBox.TabIndex = 1;
            // 
            // DatabaseOrganizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dbOrganizerTableLayoutPanel);
            this.Name = "DatabaseOrganizer";
            this.Text = "DatabaseOrganizer";
            this.dbOrganizerTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel dbOrganizerTableLayoutPanel;
        private System.Windows.Forms.ComboBox databaseTypeSelectorComboBox;
        private System.Windows.Forms.ListBox dbOrangizerListBox;
    }
}