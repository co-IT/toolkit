
namespace coIT.Clockodo.QuickActions.Lexoffice.RechnungspositionenGenerator
{
    partial class LexofficeRechnungspositionsGenerator
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
            groupBox1 = new GroupBox();
            lbxKunde = new ListBox();
            groupBox2 = new GroupBox();
            lbxLeistung = new ListBox();
            groupBox3 = new GroupBox();
            lbxMitarbeiter = new ListBox();
            tbxTitelDerPosition = new TextBox();
            label1 = new Label();
            btnPositionErstellen = new Button();
            tbxErstelltePosition = new TextBox();
            label2 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbxKunde);
            groupBox1.Location = new Point(27, 34);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(355, 354);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kunde";
            // 
            // lbxKunde
            // 
            lbxKunde.FormattingEnabled = true;
            lbxKunde.ItemHeight = 23;
            lbxKunde.Location = new Point(8, 57);
            lbxKunde.Margin = new Padding(4, 5, 4, 5);
            lbxKunde.Name = "lbxKunde";
            lbxKunde.Size = new Size(338, 257);
            lbxKunde.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lbxLeistung);
            groupBox2.Location = new Point(390, 34);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(355, 354);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Leistung";
            // 
            // lbxLeistung
            // 
            lbxLeistung.FormattingEnabled = true;
            lbxLeistung.ItemHeight = 23;
            lbxLeistung.Location = new Point(8, 57);
            lbxLeistung.Margin = new Padding(4, 5, 4, 5);
            lbxLeistung.Name = "lbxLeistung";
            lbxLeistung.Size = new Size(338, 257);
            lbxLeistung.TabIndex = 0;
            lbxLeistung.SelectedValueChanged += lbxLeistung_SelectedValueChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lbxMitarbeiter);
            groupBox3.Location = new Point(752, 34);
            groupBox3.Margin = new Padding(4, 5, 4, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 5, 4, 5);
            groupBox3.Size = new Size(355, 354);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Mitarbeiter";
            // 
            // lbxMitarbeiter
            // 
            lbxMitarbeiter.FormattingEnabled = true;
            lbxMitarbeiter.ItemHeight = 23;
            lbxMitarbeiter.Location = new Point(8, 57);
            lbxMitarbeiter.Margin = new Padding(4, 5, 4, 5);
            lbxMitarbeiter.Name = "lbxMitarbeiter";
            lbxMitarbeiter.Size = new Size(338, 257);
            lbxMitarbeiter.TabIndex = 0;
            // 
            // tbxTitelDerPosition
            // 
            tbxTitelDerPosition.Location = new Point(197, 428);
            tbxTitelDerPosition.Margin = new Padding(4, 5, 4, 5);
            tbxTitelDerPosition.Name = "tbxTitelDerPosition";
            tbxTitelDerPosition.Size = new Size(318, 30);
            tbxTitelDerPosition.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 431);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(141, 23);
            label1.TabIndex = 4;
            label1.Text = "Titel der Position:";
            // 
            // btnPositionErstellen
            // 
            btnPositionErstellen.Location = new Point(524, 428);
            btnPositionErstellen.Margin = new Padding(4, 5, 4, 5);
            btnPositionErstellen.Name = "btnPositionErstellen";
            btnPositionErstellen.Size = new Size(195, 30);
            btnPositionErstellen.TabIndex = 5;
            btnPositionErstellen.Text = "Position erstellen";
            btnPositionErstellen.UseVisualStyleBackColor = true;
            btnPositionErstellen.Click += btnPositionErstellen_Click;
            // 
            // tbxErstelltePosition
            // 
            tbxErstelltePosition.Location = new Point(199, 502);
            tbxErstelltePosition.Margin = new Padding(4, 5, 4, 5);
            tbxErstelltePosition.Name = "tbxErstelltePosition";
            tbxErstelltePosition.Size = new Size(520, 30);
            tbxErstelltePosition.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 504);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(139, 23);
            label2.TabIndex = 7;
            label2.Text = "Erstellte Position:";
            // 
            // LexofficeRechnungspositionsGenerator
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(tbxErstelltePosition);
            Controls.Add(btnPositionErstellen);
            Controls.Add(label1);
            Controls.Add(tbxTitelDerPosition);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 5, 4, 5);
            Name = "LexofficeRechnungspositionsGenerator";
            Size = new Size(1233, 810);
            Load += LexofficeRechnungspositionsGenerator_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private ListBox lbxKunde;
        private GroupBox groupBox2;
        private ListBox lbxLeistung;
        private GroupBox groupBox3;
        private ListBox lbxMitarbeiter;
        private TextBox tbxTitelDerPosition;
        private Label label1;
        private Button btnPositionErstellen;
        private TextBox tbxErstelltePosition;
        private Label label2;
    }
}
