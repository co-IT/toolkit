namespace coIT.Clockodo.TimeEntriesValidator
{
    partial class CreateCredentials
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
            this.tbxEmployeename = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxEmployeemail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxEmployeetoken = new System.Windows.Forms.TextBox();
            this.btnCreateConfig = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxEmployeename
            // 
            this.tbxEmployeename.Location = new System.Drawing.Point(71, 33);
            this.tbxEmployeename.Name = "tbxEmployeename";
            this.tbxEmployeename.PlaceholderText = "100XX:Name";
            this.tbxEmployeename.Size = new System.Drawing.Size(224, 23);
            this.tbxEmployeename.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Email:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbxEmployeemail
            // 
            this.tbxEmployeemail.Location = new System.Drawing.Point(71, 73);
            this.tbxEmployeemail.Name = "tbxEmployeemail";
            this.tbxEmployeemail.Size = new System.Drawing.Size(224, 23);
            this.tbxEmployeemail.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Token:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbxEmployeetoken
            // 
            this.tbxEmployeetoken.Location = new System.Drawing.Point(71, 114);
            this.tbxEmployeetoken.Name = "tbxEmployeetoken";
            this.tbxEmployeetoken.Size = new System.Drawing.Size(224, 23);
            this.tbxEmployeetoken.TabIndex = 4;
            // 
            // btnCreateConfig
            // 
            this.btnCreateConfig.Location = new System.Drawing.Point(177, 153);
            this.btnCreateConfig.Name = "btnCreateConfig";
            this.btnCreateConfig.Size = new System.Drawing.Size(118, 29);
            this.btnCreateConfig.TabIndex = 6;
            this.btnCreateConfig.Text = "Anlegen";
            this.btnCreateConfig.UseVisualStyleBackColor = true;
            this.btnCreateConfig.Click += new System.EventHandler(this.btnCreateConfig_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateConfig);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbxEmployeetoken);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxEmployeemail);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxEmployeename);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 188);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bitte Daten eingeben";
            // 
            // CreateCredentials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 212);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateCredentials";
            this.Text = "Time Entry Validator | Erstkonfiguration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBox tbxEmployeename;
        private Label label1;
        private Label label2;
        private TextBox tbxEmployeemail;
        private Label label3;
        private TextBox tbxEmployeetoken;
        private Button btnCreateConfig;
        private GroupBox groupBox1;
    }
}