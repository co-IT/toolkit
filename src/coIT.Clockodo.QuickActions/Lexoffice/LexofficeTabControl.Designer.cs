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
            tbcLexoffice = new TabControl();
            tbpRechnungSelbstkontrolle = new TabPage();
            tbpPositionsGenerator = new TabPage();
            tbcLexoffice.SuspendLayout();
            SuspendLayout();
            // 
            // tbcLexoffice
            // 
            tbcLexoffice.Controls.Add(tbpRechnungSelbstkontrolle);
            tbcLexoffice.Controls.Add(tbpPositionsGenerator);
            tbcLexoffice.Dock = DockStyle.Fill;
            tbcLexoffice.Location = new Point(0, 0);
            tbcLexoffice.Name = "tbcLexoffice";
            tbcLexoffice.SelectedIndex = 0;
            tbcLexoffice.Size = new Size(853, 479);
            tbcLexoffice.TabIndex = 0;
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
            // tbpPositionsGenerator
            // 
            tbpPositionsGenerator.Location = new Point(4, 24);
            tbpPositionsGenerator.Name = "tbpPositionsGenerator";
            tbpPositionsGenerator.Padding = new Padding(3);
            tbpPositionsGenerator.Size = new Size(845, 451);
            tbpPositionsGenerator.TabIndex = 1;
            tbpPositionsGenerator.Text = "Positions Generator";
            tbpPositionsGenerator.UseVisualStyleBackColor = true;
            // 
            // LexofficeTabControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tbcLexoffice);
            Name = "LexofficeTabControl";
            Size = new Size(853, 479);
            Load += LexofficeTabControl_Load;
            tbcLexoffice.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tbcLexoffice;
        private TabPage tbpRechnungSelbstkontrolle;
        private LexofficeRechnungskontrolle lexofficeRechnungskontrolle1;
        private TabPage tbpPositionsGenerator;
    }
}
