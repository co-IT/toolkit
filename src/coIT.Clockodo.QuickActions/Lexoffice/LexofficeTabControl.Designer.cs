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
            tbpRechnungSelbstkontrolle = new TabPage();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tbpRechnungSelbstkontrolle);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(853, 479);
            tabControl1.TabIndex = 0;
            // 
            // tbpRechnungSelbstkontrolle
            // 
            tbpRechnungSelbstkontrolle.Location = new Point(4, 24);
            tbpRechnungSelbstkontrolle.Name = "tbpRechnungSelbstkontrolle";
            tbpRechnungSelbstkontrolle.Padding = new Padding(3);
            tbpRechnungSelbstkontrolle.Size = new Size(845, 451);
            tbpRechnungSelbstkontrolle.TabIndex = 0;
            tbpRechnungSelbstkontrolle.Text = "Rechnung Selbstkontrolle";
            tbpRechnungSelbstkontrolle.UseVisualStyleBackColor = true;
            // 
            // LexofficeTabControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Name = "LexofficeTabControl";
            Size = new Size(853, 479);
            Load += LexofficeTabControl_Load;
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tbpRechnungSelbstkontrolle;
        private LexofficeRechnungskontrolle lexofficeRechnungskontrolle1;
    }
}
