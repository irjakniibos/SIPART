namespace SIPART_LAST
{
    partial class ManageKontrakPenyewa
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
            this.Nama = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNamaPenyewa = new System.Windows.Forms.TextBox();
            this.txtKTP = new System.Windows.Forms.TextBox();
            this.txtTelepon = new System.Windows.Forms.TextBox();
            this.dgvKontrakPenyewa = new System.Windows.Forms.DataGridView();
            this.Back = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dateMulai = new System.Windows.Forms.DateTimePicker();
            this.dateSelesai = new System.Windows.Forms.DateTimePicker();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.comboBoxNomorUnit = new System.Windows.Forms.ComboBox();
            this.TipeKamar = new System.Windows.Forms.Label();
            this.txtTipeKamar = new System.Windows.Forms.TextBox();
            this.Analyze = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKontrakPenyewa)).BeginInit();
            this.SuspendLayout();
            // 
            // Nama
            // 
            this.Nama.AutoSize = true;
            this.Nama.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nama.Location = new System.Drawing.Point(21, 172);
            this.Nama.Name = "Nama";
            this.Nama.Size = new System.Drawing.Size(125, 20);
            this.Nama.TabIndex = 0;
            this.Nama.Text = "Nama Penyewa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "No KTP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "No.Telepon";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Alamat Penyewa";
            // 
            // txtNamaPenyewa
            // 
            this.txtNamaPenyewa.Location = new System.Drawing.Point(190, 172);
            this.txtNamaPenyewa.Name = "txtNamaPenyewa";
            this.txtNamaPenyewa.Size = new System.Drawing.Size(391, 22);
            this.txtNamaPenyewa.TabIndex = 5;
            // 
            // txtKTP
            // 
            this.txtKTP.Location = new System.Drawing.Point(190, 204);
            this.txtKTP.Name = "txtKTP";
            this.txtKTP.Size = new System.Drawing.Size(391, 22);
            this.txtKTP.TabIndex = 6;
            // 
            // txtTelepon
            // 
            this.txtTelepon.Location = new System.Drawing.Point(190, 236);
            this.txtTelepon.Name = "txtTelepon";
            this.txtTelepon.Size = new System.Drawing.Size(391, 22);
            this.txtTelepon.TabIndex = 7;
            // 
            // dgvKontrakPenyewa
            // 
            this.dgvKontrakPenyewa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKontrakPenyewa.Location = new System.Drawing.Point(601, 74);
            this.dgvKontrakPenyewa.Name = "dgvKontrakPenyewa";
            this.dgvKontrakPenyewa.RowHeadersWidth = 51;
            this.dgvKontrakPenyewa.RowTemplate.Height = 24;
            this.dgvKontrakPenyewa.Size = new System.Drawing.Size(579, 374);
            this.dgvKontrakPenyewa.TabIndex = 12;
            this.dgvKontrakPenyewa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKontrakPenyewa_CellClick);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(1089, 454);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(91, 39);
            this.Back.TabIndex = 17;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.GreenYellow;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(72, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 36);
            this.button1.TabIndex = 18;
            this.button1.Text = "Tambah";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.BtnTambah_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Yellow;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(183, 412);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 36);
            this.button2.TabIndex = 19;
            this.button2.Text = "Edit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.IndianRed;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(294, 412);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 36);
            this.button3.TabIndex = 20;
            this.button3.Text = "Refresh";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.DarkGray;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(405, 412);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(105, 36);
            this.button4.TabIndex = 21;
            this.button4.Text = "Hapus";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(326, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(548, 34);
            this.label4.TabIndex = 22;
            this.label4.Text = "MANAJEMEN DATA KONTRAK PENYEWA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Nomor Unit";
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(190, 269);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(391, 22);
            this.txtAlamat.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 20);
            this.label7.TabIndex = 27;
            this.label7.Text = "Tanggal Mulai";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(21, 339);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 20);
            this.label8.TabIndex = 28;
            this.label8.Text = "Tanggal Selesai";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(21, 369);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 20);
            this.label9.TabIndex = 29;
            this.label9.Text = "Status Kontrak";
            // 
            // dateMulai
            // 
            this.dateMulai.Location = new System.Drawing.Point(190, 302);
            this.dateMulai.Name = "dateMulai";
            this.dateMulai.Size = new System.Drawing.Size(232, 22);
            this.dateMulai.TabIndex = 35;
            // 
            // dateSelesai
            // 
            this.dateSelesai.Location = new System.Drawing.Point(190, 335);
            this.dateSelesai.Name = "dateSelesai";
            this.dateSelesai.Size = new System.Drawing.Size(232, 22);
            this.dateSelesai.TabIndex = 36;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(190, 366);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(232, 24);
            this.comboBoxStatus.TabIndex = 37;
            // 
            // comboBoxNomorUnit
            // 
            this.comboBoxNomorUnit.FormattingEnabled = true;
            this.comboBoxNomorUnit.Location = new System.Drawing.Point(190, 110);
            this.comboBoxNomorUnit.Name = "comboBoxNomorUnit";
            this.comboBoxNomorUnit.Size = new System.Drawing.Size(391, 24);
            this.comboBoxNomorUnit.TabIndex = 39;
            // 
            // TipeKamar
            // 
            this.TipeKamar.AutoSize = true;
            this.TipeKamar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipeKamar.Location = new System.Drawing.Point(21, 139);
            this.TipeKamar.Name = "TipeKamar";
            this.TipeKamar.Size = new System.Drawing.Size(95, 20);
            this.TipeKamar.TabIndex = 40;
            this.TipeKamar.Text = "Tipe Kamar";
            // 
            // txtTipeKamar
            // 
            this.txtTipeKamar.Location = new System.Drawing.Point(190, 141);
            this.txtTipeKamar.Name = "txtTipeKamar";
            this.txtTipeKamar.Size = new System.Drawing.Size(391, 22);
            this.txtTipeKamar.TabIndex = 41;
            // 
            // Analyze
            // 
            this.Analyze.Location = new System.Drawing.Point(601, 469);
            this.Analyze.Name = "Analyze";
            this.Analyze.Size = new System.Drawing.Size(75, 23);
            this.Analyze.TabIndex = 42;
            this.Analyze.Text = "analisis";
            this.Analyze.UseVisualStyleBackColor = true;
            this.Analyze.Click += new System.EventHandler(this.Analyze_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(731, 469);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 43;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.button5_Click);
            // 
            // ManageKontrakPenyewa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1192, 524);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.Analyze);
            this.Controls.Add(this.txtTipeKamar);
            this.Controls.Add(this.TipeKamar);
            this.Controls.Add(this.comboBoxNomorUnit);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.dateSelesai);
            this.Controls.Add(this.dateMulai);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtAlamat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.dgvKontrakPenyewa);
            this.Controls.Add(this.txtTelepon);
            this.Controls.Add(this.txtKTP);
            this.Controls.Add(this.txtNamaPenyewa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Nama);
            this.Name = "ManageKontrakPenyewa";
            this.Text = "FormKontrakPenyewa";
            this.Load += new System.EventHandler(this.ManageKontrakPenyewa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKontrakPenyewa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Nama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNamaPenyewa;
        private System.Windows.Forms.TextBox txtKTP;
        private System.Windows.Forms.TextBox txtTelepon;
        private System.Windows.Forms.DataGridView dgvKontrakPenyewa;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateMulai;
        private System.Windows.Forms.DateTimePicker dateSelesai;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.ComboBox comboBoxNomorUnit;
        private System.Windows.Forms.Label TipeKamar;
        private System.Windows.Forms.TextBox txtTipeKamar;
        private System.Windows.Forms.Button Analyze;
        private System.Windows.Forms.Button btnReport;
    }
}