﻿namespace CRUDOrmas
{
    partial class FormReportApart
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
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Back = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panelReport = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Location = new System.Drawing.Point(24, 56);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(750, 340);
            this.reportViewer.TabIndex = 0;
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(24, 402);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(75, 37);
            this.Back.TabIndex = 1;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(589, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Export PDF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(699, 402);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 37);
            this.button2.TabIndex = 3;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(471, 402);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 37);
            this.button3.TabIndex = 4;
            this.button3.Text = "Export Excel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // panelReport
            // 
            this.panelReport.Controls.Add(this.reportViewer);
            this.panelReport.Controls.Add(this.label1);
            this.panelReport.Controls.Add(this.Back);
            this.panelReport.Controls.Add(this.button3);
            this.panelReport.Controls.Add(this.button2);
            this.panelReport.Controls.Add(this.button1);
            this.panelReport.Location = new System.Drawing.Point(-1, -1);
            this.panelReport.Name = "panelReport";
            this.panelReport.Size = new System.Drawing.Size(802, 454);
            this.panelReport.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(248, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Report Unit Apartemen";
            // 
            // FormReportApart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelReport);
            this.Name = "FormReportApart";
            this.Text = "FormReportApart";
            this.Load += new System.EventHandler(this.FormReportApart_Load);
            this.panelReport.ResumeLayout(false);
            this.panelReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panelReport;
        private System.Windows.Forms.Label label1;
    }
}