namespace coIT.Clockodo.TimeEntriesValidator
{
    partial class TimeEntriesValidator
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
            this.dtpPeriodStart = new System.Windows.Forms.DateTimePicker();
            this.dtpPeriodEnd = new System.Windows.Forms.DateTimePicker();
            this.lblPeriodStart = new System.Windows.Forms.Label();
            this.lblPeriodEnd = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.clbEmployees = new System.Windows.Forms.CheckedListBox();
            this.btnShowEntries = new System.Windows.Forms.Button();
            this.dgvTimeEntriesWIthErrors = new System.Windows.Forms.DataGridView();
            this.gbxEmployees = new System.Windows.Forms.GroupBox();
            this.gbxTeimframe = new System.Windows.Forms.GroupBox();
            this.btnTimeFrameCurrentMonth = new System.Windows.Forms.Button();
            this.btnTimeframeOneMonthAgo = new System.Windows.Forms.Button();
            this.btnTimeframeTwoMonthsAgo = new System.Windows.Forms.Button();
            this.btnTimeframeThreeMonthsAgo = new System.Windows.Forms.Button();
            this.gbxFilter = new System.Windows.Forms.GroupBox();
            this.cbxIgnoreTimeentriesWithLessThan10Min = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeEntriesWIthErrors)).BeginInit();
            this.gbxEmployees.SuspendLayout();
            this.gbxTeimframe.SuspendLayout();
            this.gbxFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpPeriodStart
            // 
            this.dtpPeriodStart.Location = new System.Drawing.Point(85, 35);
            this.dtpPeriodStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpPeriodStart.Name = "dtpPeriodStart";
            this.dtpPeriodStart.Size = new System.Drawing.Size(204, 23);
            this.dtpPeriodStart.TabIndex = 0;
            // 
            // dtpPeriodEnd
            // 
            this.dtpPeriodEnd.Location = new System.Drawing.Point(85, 70);
            this.dtpPeriodEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpPeriodEnd.Name = "dtpPeriodEnd";
            this.dtpPeriodEnd.Size = new System.Drawing.Size(204, 23);
            this.dtpPeriodEnd.TabIndex = 1;
            // 
            // lblPeriodStart
            // 
            this.lblPeriodStart.Location = new System.Drawing.Point(37, 35);
            this.lblPeriodStart.Name = "lblPeriodStart";
            this.lblPeriodStart.Size = new System.Drawing.Size(42, 19);
            this.lblPeriodStart.TabIndex = 2;
            this.lblPeriodStart.Text = "Start:";
            this.lblPeriodStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPeriodEnd
            // 
            this.lblPeriodEnd.Location = new System.Drawing.Point(37, 70);
            this.lblPeriodEnd.Name = "lblPeriodEnd";
            this.lblPeriodEnd.Size = new System.Drawing.Size(42, 19);
            this.lblPeriodEnd.TabIndex = 3;
            this.lblPeriodEnd.Text = "Ende:";
            this.lblPeriodEnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(166, 181);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(123, 39);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Abfragen";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // clbEmployees
            // 
            this.clbEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.clbEmployees.CheckOnClick = true;
            this.clbEmployees.FormattingEnabled = true;
            this.clbEmployees.Location = new System.Drawing.Point(9, 35);
            this.clbEmployees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clbEmployees.Name = "clbEmployees";
            this.clbEmployees.Size = new System.Drawing.Size(297, 130);
            this.clbEmployees.TabIndex = 5;
            // 
            // btnShowEntries
            // 
            this.btnShowEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowEntries.Location = new System.Drawing.Point(226, 175);
            this.btnShowEntries.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowEntries.Name = "btnShowEntries";
            this.btnShowEntries.Size = new System.Drawing.Size(82, 22);
            this.btnShowEntries.TabIndex = 6;
            this.btnShowEntries.Text = "Anzeigen";
            this.btnShowEntries.UseVisualStyleBackColor = true;
            this.btnShowEntries.Click += new System.EventHandler(this.btnShowEntries_Click);
            // 
            // dgvTimeEntriesWIthErrors
            // 
            this.dgvTimeEntriesWIthErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTimeEntriesWIthErrors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTimeEntriesWIthErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimeEntriesWIthErrors.Location = new System.Drawing.Point(352, 33);
            this.dgvTimeEntriesWIthErrors.Name = "dgvTimeEntriesWIthErrors";
            this.dgvTimeEntriesWIthErrors.RowTemplate.Height = 25;
            this.dgvTimeEntriesWIthErrors.Size = new System.Drawing.Size(539, 542);
            this.dgvTimeEntriesWIthErrors.TabIndex = 7;
            // 
            // gbxEmployees
            // 
            this.gbxEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxEmployees.Controls.Add(this.clbEmployees);
            this.gbxEmployees.Controls.Add(this.btnShowEntries);
            this.gbxEmployees.Location = new System.Drawing.Point(5, 16);
            this.gbxEmployees.MaximumSize = new System.Drawing.Size(400, 500);
            this.gbxEmployees.Name = "gbxEmployees";
            this.gbxEmployees.Size = new System.Drawing.Size(331, 208);
            this.gbxEmployees.TabIndex = 8;
            this.gbxEmployees.TabStop = false;
            this.gbxEmployees.Text = "Mitarbeiter auswählen";
            // 
            // gbxTeimframe
            // 
            this.gbxTeimframe.Controls.Add(this.btnTimeFrameCurrentMonth);
            this.gbxTeimframe.Controls.Add(this.btnTimeframeOneMonthAgo);
            this.gbxTeimframe.Controls.Add(this.btnTimeframeTwoMonthsAgo);
            this.gbxTeimframe.Controls.Add(this.btnTimeframeThreeMonthsAgo);
            this.gbxTeimframe.Controls.Add(this.lblPeriodStart);
            this.gbxTeimframe.Controls.Add(this.dtpPeriodStart);
            this.gbxTeimframe.Controls.Add(this.dtpPeriodEnd);
            this.gbxTeimframe.Controls.Add(this.btnLoad);
            this.gbxTeimframe.Controls.Add(this.lblPeriodEnd);
            this.gbxTeimframe.Location = new System.Drawing.Point(12, 12);
            this.gbxTeimframe.Name = "gbxTeimframe";
            this.gbxTeimframe.Size = new System.Drawing.Size(334, 231);
            this.gbxTeimframe.TabIndex = 9;
            this.gbxTeimframe.TabStop = false;
            this.gbxTeimframe.Text = "Zeitraum auswählen";
            // 
            // btnTimeFrameCurrentMonth
            // 
            this.btnTimeFrameCurrentMonth.Location = new System.Drawing.Point(166, 143);
            this.btnTimeFrameCurrentMonth.Name = "btnTimeFrameCurrentMonth";
            this.btnTimeFrameCurrentMonth.Size = new System.Drawing.Size(123, 23);
            this.btnTimeFrameCurrentMonth.TabIndex = 8;
            this.btnTimeFrameCurrentMonth.Text = "-0 Monate";
            this.btnTimeFrameCurrentMonth.UseVisualStyleBackColor = true;
            this.btnTimeFrameCurrentMonth.Click += new System.EventHandler(this.btnTimeFrameCurrentMonth_Click);
            // 
            // btnTimeframeOneMonthAgo
            // 
            this.btnTimeframeOneMonthAgo.Location = new System.Drawing.Point(37, 143);
            this.btnTimeframeOneMonthAgo.Name = "btnTimeframeOneMonthAgo";
            this.btnTimeframeOneMonthAgo.Size = new System.Drawing.Size(123, 23);
            this.btnTimeframeOneMonthAgo.TabIndex = 7;
            this.btnTimeframeOneMonthAgo.Text = "-1 Monat";
            this.btnTimeframeOneMonthAgo.UseVisualStyleBackColor = true;
            this.btnTimeframeOneMonthAgo.Click += new System.EventHandler(this.btnTimeframeOneMonthAgo_Click);
            // 
            // btnTimeframeTwoMonthsAgo
            // 
            this.btnTimeframeTwoMonthsAgo.Location = new System.Drawing.Point(166, 114);
            this.btnTimeframeTwoMonthsAgo.Name = "btnTimeframeTwoMonthsAgo";
            this.btnTimeframeTwoMonthsAgo.Size = new System.Drawing.Size(123, 23);
            this.btnTimeframeTwoMonthsAgo.TabIndex = 6;
            this.btnTimeframeTwoMonthsAgo.Text = "-2 Monate";
            this.btnTimeframeTwoMonthsAgo.UseVisualStyleBackColor = true;
            this.btnTimeframeTwoMonthsAgo.Click += new System.EventHandler(this.btnTimeframeTwoMonthsAgo_Click);
            // 
            // btnTimeframeThreeMonthsAgo
            // 
            this.btnTimeframeThreeMonthsAgo.Location = new System.Drawing.Point(37, 114);
            this.btnTimeframeThreeMonthsAgo.Name = "btnTimeframeThreeMonthsAgo";
            this.btnTimeframeThreeMonthsAgo.Size = new System.Drawing.Size(123, 23);
            this.btnTimeframeThreeMonthsAgo.TabIndex = 5;
            this.btnTimeframeThreeMonthsAgo.Text = "-3 Monate";
            this.btnTimeframeThreeMonthsAgo.UseVisualStyleBackColor = true;
            this.btnTimeframeThreeMonthsAgo.Click += new System.EventHandler(this.btnTimeframeThreeMonthsAgo_Click);
            // 
            // gbxFilter
            // 
            this.gbxFilter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.gbxFilter.Controls.Add(this.cbxIgnoreTimeentriesWithLessThan10Min);
            this.gbxFilter.Location = new System.Drawing.Point(5, 230);
            this.gbxFilter.Name = "gbxFilter";
            this.gbxFilter.Size = new System.Drawing.Size(331, 84);
            this.gbxFilter.TabIndex = 10;
            this.gbxFilter.TabStop = false;
            this.gbxFilter.Text = "Filter";
            // 
            // cbxIgnoreTimeentriesWithLessThan10Min
            // 
            this.cbxIgnoreTimeentriesWithLessThan10Min.AutoSize = true;
            this.cbxIgnoreTimeentriesWithLessThan10Min.Location = new System.Drawing.Point(27, 35);
            this.cbxIgnoreTimeentriesWithLessThan10Min.Name = "cbxIgnoreTimeentriesWithLessThan10Min";
            this.cbxIgnoreTimeentriesWithLessThan10Min.Size = new System.Drawing.Size(253, 19);
            this.cbxIgnoreTimeentriesWithLessThan10Min.TabIndex = 0;
            this.cbxIgnoreTimeentriesWithLessThan10Min.Text = "Fehler für Zeiteinträge < 10min ausblenden";
            this.cbxIgnoreTimeentriesWithLessThan10Min.UseVisualStyleBackColor = true;
            this.cbxIgnoreTimeentriesWithLessThan10Min.CheckedChanged += new System.EventHandler(this.cbxIgnoreTimeentriesWithLessThan10Min_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.gbxEmployees);
            this.panel1.Controls.Add(this.gbxFilter);
            this.panel1.Location = new System.Drawing.Point(7, 249);
            this.panel1.MaximumSize = new System.Drawing.Size(400, 500);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 328);
            this.panel1.TabIndex = 11;
            // 
            // TimeEntriesValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 587);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbxTeimframe);
            this.Controls.Add(this.dgvTimeEntriesWIthErrors);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TimeEntriesValidator";
            this.Text = "co-IT.eu GmbH | Time Entry Validator";
            this.Load += new System.EventHandler(this.TimeEntriesValidator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeEntriesWIthErrors)).EndInit();
            this.gbxEmployees.ResumeLayout(false);
            this.gbxTeimframe.ResumeLayout(false);
            this.gbxFilter.ResumeLayout(false);
            this.gbxFilter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DateTimePicker dtpPeriodStart;
        private DateTimePicker dtpPeriodEnd;
        private Label lblPeriodStart;
        private Label lblPeriodEnd;
        private Button btnLoad;
        private CheckedListBox clbEmployees;
        private Button btnShowEntries;
        private DataGridView dgvTimeEntriesWIthErrors;
        private GroupBox gbxEmployees;
        private GroupBox gbxTeimframe;
        private Button btnTimeframeThreeMonthsAgo;
        private Button btnTimeframeOneMonthAgo;
        private Button btnTimeframeTwoMonthsAgo;
        private GroupBox gbxFilter;
        private Panel panel1;
        private CheckBox cbxIgnoreTimeentriesWithLessThan10Min;
        private Button btnTimeFrameCurrentMonth;
    }
}