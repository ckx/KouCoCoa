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
            this.mobList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // mobList
            // 
            this.mobList.FormattingEnabled = true;
            this.mobList.ItemHeight = 41;
            this.mobList.Location = new System.Drawing.Point(12, 73);
            this.mobList.Name = "mobList";
            this.mobList.Size = new System.Drawing.Size(305, 1070);
            this.mobList.TabIndex = 0;
            // 
            // MobDatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1943, 1230);
            this.Controls.Add(this.mobList);
            this.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.Name = "MobDatabaseEditor";
            this.Text = "MobDatabaseEditor";
            this.Load += new System.EventHandler(this.MobDatabaseEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox mobList;
    }
}