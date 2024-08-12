namespace coIT.Lexoffice.GdiExport.Umsatzkontenprüfung
{
    partial class UmsatzkontenprüfungControl
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
            tvErgebnis = new TreeView();
            tbxCsvPfad = new TextBox();
            btnCsvAuswählen = new Button();
            dlgCsvÖffnen = new OpenFileDialog();
            SuspendLayout();
            // 
            // tvErgebnis
            // 
            tvErgebnis.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvErgebnis.Location = new Point(0, 28);
            tvErgebnis.Name = "tvErgebnis";
            tvErgebnis.Size = new Size(765, 456);
            tvErgebnis.TabIndex = 0;
            // 
            // tbxCsvPfad
            // 
            tbxCsvPfad.Enabled = false;
            tbxCsvPfad.Location = new Point(3, 3);
            tbxCsvPfad.Name = "tbxCsvPfad";
            tbxCsvPfad.Size = new Size(261, 23);
            tbxCsvPfad.TabIndex = 2;
            // 
            // btnCsvAuswählen
            // 
            btnCsvAuswählen.Location = new Point(270, 3);
            btnCsvAuswählen.Name = "btnCsvAuswählen";
            btnCsvAuswählen.Size = new Size(104, 23);
            btnCsvAuswählen.TabIndex = 3;
            btnCsvAuswählen.Text = "Csv Auswählen";
            btnCsvAuswählen.UseVisualStyleBackColor = true;
            btnCsvAuswählen.Click += btnCsvAuswählen_Click;
            // 
            // dlgCsvÖffnen
            // 
            dlgCsvÖffnen.Filter = "CSV files (*.csv)|*.csv";
            // 
            // UmsatzkontenprüfungControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCsvAuswählen);
            Controls.Add(tbxCsvPfad);
            Controls.Add(tvErgebnis);
            Name = "UmsatzkontenprüfungControl";
            Size = new Size(765, 484);
            Load += UmsatzkontenprüfungControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView tvErgebnis;
        private TextBox tbxCsvPfad;
        private Button btnCsvAuswählen;
        private OpenFileDialog dlgCsvÖffnen;
    }
}
