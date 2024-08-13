namespace coIT.Clockodo.QuickActions.Lexoffice
{
    partial class LexofficeRechnungskontrolle
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
            tbxRechnungUrl = new TextBox();
            btnRechnungPrüfen = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tbxRechnungUrl
            // 
            tbxRechnungUrl.Location = new Point(24, 42);
            tbxRechnungUrl.Name = "tbxRechnungUrl";
            tbxRechnungUrl.PlaceholderText = "https://app.lexoffice.de/voucher/#/########-####-####-####-############";
            tbxRechnungUrl.Size = new Size(702, 23);
            tbxRechnungUrl.TabIndex = 0;
            // 
            // btnRechnungPrüfen
            // 
            btnRechnungPrüfen.Location = new Point(732, 42);
            btnRechnungPrüfen.Name = "btnRechnungPrüfen";
            btnRechnungPrüfen.Size = new Size(210, 32);
            btnRechnungPrüfen.TabIndex = 1;
            btnRechnungPrüfen.Text = "Rechnung prüfen";
            btnRechnungPrüfen.UseVisualStyleBackColor = true;
            btnRechnungPrüfen.Click += btnRechnungPrüfen_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbxRechnungUrl);
            groupBox1.Controls.Add(btnRechnungPrüfen);
            groupBox1.Location = new Point(17, 31);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(948, 90);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Url der zu prüfenden Rechnung einfügen";
            // 
            // groupBox2
            // 
            groupBox2.Location = new Point(17, 127);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(948, 260);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Ergebnisse der Prüfung";
            // 
            // LexofficeRechnungskontrolle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "LexofficeRechnungskontrolle";
            Size = new Size(968, 491);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox tbxRechnungUrl;
        private Button btnRechnungPrüfen;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}
