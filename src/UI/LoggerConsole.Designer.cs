namespace KouCoCoa
{
    partial class LoggerConsole
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
            this.loggerTextBox = new System.Windows.Forms.TextBox();
            this.headerTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logLevelComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel.SuspendLayout();
            this.headerTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.loggerTextBox, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.headerTableLayoutPanel, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.666667F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.33334F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // loggerTextBox
            // 
            this.loggerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loggerTextBox.Location = new System.Drawing.Point(3, 42);
            this.loggerTextBox.Multiline = true;
            this.loggerTextBox.Name = "loggerTextBox";
            this.loggerTextBox.ReadOnly = true;
            this.loggerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.loggerTextBox.Size = new System.Drawing.Size(794, 405);
            this.loggerTextBox.TabIndex = 0;
            // 
            // headerTableLayoutPanel
            // 
            this.headerTableLayoutPanel.AutoSize = true;
            this.headerTableLayoutPanel.ColumnCount = 2;
            this.headerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.58186F));
            this.headerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.41814F));
            this.headerTableLayoutPanel.Controls.Add(this.logLevelComboBox, 0, 0);
            this.headerTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.headerTableLayoutPanel.Name = "headerTableLayoutPanel";
            this.headerTableLayoutPanel.RowCount = 1;
            this.headerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.headerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.headerTableLayoutPanel.Size = new System.Drawing.Size(794, 33);
            this.headerTableLayoutPanel.TabIndex = 1;
            // 
            // logLevelComboBox
            // 
            this.logLevelComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logLevelComboBox.FormattingEnabled = true;
            this.logLevelComboBox.Location = new System.Drawing.Point(3, 3);
            this.logLevelComboBox.Name = "logLevelComboBox";
            this.logLevelComboBox.Size = new System.Drawing.Size(212, 23);
            this.logLevelComboBox.TabIndex = 0;
            this.logLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.logLevelComboBox_SelectedIndexChanged);
            // 
            // LoggerConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "LoggerConsole";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.headerTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox loggerTextBox;
        private System.Windows.Forms.TableLayoutPanel headerTableLayoutPanel;
        private System.Windows.Forms.ComboBox logLevelComboBox;
    }
}