namespace SIPART_LAST
{
    partial class ManageApart
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
            this.dgvUnitApart = new System.Windows.Forms.DataGridView();
            this.txtHargaSewa = new System.Windows.Forms.TextBox();
            this.txtNomorUnit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Back = new System.Windows.Forms.Button();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.Apartemen = new System.Windows.Forms.Label();
            this.tambah = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBoxTipe = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitApart)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUnitApart
            // 
            this.dgvUnitApart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnitApart.Location = new System.Drawing.Point(52, 244);
            this.dgvUnitApart.Name = "dgvUnitApart";
            this.dgvUnitApart.RowHeadersWidth = 51;
            this.dgvUnitApart.RowTemplate.Height = 24;
            this.dgvUnitApart.Size = new System.Drawing.Size(686, 182);
            this.dgvUnitApart.TabIndex = 31;
            this.dgvUnitApart.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUnitApart_CellClick);
            // 
            // txtHargaSewa
            // 
            this.txtHargaSewa.Location = new System.Drawing.Point(176, 143);
            this.txtHargaSewa.Name = "txtHargaSewa";
            this.txtHargaSewa.Size = new System.Drawing.Size(428, 22);
            this.txtHargaSewa.TabIndex = 28;
            // 
            // txtNomorUnit
            // 
            this.txtNomorUnit.Location = new System.Drawing.Point(176, 73);
            this.txtNomorUnit.Name = "txtNomorUnit";
            this.txtNomorUnit.Size = new System.Drawing.Size(428, 22);
            this.txtNomorUnit.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(48, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "Status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(48, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "Harga Sewa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Tipe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Nomor Unit";
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(50, 432);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(92, 33);
            this.Back.TabIndex = 32;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.btnBack);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(176, 178);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(194, 24);
            this.comboBoxStatus.TabIndex = 37;
            // 
            // Apartemen
            // 
            this.Apartemen.AutoSize = true;
            this.Apartemen.BackColor = System.Drawing.Color.Transparent;
            this.Apartemen.Font = new System.Drawing.Font("Century Gothic", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Apartemen.Location = new System.Drawing.Point(170, 19);
            this.Apartemen.Name = "Apartemen";
            this.Apartemen.Size = new System.Drawing.Size(429, 34);
            this.Apartemen.TabIndex = 38;
            this.Apartemen.Text = "MANAJEMEN UNIT APARTEMEN";
            // 
            // tambah
            // 
            this.tambah.BackColor = System.Drawing.Color.GreenYellow;
            this.tambah.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tambah.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tambah.Location = new System.Drawing.Point(633, 73);
            this.tambah.Name = "tambah";
            this.tambah.Size = new System.Drawing.Size(105, 31);
            this.tambah.TabIndex = 39;
            this.tambah.Text = "Tambah";
            this.tambah.UseVisualStyleBackColor = false;
            this.tambah.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Yellow;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(633, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 31);
            this.button1.TabIndex = 40;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.IndianRed;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(633, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 31);
            this.button2.TabIndex = 41;
            this.button2.Text = "Hapus";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkGray;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(633, 185);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 31);
            this.button3.TabIndex = 42;
            this.button3.Text = "Refresh";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // comboBoxTipe
            // 
            this.comboBoxTipe.FormattingEnabled = true;
            this.comboBoxTipe.Location = new System.Drawing.Point(176, 106);
            this.comboBoxTipe.Name = "comboBoxTipe";
            this.comboBoxTipe.Size = new System.Drawing.Size(428, 24);
            this.comboBoxTipe.TabIndex = 45;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(572, 432);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 33);
            this.button4.TabIndex = 46;
            this.button4.Text = "Analisis";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(663, 432);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 33);
            this.button5.TabIndex = 47;
            this.button5.Text = "Report";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnReport);
            // 
            // ManageApart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(800, 473);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBoxTipe);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tambah);
            this.Controls.Add(this.Apartemen);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.dgvUnitApart);
            this.Controls.Add(this.txtHargaSewa);
            this.Controls.Add(this.txtNomorUnit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "ManageApart";
            this.Text = "ManageApart";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitApart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvUnitApart;
        private System.Windows.Forms.TextBox txtHargaSewa;
        private System.Windows.Forms.TextBox txtNomorUnit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label Apartemen;
        private System.Windows.Forms.Button tambah;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBoxTipe;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}