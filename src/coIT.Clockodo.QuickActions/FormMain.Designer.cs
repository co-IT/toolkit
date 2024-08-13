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
            groupBox5 = new GroupBox();
            groupBox6 = new GroupBox();
            groupBox7 = new GroupBox();
            textBox1 = new TextBox();
            gbxClockodoSettings = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox1 = new GroupBox();
            ctrl_EmailAdresse = new TextBox();
            groupBox2 = new GroupBox();
            ctrl_ApiToken = new TextBox();
            groupBox4 = new GroupBox();
            label5 = new Label();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            ctrl_EinstellungenSpeichern = new Button();
            tbpErfassen = new TabPage();
            splitContainer1 = new SplitContainer();
            ctrl_laufendeUhrAbfrage = new Button();
            ctrl_textboxLaufenderEintrag = new TextBox();
            ctrl_ZeigeSelektiertenMitarbeiter = new Label();
            ctrl_LadeDaten = new Button();
            ctrl_Zeiteintraege = new DataGridView();
            tbcForms = new TabControl();
            tbpClockodo = new TabPage();
            tbcClockodo = new TabControl();
            tbpClockodoSelbstkontrolle = new TabPage();
            dgvClockodoFehler = new DataGridView();
            btnFehlerAktualisieren = new Button();
            gbxZeitraumSchnellauswahl = new GroupBox();
            btnLetzteZweiWochenVormonat = new Button();
            btnLetzterMonat = new Button();
            btnErsteZweiWochenAktuellerMonat = new Button();
            gbxZeitraum = new GroupBox();
            lblStart = new Label();
            dtpZeitraumEnde = new DateTimePicker();
            lblEnde = new Label();
            dtpZeitraumStart = new DateTimePicker();
            tbpLexoffice = new TabPage();
            lexofficeTabControl1 = new Lexoffice.LexofficeTabControl();
            tbpEinstellungen.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            gbxClockodoSettings.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            tbpErfassen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ctrl_Zeiteintraege).BeginInit();
            tbcForms.SuspendLayout();
            tbpClockodo.SuspendLayout();
            tbcClockodo.SuspendLayout();
            tbpClockodoSelbstkontrolle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClockodoFehler).BeginInit();
            gbxZeitraumSchnellauswahl.SuspendLayout();
            gbxZeitraum.SuspendLayout();
            tbpLexoffice.SuspendLayout();
            SuspendLayout();
            // 
            // tbpEinstellungen
            // 
            tbpEinstellungen.Controls.Add(groupBox5);
            tbpEinstellungen.Controls.Add(gbxClockodoSettings);
            tbpEinstellungen.Controls.Add(ctrl_EinstellungenSpeichern);
            tbpEinstellungen.Location = new Point(4, 32);
            tbpEinstellungen.Name = "tbpEinstellungen";
            tbpEinstellungen.Padding = new Padding(3);
            tbpEinstellungen.Size = new Size(1279, 669);
            tbpEinstellungen.TabIndex = 1;
            tbpEinstellungen.Text = "Einstellungen";
            tbpEinstellungen.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(groupBox6);
            groupBox5.Location = new Point(6, 296);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(1151, 206);
            groupBox5.TabIndex = 12;
            groupBox5.TabStop = false;
            groupBox5.Text = "Lexoffice";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(groupBox7);
            groupBox6.Location = new Point(20, 45);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(386, 140);
            groupBox6.TabIndex = 10;
            groupBox6.TabStop = false;
            groupBox6.Text = "Zugänge konfigurieren";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(textBox1);
            groupBox7.Location = new Point(25, 46);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(315, 66);
            groupBox7.TabIndex = 7;
            groupBox7.TabStop = false;
            groupBox7.Text = "Lexoffice Api Token";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(6, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(303, 31);
            textBox1.TabIndex = 6;
            // 
            // gbxClockodoSettings
            // 
            gbxClockodoSettings.Controls.Add(groupBox3);
            gbxClockodoSettings.Controls.Add(groupBox4);
            gbxClockodoSettings.Location = new Point(6, 6);
            gbxClockodoSettings.Name = "gbxClockodoSettings";
            gbxClockodoSettings.Size = new Size(1151, 284);
            gbxClockodoSettings.TabIndex = 11;
            gbxClockodoSettings.TabStop = false;
            gbxClockodoSettings.Text = "Clockodo";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox1);
            groupBox3.Controls.Add(groupBox2);
            groupBox3.Location = new Point(20, 42);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(386, 221);
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
            // groupBox4
            // 
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(linkLabel1);
            groupBox4.Controls.Add(label1);
            groupBox4.Location = new Point(423, 42);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(699, 221);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Anleitung";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 168);
            label5.Name = "label5";
            label5.Size = new Size(447, 25);
            label5.TabIndex = 5;
            label5.Text = "3. Füge den Api-Key Links in der Konfiguration links ein";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 122);
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
            label1.Location = new Point(13, 38);
            label1.Name = "label1";
            label1.Size = new Size(636, 25);
            label1.TabIndex = 0;
            label1.Text = "1. Öffne den folgenden Link um zu deinen Clockodo Einstellungen zu gelangen";
            // 
            // ctrl_EinstellungenSpeichern
            // 
            ctrl_EinstellungenSpeichern.BackColor = Color.FromArgb(255, 192, 128);
            ctrl_EinstellungenSpeichern.Location = new Point(910, 539);
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
            ctrl_Zeiteintraege.Size = new Size(1273, 565);
            ctrl_Zeiteintraege.TabIndex = 0;
            ctrl_Zeiteintraege.CellDoubleClick += ctrl_Zeiteintraege_CellDoubleClick;
            ctrl_Zeiteintraege.KeyDown += ctrl_Zeiteintraege_KeyDown;
            // 
            // tbcForms
            // 
            tbcForms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbcForms.Controls.Add(tbpClockodo);
            tbcForms.Controls.Add(tbpLexoffice);
            tbcForms.Controls.Add(tbpErfassen);
            tbcForms.Controls.Add(tbpEinstellungen);
            tbcForms.Font = new Font("Segoe UI", 13F);
            tbcForms.Location = new Point(2, 9);
            tbcForms.Name = "tbcForms";
            tbcForms.SelectedIndex = 0;
            tbcForms.Size = new Size(1287, 705);
            tbcForms.TabIndex = 5;
            // 
            // tbpClockodo
            // 
            tbpClockodo.Controls.Add(tbcClockodo);
            tbpClockodo.Location = new Point(4, 32);
            tbpClockodo.Name = "tbpClockodo";
            tbpClockodo.Padding = new Padding(3);
            tbpClockodo.Size = new Size(1279, 669);
            tbpClockodo.TabIndex = 2;
            tbpClockodo.Text = "Clockodo";
            tbpClockodo.UseVisualStyleBackColor = true;
            // 
            // tbcClockodo
            // 
            tbcClockodo.Controls.Add(tbpClockodoSelbstkontrolle);
            tbcClockodo.Dock = DockStyle.Fill;
            tbcClockodo.Location = new Point(3, 3);
            tbcClockodo.Name = "tbcClockodo";
            tbcClockodo.SelectedIndex = 0;
            tbcClockodo.Size = new Size(1273, 663);
            tbcClockodo.TabIndex = 9;
            // 
            // tbpClockodoSelbstkontrolle
            // 
            tbpClockodoSelbstkontrolle.Controls.Add(dgvClockodoFehler);
            tbpClockodoSelbstkontrolle.Controls.Add(btnFehlerAktualisieren);
            tbpClockodoSelbstkontrolle.Controls.Add(gbxZeitraumSchnellauswahl);
            tbpClockodoSelbstkontrolle.Controls.Add(gbxZeitraum);
            tbpClockodoSelbstkontrolle.Location = new Point(4, 32);
            tbpClockodoSelbstkontrolle.Name = "tbpClockodoSelbstkontrolle";
            tbpClockodoSelbstkontrolle.Padding = new Padding(3);
            tbpClockodoSelbstkontrolle.Size = new Size(1265, 627);
            tbpClockodoSelbstkontrolle.TabIndex = 0;
            tbpClockodoSelbstkontrolle.Text = "Selbstkontrolle";
            tbpClockodoSelbstkontrolle.UseVisualStyleBackColor = true;
            // 
            // dgvClockodoFehler
            // 
            dgvClockodoFehler.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvClockodoFehler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClockodoFehler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClockodoFehler.Location = new Point(0, 129);
            dgvClockodoFehler.Name = "dgvClockodoFehler";
            dgvClockodoFehler.Size = new Size(1265, 498);
            dgvClockodoFehler.TabIndex = 0;
            dgvClockodoFehler.CellDoubleClick += dgvClockodoFehler_CellDoubleClick;
            // 
            // btnFehlerAktualisieren
            // 
            btnFehlerAktualisieren.Location = new Point(799, 14);
            btnFehlerAktualisieren.Name = "btnFehlerAktualisieren";
            btnFehlerAktualisieren.Size = new Size(138, 109);
            btnFehlerAktualisieren.TabIndex = 1;
            btnFehlerAktualisieren.Text = "Aktualisieren";
            btnFehlerAktualisieren.UseVisualStyleBackColor = true;
            btnFehlerAktualisieren.Click += btnFehlerAktualisiere_click;
            // 
            // gbxZeitraumSchnellauswahl
            // 
            gbxZeitraumSchnellauswahl.Controls.Add(btnLetzteZweiWochenVormonat);
            gbxZeitraumSchnellauswahl.Controls.Add(btnLetzterMonat);
            gbxZeitraumSchnellauswahl.Controls.Add(btnErsteZweiWochenAktuellerMonat);
            gbxZeitraumSchnellauswahl.Location = new Point(408, 3);
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
            gbxZeitraum.Location = new Point(6, 3);
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
            // tbpLexoffice
            // 
            tbpLexoffice.Controls.Add(lexofficeTabControl1);
            tbpLexoffice.Location = new Point(4, 32);
            tbpLexoffice.Name = "tbpLexoffice";
            tbpLexoffice.Padding = new Padding(3);
            tbpLexoffice.Size = new Size(1279, 669);
            tbpLexoffice.TabIndex = 3;
            tbpLexoffice.Text = "Lexoffice";
            tbpLexoffice.UseVisualStyleBackColor = true;
            // 
            // lexofficeTabControl1
            // 
            lexofficeTabControl1.Dock = DockStyle.Fill;
            lexofficeTabControl1.Location = new Point(3, 3);
            lexofficeTabControl1.Name = "lexofficeTabControl1";
            lexofficeTabControl1.Size = new Size(1273, 663);
            lexofficeTabControl1.TabIndex = 0;
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
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            gbxClockodoSettings.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            tbpErfassen.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ctrl_Zeiteintraege).EndInit();
            tbcForms.ResumeLayout(false);
            tbpClockodo.ResumeLayout(false);
            tbcClockodo.ResumeLayout(false);
            tbpClockodoSelbstkontrolle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvClockodoFehler).EndInit();
            gbxZeitraumSchnellauswahl.ResumeLayout(false);
            gbxZeitraum.ResumeLayout(false);
            gbxZeitraum.PerformLayout();
            tbpLexoffice.ResumeLayout(false);
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
        private TabPage tbpClockodo;
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
        private Label label5;
        private Label label3;
        private TabPage tbpLexoffice;
        private TabControl tbcClockodo;
        private TabPage tbpClockodoSelbstkontrolle;
        private GroupBox gbxClockodoSettings;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private TextBox textBox1;
        private Lexoffice.LexofficeTabControl lexofficeTabControl1;
    }
}
