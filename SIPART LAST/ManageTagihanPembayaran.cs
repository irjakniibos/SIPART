﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPART_LAST
{
    public partial class ManageTagihanPembayaran : Form
    {
        Koneksi kn = new Koneksi();
        string connect = "";


        // private string connectionString = "Data Source=LAPTOP-9IG4E42U\\IRZALUVSALMA;" + "Initial Catalog=SIPART;Integrated Security=True";
        private byte[] buktiPembayaranBytes = null;
        private string buktiPembayaranFileName = "";
        public ManageTagihanPembayaran()
        {
            InitializeComponent();
            LoadNamaPenyewa();
            LoadData();
            comboBoxStatusTagihan.Items.AddRange(new[] { "Belum Lunas", "Lunas" });
            comboBoxMetodePembayaran.Items.AddRange(new[] { "Transfer", "QRIS", "Tunai", "Kartu Kredit", "Kartu Debit" });

            // Add event handler for status tagihan change
            comboBoxStatusTagihan.SelectedIndexChanged += ComboBoxStatusTagihan_SelectedIndexChanged;

            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Resize += (s, e) => panelTagihanBayar();
            panelTagihanBayar();
        }

        private void panelTagihanBayar()
        {
            // Panel di tengah horizontal, tetapi tetap di atas (misal 40px dari atas)
            panelTagihan.Left = (this.ClientSize.Width - panelTagihan.Width) / 2;
            panelTagihan.Top = 40; // Jarak dari atas, bisa diubah sesuai kebutuhan
        }

        private void ClearForm()
        {
            comboBoxKontrakID.SelectedIndex = -1;
            txtJumlahTagihan.Clear();
            dateTerbit.Value = DateTime.Now;
            dateJatuhTempo.Value = DateTime.Now;
            comboBoxStatusTagihan.SelectedIndex = -1;

            datePembayaran.Value = DateTime.Now;
            txtJumlahBayar.Clear();
            comboBoxMetodePembayaran.SelectedIndex = -1;
            dateInputBayar.Value = DateTime.Now;
            txtKeterangan.Clear();
        }

        private void ComboBoxStatusTagihan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStatusTagihan.SelectedItem?.ToString() == "Lunas")
            {
                // Jika status Lunas, set jumlah bayar sama dengan jumlah tagihan
                if (!string.IsNullOrWhiteSpace(txtJumlahTagihan.Text))
                {
                    txtJumlahBayar.Text = txtJumlahTagihan.Text;
                }

                // Set tanggal pembayaran dan input ke hari ini jika masih kosong
                if (datePembayaran.Value.Date == DateTime.Now.Date)
                {
                    datePembayaran.Value = DateTime.Now;
                }
                if (dateInputBayar.Value.Date == DateTime.Now.Date)
                {
                    dateInputBayar.Value = DateTime.Now;
                }
            }
            else if (comboBoxStatusTagihan.SelectedItem?.ToString() == "Belum Lunas")
            {
                // Jika status Belum Lunas, kosongkan field pembayaran
                txtJumlahBayar.Clear();
                comboBoxMetodePembayaran.SelectedIndex = -1;
            }
        }

        private void LoadNamaPenyewa()
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                // Tampilkan semua kontrak, bukan hanya yang aktif
                string query = @"
                    SELECT k.KontrakID, k.NamaPenyewa, k.TanggalMulai, k.TanggalSelesai, u.HargaSewa
                    FROM KontrakSewa k
                    INNER JOIN UnitApartemen u ON k.UnitID = u.UnitID";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    comboBoxKontrakID.DisplayMember = "NamaPenyewa";
                    comboBoxKontrakID.ValueMember = "KontrakID";
                    comboBoxKontrakID.DataSource = dt;
                    comboBoxKontrakID.SelectedIndex = -1;

                    comboBoxKontrakID.SelectedIndexChanged += ComboBoxNamaPenyewa_SelectedIndexChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data penyewa: " + ex.Message);
                }
            }
        }

        private void ComboBoxNamaPenyewa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKontrakID.SelectedIndex != -1)
            {
                DataRowView selectedRow = (DataRowView)comboBoxKontrakID.SelectedItem;

                DateTime tanggalMulai = Convert.ToDateTime(selectedRow["TanggalMulai"]);
                DateTime tanggalSelesai = Convert.ToDateTime(selectedRow["TanggalSelesai"]);
                decimal hargaSewa = Convert.ToDecimal(selectedRow["HargaSewa"]);

                // Calculate number of days
                int jumlahHari = (tanggalSelesai - tanggalMulai).Days;

                // Calculate total tagihan (assuming HargaSewa is per day rate)
                decimal jumlahTagihan = hargaSewa * jumlahHari;

                // Set the calculated tagihan to textbox
                txtJumlahTagihan.Text = jumlahTagihan.ToString("N2");

                // NEW: Jika status tagihan sudah dipilih sebagai "Lunas", otomatis isi jumlah bayar
                if (comboBoxStatusTagihan.SelectedItem?.ToString() == "Lunas")
                {
                    txtJumlahBayar.Text = jumlahTagihan.ToString("N2");
                }

                // Optionally show calculation details in a message
                MessageBox.Show($"Nama Penyewa: {selectedRow["NamaPenyewa"]}\n" +
                               $"Periode Sewa: {tanggalMulai:dd/MM/yyyy} - {tanggalSelesai:dd/MM/yyyy}\n" +
                               $"Jumlah Hari: {jumlahHari} hari\n" +
                               $"Harga Sewa per Hari: {hargaSewa:C}\n" +
                               $"Total Tagihan: {jumlahTagihan:C}",
                               "Informasi Tagihan");
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                string query = @"
            SELECT tp.TagihanID, tp.KontrakID, k.NamaPenyewa, tp.JumlahTagihan, 
                   tp.TanggalTerbit, tp.TanggalJatuhTempo, tp.StatusTagihan,
                   tp.TanggalPembayaran, tp.JumlahBayar, tp.MetodePembayaran, 
                   tp.TanggalInputPembayaran, tp.Keterangan
            FROM TagihanPembayaran tp
            INNER JOIN KontrakSewa k ON tp.KontrakID = k.KontrakID";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    dgvTagihanPembayaran.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message);
                }
            }
        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validation
            if (comboBoxKontrakID.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih nama penyewa terlebih dahulu!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtJumlahTagihan.Text))
            {
                MessageBox.Show("Jumlah tagihan tidak boleh kosong!");
                return;
            }

            // NEW: Validation untuk status Lunas
            if (comboBoxStatusTagihan.Text == "Lunas")
            {
                if (string.IsNullOrWhiteSpace(txtJumlahBayar.Text))
                {
                    MessageBox.Show("Jumlah bayar tidak boleh kosong untuk status Lunas!");
                    return;
                }
                if (comboBoxMetodePembayaran.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih metode pembayaran untuk status Lunas!");
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // Menyimpan data tagihan ke dalam tabel TagihanPembayaran
                    SqlCommand insertTagihan = new SqlCommand(@"
                INSERT INTO TagihanPembayaran
                    (KontrakID, JumlahTagihan, TanggalTerbit, TanggalJatuhTempo, StatusTagihan,
                    TanggalPembayaran, JumlahBayar, MetodePembayaran, TanggalInputPembayaran, Keterangan)
                VALUES (@KontrakID, @JumlahTagihan, @TanggalTerbit, @TanggalJatuhTempo, @StatusTagihan,
                        @TanggalPembayaran, @JumlahBayar, @MetodePembayaran, @TanggalInput, @Keterangan)", conn, trans);

                    insertTagihan.Parameters.AddWithValue("@KontrakID", comboBoxKontrakID.SelectedValue);
                    insertTagihan.Parameters.AddWithValue("@JumlahTagihan", Convert.ToDecimal(txtJumlahTagihan.Text));
                    insertTagihan.Parameters.AddWithValue("@TanggalTerbit", dateTerbit.Value);
                    insertTagihan.Parameters.AddWithValue("@TanggalJatuhTempo", dateJatuhTempo.Value);
                    insertTagihan.Parameters.AddWithValue("@StatusTagihan", comboBoxStatusTagihan.Text);

                    // Handle nullable payment fields
                    insertTagihan.Parameters.AddWithValue("@TanggalPembayaran",
                        string.IsNullOrEmpty(comboBoxStatusTagihan.Text) || comboBoxStatusTagihan.Text != "Lunas"
                        ? (object)DBNull.Value : datePembayaran.Value);

                    insertTagihan.Parameters.AddWithValue("@JumlahBayar",
                        string.IsNullOrWhiteSpace(txtJumlahBayar.Text)
                        ? (object)DBNull.Value : Convert.ToDecimal(txtJumlahBayar.Text));

                    insertTagihan.Parameters.AddWithValue("@MetodePembayaran",
                        string.IsNullOrWhiteSpace(comboBoxMetodePembayaran.Text)
                        ? (object)DBNull.Value : comboBoxMetodePembayaran.Text);

                    insertTagihan.Parameters.AddWithValue("@TanggalInput",
                        string.IsNullOrEmpty(comboBoxStatusTagihan.Text) || comboBoxStatusTagihan.Text != "Lunas"
                        ? (object)DBNull.Value : dateInputBayar.Value);

                    insertTagihan.Parameters.AddWithValue("@Keterangan",
                        string.IsNullOrWhiteSpace(txtKeterangan.Text)
                        ? (object)DBNull.Value : txtKeterangan.Text);

                    // Eksekusi perintah insert tagihan
                    insertTagihan.ExecuteNonQuery();

                    // Jika status tagihan adalah Lunas, update status kontrak menjadi 'Selesai'
                    if (comboBoxStatusTagihan.Text == "Lunas")
                    {
                        SqlCommand updateKontrakStatus = new SqlCommand(@"
                    UPDATE KontrakSewa 
                    SET StatusKontrak = 'Selesai' 
                    WHERE KontrakID = @KontrakID", conn, trans);
                        updateKontrakStatus.Parameters.AddWithValue("@KontrakID", comboBoxKontrakID.SelectedValue);
                        updateKontrakStatus.ExecuteNonQuery();
                    }

                    // Commit transaksi jika semua langkah berhasil
                    trans.Commit();
                    MessageBox.Show("Tagihan berhasil ditambahkan dan status kontrak diperbarui.");
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    // Rollback transaksi jika ada kesalahan
                    trans.Rollback();
                    MessageBox.Show("Gagal menambahkan tagihan: " + ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTagihanPembayaran.CurrentRow != null)
            {
                object kontrakIDObj = comboBoxKontrakID.SelectedValue;
                if (kontrakIDObj == null || kontrakIDObj == DBNull.Value)
                {
                    MessageBox.Show("Nama penyewa/KontrakID belum dipilih atau tidak valid.");
                    return;
                }
                int kontrakID;
                if (!int.TryParse(kontrakIDObj.ToString(), out kontrakID))
                {
                    MessageBox.Show("KontrakID tidak valid.");
                    return;
                }

                int tagihanID = Convert.ToInt32(dgvTagihanPembayaran.CurrentRow.Cells["TagihanID"].Value);

                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    // Ambil data lama jika tidak ada file baru
                    byte[] buktiLama = null;
                    if (buktiPembayaranBytes == null)
                    {
                        conn.Open();
                        using (SqlCommand getBukti = new SqlCommand("SELECT BuktiPembayaran FROM TagihanPembayaran WHERE TagihanID = @ID", conn))
                        {
                            getBukti.Parameters.AddWithValue("@ID", tagihanID);
                            object result = getBukti.ExecuteScalar();
                            if (result != DBNull.Value && result != null)
                                buktiLama = (byte[])result;
                        }
                        conn.Close();
                    }

                    string query = @"
                UPDATE TagihanPembayaran SET
                    KontrakID = @KontrakID, 
                    JumlahTagihan = @JumlahTagihan, 
                    TanggalTerbit = @TanggalTerbit,
                    TanggalJatuhTempo = @TanggalJatuhTempo, 
                    StatusTagihan = @StatusTagihan,
                    TanggalPembayaran = @TanggalPembayaran, 
                    JumlahBayar = @JumlahBayar,
                    MetodePembayaran = @MetodePembayaran, 
                    TanggalInputPembayaran = @TanggalInput,
                    Keterangan = @Keterangan, 
                    BuktiPembayaran = @BuktiPembayaran
                WHERE TagihanID = @TagihanID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@KontrakID", kontrakID);
                    cmd.Parameters.AddWithValue("@JumlahTagihan", Convert.ToDecimal(txtJumlahTagihan.Text));
                    cmd.Parameters.AddWithValue("@TanggalTerbit", dateTerbit.Value);
                    cmd.Parameters.AddWithValue("@TanggalJatuhTempo", dateJatuhTempo.Value);
                    cmd.Parameters.AddWithValue("@StatusTagihan", comboBoxStatusTagihan.Text);

                    // Handle nullable payment fields
                    cmd.Parameters.AddWithValue("@TanggalPembayaran",
                        string.IsNullOrEmpty(comboBoxStatusTagihan.Text) || comboBoxStatusTagihan.Text != "Lunas"
                        ? (object)DBNull.Value : datePembayaran.Value);

                    cmd.Parameters.AddWithValue("@JumlahBayar",
                        string.IsNullOrWhiteSpace(txtJumlahBayar.Text)
                        ? (object)DBNull.Value : Convert.ToDecimal(txtJumlahBayar.Text));

                    cmd.Parameters.AddWithValue("@MetodePembayaran",
                        string.IsNullOrWhiteSpace(comboBoxMetodePembayaran.Text)
                        ? (object)DBNull.Value : comboBoxMetodePembayaran.Text);

                    cmd.Parameters.AddWithValue("@TanggalInput",
                        string.IsNullOrEmpty(comboBoxStatusTagihan.Text) || comboBoxStatusTagihan.Text != "Lunas"
                        ? (object)DBNull.Value : dateInputBayar.Value);

                    cmd.Parameters.AddWithValue("@Keterangan",
                        string.IsNullOrWhiteSpace(txtKeterangan.Text)
                        ? (object)DBNull.Value : txtKeterangan.Text);

                    // Perbaikan di sini:
                    cmd.Parameters.AddWithValue("@BuktiPembayaran", buktiPembayaranBytes ?? (object)buktiLama ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TagihanID", tagihanID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diperbarui.");
                        LoadData();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal memperbarui data: " + ex.Message);
                    }
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvTagihanPembayaran.CurrentRow != null)
            {
                var result = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;

                int tagihanID = Convert.ToInt32(dgvTagihanPembayaran.CurrentRow.Cells["TagihanID"].Value);

                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    string query = "DELETE FROM TagihanPembayaran WHERE TagihanID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", tagihanID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil dihapus.");
                        LoadData();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus data: " + ex.Message);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void dgvTagihanPembayaran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTagihanPembayaran.Rows[e.RowIndex];

                // Set the combobox to show the correct nama penyewa
                int kontrakID = Convert.ToInt32(row.Cells["KontrakID"].Value);
                comboBoxKontrakID.SelectedValue = kontrakID;

                txtNamaPenyewa.Text = row.Cells["NamaPenyewa"].Value?.ToString();

                txtJumlahTagihan.Text = row.Cells["JumlahTagihan"].Value?.ToString();
                dateTerbit.Value = Convert.ToDateTime(row.Cells["TanggalTerbit"].Value);
                dateJatuhTempo.Value = Convert.ToDateTime(row.Cells["TanggalJatuhTempo"].Value);
                comboBoxStatusTagihan.Text = row.Cells["StatusTagihan"].Value?.ToString();

                if (row.Cells["TanggalPembayaran"].Value != DBNull.Value)
                    datePembayaran.Value = Convert.ToDateTime(row.Cells["TanggalPembayaran"].Value);

                txtJumlahBayar.Text = row.Cells["JumlahBayar"].Value?.ToString();
                comboBoxMetodePembayaran.Text = row.Cells["MetodePembayaran"].Value?.ToString();

                if (row.Cells["TanggalInputPembayaran"].Value != DBNull.Value)
                    dateInputBayar.Value = Convert.ToDateTime(row.Cells["TanggalInputPembayaran"].Value);

                txtKeterangan.Text = row.Cells["Keterangan"].Value?.ToString();
            }
        }

        private void btnBack(object sender, EventArgs e)
        {
            MainPage mainpage = new MainPage();
            mainpage.Show();
            this.Hide();
        }

        private void btnEkspor_Click(object sender, EventArgs e)
        {
            FormTagihan formKegiatan = new FormTagihan();
            formKegiatan.Show();
            this.Hide(); // Menyembunyikan FormOrganisasi saat FormKegiatan dibuka
        }

        private void btnAnalyz_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    StringBuilder statistik = new StringBuilder();
                    conn.InfoMessage += (s, args) =>
                    {
                        statistik.AppendLine(args.Message);
                    };

                    conn.Open();

                    // Aktifkan statistik IO dan TIME
                    using (SqlCommand cmdStat = new SqlCommand("SET STATISTICS IO ON; SET STATISTICS TIME ON;", conn))
                    {
                        cmdStat.ExecuteNonQuery();
                    }

                    // Jalankan query yang ingin dianalisis
                    string query = @"
                SELECT 
                    COUNT(CASE WHEN StatusTagihan = 'Lunas' THEN 1 END) AS JumlahTagihanLunas,
                    COUNT(DISTINCT TanggalTerbit) AS JumlahTanggalTerbit
                FROM TagihanPembayaran
                WHERE StatusTagihan = 'Lunas' OR TanggalTerbit IS NOT NULL;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Baca hasil (tidak perlu ditampilkan, hanya untuk trigger statistik)
                            while (reader.Read()) { }
                        }
                    }

                    // Tampilkan hasil statistik
                    if (statistik.Length > 0)
                        MessageBox.Show(statistik.ToString(), "STATISTICS INFO");
                    else
                        MessageBox.Show("Tidak ada statistik yang diterima.", "STATISTICS INFO");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat melakukan analisis: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBrowseBukti_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih Bukti Pembayaran";
                ofd.Filter = "Image or PDF Files (*.jpg;*.jpeg;*.png;*.bmp;*.pdf)|*.jpg;*.jpeg;*.png;*.bmp;*.pdf|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    buktiPembayaranBytes = File.ReadAllBytes(ofd.FileName);
                    buktiPembayaranFileName = Path.GetFileName(ofd.FileName);
                    lblBuktiPembayaran.Text = buktiPembayaranFileName;
                }
            }
        }

        private void btnLihatBukti_Click(object sender, EventArgs e)
        {
            if (dgvTagihanPembayaran.CurrentRow == null)
            {
                MessageBox.Show("Pilih data pembayaran terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int tagihanID = Convert.ToInt32(dgvTagihanPembayaran.CurrentRow.Cells["TagihanID"].Value);

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                // Ambil dari tabel TagihanPembayaran, bukan DataPembayaran
                string query = "SELECT BuktiPembayaran FROM TagihanPembayaran WHERE TagihanID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", tagihanID);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        byte[] fileBytes = (byte[])result;

                        // Tentukan ekstensi file (opsional, default .jpg)
                        string tempPath = Path.Combine(Path.GetTempPath(), $"BuktiPembayaran_{tagihanID}.jpg");
                        File.WriteAllBytes(tempPath, fileBytes);
                        System.Diagnostics.Process.Start(tempPath);
                    }
                    else
                    {
                        MessageBox.Show("Bukti pembayaran belum diupload.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }

}
