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
            label1 = new Label();
            groupBox2 = new GroupBox();
            lblErgebnisse = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // tbxRechnungUrl
            // 
            tbxRechnungUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbxRechnungUrl.Location = new Point(31, 74);
            tbxRechnungUrl.Margin = new Padding(4, 5, 4, 5);
            tbxRechnungUrl.Name = "tbxRechnungUrl";
            tbxRechnungUrl.PlaceholderText = "https://app.lexoffice.de/voucher/#/########-####-####-####-############";
            tbxRechnungUrl.Size = new Size(778, 30);
            tbxRechnungUrl.TabIndex = 0;
            // 
            // btnRechnungPrüfen
            // 
            btnRechnungPrüfen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRechnungPrüfen.Location = new Point(817, 73);
            btnRechnungPrüfen.Margin = new Padding(4, 5, 4, 5);
            btnRechnungPrüfen.Name = "btnRechnungPrüfen";
            btnRechnungPrüfen.Size = new Size(247, 30);
            btnRechnungPrüfen.TabIndex = 1;
            btnRechnungPrüfen.Text = "Rechnung prüfen";
            btnRechnungPrüfen.UseVisualStyleBackColor = true;
            btnRechnungPrüfen.Click += btnRechnungPrüfen_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(tbxRechnungUrl);
            groupBox1.Controls.Add(btnRechnungPrüfen);
            groupBox1.Location = new Point(22, 48);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(1121, 138);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Url der zu prüfenden Rechnung einfügen";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 46);
            label1.Name = "label1";
            label1.Size = new Size(554, 23);
            label1.TabIndex = 2;
            label1.Text = "Bitte stelle sicher, dass die Rechnung zuerst zwischengespeichert wurde";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(lblErgebnisse);
            groupBox2.Location = new Point(22, 195);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(1121, 364);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Ergebnisse der Prüfung";
            // 
            // lblErgebnisse
            // 
            lblErgebnisse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblErgebnisse.Location = new Point(31, 38);
            lblErgebnisse.Margin = new Padding(4, 0, 4, 0);
            lblErgebnisse.Name = "lblErgebnisse";
            lblErgebnisse.Size = new Size(1069, 282);
            lblErgebnisse.TabIndex = 0;
            // 
            // LexofficeRechnungskontrolle
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 5, 4, 5);
            Name = "LexofficeRechnungskontrolle";
            Size = new Size(1166, 579);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox tbxRechnungUrl;
        private Button btnRechnungPrüfen;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblErgebnisse;
        private Label label1;
    }
}
