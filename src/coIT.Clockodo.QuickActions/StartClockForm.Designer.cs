namespace coIT.Clockodo.QuickActions
{
    partial class EditTimeEntry
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrl_StartClock = new System.Windows.Forms.Button();
            this.ctrl_Kunde = new System.Windows.Forms.TextBox();
            this.ctrl_Leistung = new System.Windows.Forms.TextBox();
            this.ctrl_Projekt = new System.Windows.Forms.TextBox();
            this.ctrl_Beschreibung = new System.Windows.Forms.TextBox();
            this.lbl_Kunde = new System.Windows.Forms.Label();
            this.lbl_Leistung = new System.Windows.Forms.Label();
            this.lbl_Projekt = new System.Windows.Forms.Label();
            this.lbl_Beschreibung = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrl_StartClock
            // 
            this.ctrl_StartClock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ctrl_StartClock.Location = new System.Drawing.Point(111, 289);
            this.ctrl_StartClock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrl_StartClock.Name = "ctrl_StartClock";
            this.ctrl_StartClock.Size = new System.Drawing.Size(246, 41);
            this.ctrl_StartClock.TabIndex = 0;
            this.ctrl_StartClock.Text = "Uhr starten";
            this.ctrl_StartClock.UseVisualStyleBackColor = false;
            this.ctrl_StartClock.Click += new System.EventHandler(this.ctrl_StartClock_Click);
            // 
            // ctrl_Kunde
            // 
            this.ctrl_Kunde.Location = new System.Drawing.Point(118, 26);
            this.ctrl_Kunde.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrl_Kunde.Name = "ctrl_Kunde";
            this.ctrl_Kunde.ReadOnly = true;
            this.ctrl_Kunde.Size = new System.Drawing.Size(245, 27);
            this.ctrl_Kunde.TabIndex = 2;
            // 
            // ctrl_Leistung
            // 
            this.ctrl_Leistung.Location = new System.Drawing.Point(118, 84);
            this.ctrl_Leistung.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrl_Leistung.Name = "ctrl_Leistung";
            this.ctrl_Leistung.ReadOnly = true;
            this.ctrl_Leistung.Size = new System.Drawing.Size(245, 27);
            this.ctrl_Leistung.TabIndex = 3;
            // 
            // ctrl_Projekt
            // 
            this.ctrl_Projekt.Location = new System.Drawing.Point(118, 134);
            this.ctrl_Projekt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrl_Projekt.Name = "ctrl_Projekt";
            this.ctrl_Projekt.ReadOnly = true;
            this.ctrl_Projekt.Size = new System.Drawing.Size(245, 27);
            this.ctrl_Projekt.TabIndex = 4;
            // 
            // ctrl_Beschreibung
            // 
            this.ctrl_Beschreibung.Location = new System.Drawing.Point(118, 177);
            this.ctrl_Beschreibung.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrl_Beschreibung.Multiline = true;
            this.ctrl_Beschreibung.Name = "ctrl_Beschreibung";
            this.ctrl_Beschreibung.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ctrl_Beschreibung.Size = new System.Drawing.Size(245, 88);
            this.ctrl_Beschreibung.TabIndex = 0;
            this.ctrl_Beschreibung.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctrl_Beschreibung_KeyDown);
            // 
            // lbl_Kunde
            // 
            this.lbl_Kunde.AutoSize = true;
            this.lbl_Kunde.Location = new System.Drawing.Point(14, 33);
            this.lbl_Kunde.Name = "lbl_Kunde";
            this.lbl_Kunde.Size = new System.Drawing.Size(51, 20);
            this.lbl_Kunde.TabIndex = 6;
            this.lbl_Kunde.Text = "Kunde";
            // 
            // lbl_Leistung
            // 
            this.lbl_Leistung.AutoSize = true;
            this.lbl_Leistung.Location = new System.Drawing.Point(14, 91);
            this.lbl_Leistung.Name = "lbl_Leistung";
            this.lbl_Leistung.Size = new System.Drawing.Size(64, 20);
            this.lbl_Leistung.TabIndex = 7;
            this.lbl_Leistung.Text = "Leistung";
            // 
            // lbl_Projekt
            // 
            this.lbl_Projekt.AutoSize = true;
            this.lbl_Projekt.Location = new System.Drawing.Point(14, 141);
            this.lbl_Projekt.Name = "lbl_Projekt";
            this.lbl_Projekt.Size = new System.Drawing.Size(55, 20);
            this.lbl_Projekt.TabIndex = 8;
            this.lbl_Projekt.Text = "Projekt";
            // 
            // lbl_Beschreibung
            // 
            this.lbl_Beschreibung.AutoSize = true;
            this.lbl_Beschreibung.Location = new System.Drawing.Point(14, 191);
            this.lbl_Beschreibung.Name = "lbl_Beschreibung";
            this.lbl_Beschreibung.Size = new System.Drawing.Size(98, 20);
            this.lbl_Beschreibung.TabIndex = 9;
            this.lbl_Beschreibung.Text = "Beschreibung";
            // 
            // EditTimeEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.lbl_Beschreibung);
            this.Controls.Add(this.lbl_Projekt);
            this.Controls.Add(this.lbl_Leistung);
            this.Controls.Add(this.lbl_Kunde);
            this.Controls.Add(this.ctrl_Beschreibung);
            this.Controls.Add(this.ctrl_Projekt);
            this.Controls.Add(this.ctrl_Leistung);
            this.Controls.Add(this.ctrl_Kunde);
            this.Controls.Add(this.ctrl_StartClock);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "EditTimeEntry";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Uhr starten";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ctrl_StartClock;
        private TextBox ctrl_Kunde;
        private TextBox ctrl_Leistung;
        private TextBox ctrl_Projekt;
        private Label lbl_Kunde;
        private Label lbl_Leistung;
        private Label lbl_Projekt;
        private Label lbl_Beschreibung;
        public TextBox ctrl_Beschreibung;
    }
}