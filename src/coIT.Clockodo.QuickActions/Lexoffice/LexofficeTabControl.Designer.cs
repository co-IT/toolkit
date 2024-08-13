namespace coIT.Clockodo.QuickActions.Lexoffice
{
    partial class LexofficeTabControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            lexofficeRechnungskontrolle1 = new LexofficeRechnungskontrolle();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(853, 479);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(lexofficeRechnungskontrolle1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(845, 451);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Rechnung Selbstkontrolle";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // lexofficeRechnungskontrolle1
            // 
            lexofficeRechnungskontrolle1.Dock = DockStyle.Fill;
            lexofficeRechnungskontrolle1.Location = new Point(3, 3);
            lexofficeRechnungskontrolle1.Name = "lexofficeRechnungskontrolle1";
            lexofficeRechnungskontrolle1.Size = new Size(839, 445);
            lexofficeRechnungskontrolle1.TabIndex = 0;
            // 
            // LexofficeTabControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Name = "LexofficeTabControl";
            Size = new Size(853, 479);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private LexofficeRechnungskontrolle lexofficeRechnungskontrolle1;
    }
}
