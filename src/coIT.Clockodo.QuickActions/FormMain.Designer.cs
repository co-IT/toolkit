namespace coIT.Clockodo.QuickActions
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            tbpEinstellungen = new TabPage();
            groupBox4 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            groupBox3 = new GroupBox();
            groupBox1 = new GroupBox();
            ctrl_EmailAdresse = new TextBox();
            groupBox2 = new GroupBox();
            ctrl_ApiToken = new TextBox();
            ctrl_EinstellungenSpeichern = new Button();
            tbpErfassen = new TabPage();
            splitContainer1 = new SplitContainer();
            ctrl_laufendeUhrAbfrage = new Button();
            ctrl_textboxLaufenderEintrag = new TextBox();
            ctrl_ZeigeSelektiertenMitarbeiter = new Label();
            ctrl_LadeDaten = new Button();
            ctrl_Zeiteintraege = new DataGridView();
            tbcForms = new TabControl();
            tbpSelbstkontrolle = new TabPage();
            gbxZeitraumSchnellauswahl = new GroupBox();
            btnLetzteZweiWochenVormonat = new Button();
            btnLetzterMonat = new Button();
            btnErsteZweiWochenAktuellerMonat = new Button();
            gbxZeitraum = new GroupBox();
            lblStart = new Label();
            dtpZeitraumEnde = new DateTimePicker();
            lblEnde = new Label();
            dtpZeitraumStart = new DateTimePicker();
            btnFehlerAktualisieren = new Button();
            dgvClockodoFehler = new DataGridView();
            tbpEinstellungen.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            tbpErfassen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ctrl_Zeiteintraege).BeginInit();
            tbcForms.SuspendLayout();
            tbpSelbstkontrolle.SuspendLayout();
            gbxZeitraumSchnellauswahl.SuspendLayout();
            gbxZeitraum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClockodoFehler).BeginInit();
            SuspendLayout();
            // 
            // tbpEinstellungen
            // 
            tbpEinstellungen.Controls.Add(groupBox4);
            tbpEinstellungen.Controls.Add(groupBox3);
            tbpEinstellungen.Location = new Point(4, 32);
            tbpEinstellungen.Name = "tbpEinstellungen";
            tbpEinstellungen.Padding = new Padding(3);
            tbpEinstellungen.Size = new Size(1279, 669);
            tbpEinstellungen.TabIndex = 1;
            tbpEinstellungen.Text = "Einstellungen";
            tbpEinstellungen.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(linkLabel1);
            groupBox4.Controls.Add(label1);
            groupBox4.Location = new Point(408, 17);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(699, 296);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Anleitung";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 182);
            label6.Name = "label6";
            label6.Size = new Size(596, 25);
            label6.TabIndex = 6;
            label6.Text = "4. Klicke auf \"Einstellungen speichern\" um die Konfiguration abzuschließen";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 146);
            label5.Name = "label5";
            label5.Size = new Size(447, 25);
            label5.TabIndex = 5;
            label5.Text = "3. Füge den Api-Key Links in der Konfiguration links ein";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 110);
            label3.Name = "label3";
            label3.Size = new Size(454, 25);
            label3.TabIndex = 3;
            label3.Text = "2. Füge die E-Mail-Adresse in der Konfiguration links ein";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(41, 73);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(358, 25);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://my.clockodo.com/de/users/editself/";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 38);
            label1.Name = "label1";
            label1.Size = new Size(636, 25);
            label1.TabIndex = 0;
            label1.Text = "1. Öffne den folgenden Link um zu deinen Clockodo Einstellungen zu gelangen";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox1);
            groupBox3.Controls.Add(groupBox2);
            groupBox3.Controls.Add(ctrl_EinstellungenSpeichern);
            groupBox3.Location = new Point(16, 17);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(386, 296);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Zugänge konfigurieren";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ctrl_EmailAdresse);
            groupBox1.Location = new Point(25, 53);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(315, 66);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Deine E-Mail-Adresse";
            // 
            // ctrl_EmailAdresse
            // 
            ctrl_EmailAdresse.Location = new Point(6, 30);
            ctrl_EmailAdresse.Name = "ctrl_EmailAdresse";
            ctrl_EmailAdresse.Size = new Size(303, 31);
            ctrl_EmailAdresse.TabIndex = 6;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ctrl_ApiToken);
            groupBox2.Location = new Point(25, 140);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(315, 67);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Dein API-Key";
            // 
            // ctrl_ApiToken
            // 
            ctrl_ApiToken.Location = new Point(6, 30);
            ctrl_ApiToken.Name = "ctrl_ApiToken";
            ctrl_ApiToken.Size = new Size(303, 31);
            ctrl_ApiToken.TabIndex = 6;
            ctrl_ApiToken.UseSystemPasswordChar = true;
            // 
            // ctrl_EinstellungenSpeichern
            // 
            ctrl_EinstellungenSpeichern.BackColor = Color.FromArgb(255, 192, 128);
            ctrl_EinstellungenSpeichern.Location = new Point(62, 227);
            ctrl_EinstellungenSpeichern.Name = "ctrl_EinstellungenSpeichern";
            ctrl_EinstellungenSpeichern.Size = new Size(247, 57);
            ctrl_EinstellungenSpeichern.TabIndex = 5;
            ctrl_EinstellungenSpeichern.Text = "Einstellungen speichern";
            ctrl_EinstellungenSpeichern.UseVisualStyleBackColor = false;
            ctrl_EinstellungenSpeichern.Click += ctrl_EinstellungenSpeichern_Click;
            // 
            // tbpErfassen
            // 
            tbpErfassen.Controls.Add(splitContainer1);
            tbpErfassen.Location = new Point(4, 32);
            tbpErfassen.Name = "tbpErfassen";
            tbpErfassen.Padding = new Padding(3);
            tbpErfassen.Size = new Size(1279, 669);
            tbpErfassen.TabIndex = 0;
            tbpErfassen.Text = "Erfassen";
            tbpErfassen.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ctrl_laufendeUhrAbfrage);
            splitContainer1.Panel1.Controls.Add(ctrl_textboxLaufenderEintrag);
            splitContainer1.Panel1.Controls.Add(ctrl_ZeigeSelektiertenMitarbeiter);
            splitContainer1.Panel1.Controls.Add(ctrl_LadeDaten);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(ctrl_Zeiteintraege);
            splitContainer1.Size = new Size(1273, 663);
            splitContainer1.SplitterDistance = 94;
            splitContainer1.TabIndex = 0;
            // 
            // ctrl_laufendeUhrAbfrage
            // 
            ctrl_laufendeUhrAbfrage.BackColor = Color.FromArgb(255, 192, 128);
            ctrl_laufendeUhrAbfrage.Location = new Point(550, 3);
            ctrl_laufendeUhrAbfrage.Name = "ctrl_laufendeUhrAbfrage";
            ctrl_laufendeUhrAbfrage.Size = new Size(133, 70);
            ctrl_laufendeUhrAbfrage.TabIndex = 5;
            ctrl_laufendeUhrAbfrage.Text = "Laufende Uhr Aktualisieren";
            ctrl_laufendeUhrAbfrage.UseVisualStyleBackColor = false;
            ctrl_laufendeUhrAbfrage.Click += ctrl_laufendeUhrAbfrage_Click;
            // 
            // ctrl_textboxLaufenderEintrag
            // 
            ctrl_textboxLaufenderEintrag.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ctrl_textboxLaufenderEintrag.Location = new Point(689, 3);
            ctrl_textboxLaufenderEintrag.Multiline = true;
            ctrl_textboxLaufenderEintrag.Name = "ctrl_textboxLaufenderEintrag";
            ctrl_textboxLaufenderEintrag.ReadOnly = true;
            ctrl_textboxLaufenderEintrag.Size = new Size(581, 69);
            ctrl_textboxLaufenderEintrag.TabIndex = 4;
            ctrl_textboxLaufenderEintrag.TextAlign = HorizontalAlignment.Right;
            // 
            // ctrl_ZeigeSelektiertenMitarbeiter
            // 
            ctrl_ZeigeSelektiertenMitarbeiter.Location = new Point(3, 3);
            ctrl_ZeigeSelektiertenMitarbeiter.Name = "ctrl_ZeigeSelektiertenMitarbeiter";
            ctrl_ZeigeSelektiertenMitarbeiter.Size = new Size(293, 33);
            ctrl_ZeigeSelektiertenMitarbeiter.TabIndex = 2;
            ctrl_ZeigeSelektiertenMitarbeiter.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ctrl_LadeDaten
            // 
            ctrl_LadeDaten.BackColor = Color.FromArgb(255, 192, 128);
            ctrl_LadeDaten.Location = new Point(3, 39);
            ctrl_LadeDaten.Name = "ctrl_LadeDaten";
            ctrl_LadeDaten.Size = new Size(293, 33);
            ctrl_LadeDaten.TabIndex = 1;
            ctrl_LadeDaten.Text = "Vergangene Zeiteinträge abrufen";
            ctrl_LadeDaten.UseVisualStyleBackColor = false;
            ctrl_LadeDaten.Click += ctrl_LadeDaten_Click;
            // 
            // ctrl_Zeiteintraege
            // 
            ctrl_Zeiteintraege.AllowUserToOrderColumns = true;
            ctrl_Zeiteintraege.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ctrl_Zeiteintraege.Dock = DockStyle.Fill;
            ctrl_Zeiteintraege.Location = new Point(0, 0);
            ctrl_Zeiteintraege.Name = "ctrl_Zeiteintraege";
            ctrl_Zeiteintraege.RowTemplate.Height = 25;
            ctrl_Zeiteintraege.Size = new Size(1273, 565);
            ctrl_Zeiteintraege.TabIndex = 0;
            ctrl_Zeiteintraege.CellDoubleClick += ctrl_Zeiteintraege_CellDoubleClick;
            ctrl_Zeiteintraege.KeyDown += ctrl_Zeiteintraege_KeyDown;
            // 
            // tbcForms
            // 
            tbcForms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbcForms.Controls.Add(tbpSelbstkontrolle);
            tbcForms.Controls.Add(tbpErfassen);
            tbcForms.Controls.Add(tbpEinstellungen);
            tbcForms.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            tbcForms.Location = new Point(2, 9);
            tbcForms.Name = "tbcForms";
            tbcForms.SelectedIndex = 0;
            tbcForms.Size = new Size(1287, 705);
            tbcForms.TabIndex = 5;
            // 
            // tbpSelbstkontrolle
            // 
            tbpSelbstkontrolle.Controls.Add(gbxZeitraumSchnellauswahl);
            tbpSelbstkontrolle.Controls.Add(gbxZeitraum);
            tbpSelbstkontrolle.Controls.Add(btnFehlerAktualisieren);
            tbpSelbstkontrolle.Controls.Add(dgvClockodoFehler);
            tbpSelbstkontrolle.Location = new Point(4, 32);
            tbpSelbstkontrolle.Name = "tbpSelbstkontrolle";
            tbpSelbstkontrolle.Padding = new Padding(3);
            tbpSelbstkontrolle.Size = new Size(1279, 669);
            tbpSelbstkontrolle.TabIndex = 2;
            tbpSelbstkontrolle.Text = "Selbstkontrolle";
            tbpSelbstkontrolle.UseVisualStyleBackColor = true;
            // 
            // gbxZeitraumSchnellauswahl
            // 
            gbxZeitraumSchnellauswahl.Controls.Add(btnLetzteZweiWochenVormonat);
            gbxZeitraumSchnellauswahl.Controls.Add(btnLetzterMonat);
            gbxZeitraumSchnellauswahl.Controls.Add(btnErsteZweiWochenAktuellerMonat);
            gbxZeitraumSchnellauswahl.Location = new Point(408, 6);
            gbxZeitraumSchnellauswahl.Name = "gbxZeitraumSchnellauswahl";
            gbxZeitraumSchnellauswahl.Size = new Size(385, 120);
            gbxZeitraumSchnellauswahl.TabIndex = 8;
            gbxZeitraumSchnellauswahl.TabStop = false;
            gbxZeitraumSchnellauswahl.Text = "Zeitraum Schnellauswahl";
            // 
            // btnLetzteZweiWochenVormonat
            // 
            btnLetzteZweiWochenVormonat.Location = new Point(6, 30);
            btnLetzteZweiWochenVormonat.Name = "btnLetzteZweiWochenVormonat";
            btnLetzteZweiWochenVormonat.Size = new Size(182, 39);
            btnLetzteZweiWochenVormonat.TabIndex = 11;
            btnLetzteZweiWochenVormonat.Text = "---";
            btnLetzteZweiWochenVormonat.UseVisualStyleBackColor = true;
            btnLetzteZweiWochenVormonat.Click += btnLetzteZweiWochenVormonat_Click;
            // 
            // btnLetzterMonat
            // 
            btnLetzterMonat.Location = new Point(6, 75);
            btnLetzterMonat.Name = "btnLetzterMonat";
            btnLetzterMonat.Size = new Size(182, 39);
            btnLetzterMonat.TabIndex = 10;
            btnLetzterMonat.Text = "---";
            btnLetzterMonat.UseVisualStyleBackColor = true;
            btnLetzterMonat.Click += btnLetzterMonat_Click;
            // 
            // btnErsteZweiWochenAktuellerMonat
            // 
            btnErsteZweiWochenAktuellerMonat.Location = new Point(194, 30);
            btnErsteZweiWochenAktuellerMonat.Name = "btnErsteZweiWochenAktuellerMonat";
            btnErsteZweiWochenAktuellerMonat.Size = new Size(182, 39);
            btnErsteZweiWochenAktuellerMonat.TabIndex = 9;
            btnErsteZweiWochenAktuellerMonat.Text = "---";
            btnErsteZweiWochenAktuellerMonat.UseVisualStyleBackColor = true;
            btnErsteZweiWochenAktuellerMonat.Click += btnErsteZweiWochenAktuellerMonat_Click;
            // 
            // gbxZeitraum
            // 
            gbxZeitraum.Controls.Add(lblStart);
            gbxZeitraum.Controls.Add(dtpZeitraumEnde);
            gbxZeitraum.Controls.Add(lblEnde);
            gbxZeitraum.Controls.Add(dtpZeitraumStart);
            gbxZeitraum.Location = new Point(6, 6);
            gbxZeitraum.Name = "gbxZeitraum";
            gbxZeitraum.Size = new Size(396, 120);
            gbxZeitraum.TabIndex = 7;
            gbxZeitraum.TabStop = false;
            gbxZeitraum.Text = "Zeitraum auswählen";
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(10, 37);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(52, 25);
            lblStart.TabIndex = 4;
            lblStart.Text = "Start:";
            // 
            // dtpZeitraumEnde
            // 
            dtpZeitraumEnde.Location = new Point(68, 78);
            dtpZeitraumEnde.Name = "dtpZeitraumEnde";
            dtpZeitraumEnde.Size = new Size(313, 31);
            dtpZeitraumEnde.TabIndex = 2;
            dtpZeitraumEnde.ValueChanged += dtpZeitraumEnde_ValueChanged;
            // 
            // lblEnde
            // 
            lblEnde.AutoSize = true;
            lblEnde.Location = new Point(10, 79);
            lblEnde.Name = "lblEnde";
            lblEnde.Size = new Size(55, 25);
            lblEnde.TabIndex = 5;
            lblEnde.Text = "Ende:";
            // 
            // dtpZeitraumStart
            // 
            dtpZeitraumStart.CustomFormat = "";
            dtpZeitraumStart.Location = new Point(68, 34);
            dtpZeitraumStart.Name = "dtpZeitraumStart";
            dtpZeitraumStart.RightToLeft = RightToLeft.No;
            dtpZeitraumStart.Size = new Size(313, 31);
            dtpZeitraumStart.TabIndex = 3;
            dtpZeitraumStart.ValueChanged += dtpZeitraumStart_ValueChanged;
            // 
            // btnFehlerAktualisieren
            // 
            btnFehlerAktualisieren.Location = new Point(799, 17);
            btnFehlerAktualisieren.Name = "btnFehlerAktualisieren";
            btnFehlerAktualisieren.Size = new Size(138, 109);
            btnFehlerAktualisieren.TabIndex = 1;
            btnFehlerAktualisieren.Text = "Aktualisieren";
            btnFehlerAktualisieren.UseVisualStyleBackColor = true;
            btnFehlerAktualisieren.Click += btnFehlerAktualisiere_click;
            // 
            // dgvClockodoFehler
            // 
            dgvClockodoFehler.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvClockodoFehler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClockodoFehler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClockodoFehler.Location = new Point(3, 132);
            dgvClockodoFehler.Name = "dgvClockodoFehler";
            dgvClockodoFehler.RowTemplate.Height = 25;
            dgvClockodoFehler.Size = new Size(1273, 534);
            dgvClockodoFehler.TabIndex = 0;
            dgvClockodoFehler.CellDoubleClick += dgvClockodoFehler_CellDoubleClick;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1293, 726);
            Controls.Add(tbcForms);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMain";
            Text = "co-IT.eu GmbH | Clockodo Quick Actions";
            Load += FormMain_Load;
            tbpEinstellungen.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tbpErfassen.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ctrl_Zeiteintraege).EndInit();
            tbcForms.ResumeLayout(false);
            tbpSelbstkontrolle.ResumeLayout(false);
            gbxZeitraumSchnellauswahl.ResumeLayout(false);
            gbxZeitraum.ResumeLayout(false);
            gbxZeitraum.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClockodoFehler).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabPage tbpEinstellungen;
        private GroupBox groupBox2;
        private TextBox ctrl_ApiToken;
        private GroupBox groupBox1;
        private TextBox ctrl_EmailAdresse;
        private Button ctrl_EinstellungenSpeichern;
        private TabPage tbpErfassen;
        private SplitContainer splitContainer1;
        private Label ctrl_ZeigeSelektiertenMitarbeiter;
        private Button ctrl_LadeDaten;
        private DataGridView ctrl_Zeiteintraege;
        private TabControl tbcForms;
        private TextBox ctrl_textboxLaufenderEintrag;
        private Button ctrl_laufendeUhrAbfrage;
        private TabPage tbpSelbstkontrolle;
        private DataGridView dgvClockodoFehler;
        private Button btnFehlerAktualisieren;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dtpZeitraumEnde;
        private Label lblStart;
        private DateTimePicker dtpZeitraumStart;
        private Label lblEnde;
        private GroupBox gbxZeitraum;
        private GroupBox gbxZeitraumSchnellauswahl;
        private Button btnLetzteZweiWochenVormonat;
        private Button btnLetzterMonat;
        private Button btnErsteZweiWochenAktuellerMonat;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private LinkLabel linkLabel1;
        private Label label1;
        private Label label6;
        private Label label5;
        private Label label3;
    }
}
