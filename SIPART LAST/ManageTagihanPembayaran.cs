using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPART_LAST
{
    public partial class ManageTagihanPembayaran : Form
    {
        private string connectionString = "Data Source=LAPTOP-9IG4E42U\\IRZALUVSALMA;" + "Initial Catalog=SIPART;Integrated Security=True";

        public ManageTagihanPembayaran()
        {
            InitializeComponent();
            LoadNamaPenyewa();
            LoadData();
            comboBoxStatusTagihan.Items.AddRange(new[] { "Belum Lunas", "Lunas" });
            comboBoxMetodePembayaran.Items.AddRange(new[] { "Transfer", "QRIS", "Tunai", "Kartu Kredit", "Kartu Debit" });

            // Add event handler for status tagihan change
            comboBoxStatusTagihan.SelectedIndexChanged += ComboBoxStatusTagihan_SelectedIndexChanged;
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT k.KontrakID, k.NamaPenyewa, k.TanggalMulai, k.TanggalSelesai, u.HargaSewa
                FROM KontrakSewa k
                INNER JOIN UnitApartemen u ON k.UnitID = u.UnitID
                WHERE k.StatusKontrak = 'Aktif'";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    comboBoxKontrakID.DisplayMember = "NamaPenyewa"; // Display nama penyewa
                    comboBoxKontrakID.ValueMember = "KontrakID";    // Value tetap KontrakID
                    comboBoxKontrakID.DataSource = dt;
                    comboBoxKontrakID.SelectedIndex = -1;

                    // Add event handler for selection change
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
            using (SqlConnection conn = new SqlConnection(connectionString))
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

            using (SqlConnection conn = new SqlConnection(connectionString))
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
                // NEW: Validation untuk status Lunas saat edit
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

                int tagihanID = Convert.ToInt32(dgvTagihanPembayaran.CurrentRow.Cells["TagihanID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                    UPDATE TagihanPembayaran SET
                        KontrakID = @KontrakID, JumlahTagihan = @JumlahTagihan, TanggalTerbit = @TanggalTerbit,
                        TanggalJatuhTempo = @TanggalJatuhTempo, StatusTagihan = @StatusTagihan,
                        TanggalPembayaran = @TanggalPembayaran, JumlahBayar = @JumlahBayar,
                        MetodePembayaran = @MetodePembayaran, TanggalInputPembayaran = @TanggalInput,
                        Keterangan = @Keterangan
                    WHERE TagihanID = @TagihanID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@KontrakID", comboBoxKontrakID.SelectedValue);
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

                using (SqlConnection conn = new SqlConnection(connectionString))
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                COUNT(CASE WHEN StatusTagihan = 'Lunas' THEN 1 END) AS JumlahTagihanLunas,
                COUNT(DISTINCT TanggalTerbit) AS JumlahTanggalTerbit
            FROM TagihanPembayaran
            WHERE StatusTagihan = 'Lunas' OR TanggalTerbit IS NOT NULL;";  // Menghitung jumlah tagihan Lunas dan tanggal terbit yang unik

                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int jumlahTagihanLunas = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        int jumlahTanggalTerbit = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);

                        MessageBox.Show(
                            $"Jumlah Tagihan Lunas: {jumlahTagihanLunas}\n" +
                            $"Jumlah Tanggal Terbit yang Unik: {jumlahTanggalTerbit}",
                            "Analisis Data Tagihan Pembayaran",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Tidak ada data yang ditemukan untuk analisis.", "Analisis Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat melakukan analisis: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
