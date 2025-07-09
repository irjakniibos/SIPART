using CRUDOrmas;
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
using System.Windows.Forms.VisualStyles;

namespace SIPART_LAST
{
    public partial class ManageApart : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        //private string connectionString = "Data Source=LAPTOP-9IG4E42U\\IRZALUVSALMA;" + "Initial Catalog=SIPART;Integrated Security=True";
        private int selectedUnitID = -1;

        private Dictionary<string, decimal> hargaTipeKamar = new Dictionary<string, decimal>()
    {
        { "Studio", 150000 },
        { "1 Bedroom", 200000 },
        { "2 Bedroom", 300000 },
        { "Duplex", 500000 },
        { "Penthouse", 800000 }
    };

        public ManageApart()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Resize += (s, e) => panelUnitApart();
            panelUnitApart();

            dgvUnitApart.CellClick += dgvUnitApart_CellClick;
            LoadData();
            InitializeComboBoxStatus();
            InitializeComboBoxTipe();
            comboBoxTipe.SelectedIndexChanged += comboBoxTipe_SelectedIndexChanged;
        }
        private void panelUnitApart()
        {
            // Panel di tengah horizontal, tetapi tetap di atas (misal 40px dari atas)
            panelUnit.Left = (this.ClientSize.Width - panelUnit.Width) / 2;
            panelUnit.Top = 40; // Jarak dari atas, bisa diubah sesuai kebutuhan
        }


        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                // Optimized query with WHERE and SELECT for necessary columns
                string query = @"
                SELECT UnitID, NomorUnit, Tipe, HargaSewa, Status
                FROM UnitApartemen
                WHERE Status = 'Tersedia' OR Status = 'Disewa' OR Status = 'Maintenance'";

                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvUnitApart.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void InitializeComboBoxTipe()
        {
            comboBoxTipe.Items.Clear();
            comboBoxTipe.Items.AddRange(new string[] { "Studio", "1 Bedroom", "2 Bedroom", "Duplex", "Penthouse" });
            comboBoxTipe.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void InitializeComboBoxStatus()
        {
            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.AddRange(new string[] { "Tersedia", "Disewa", "Maintenance" });
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ClearForm()
        {
            txtNomorUnit.Clear();
            comboBoxTipe.SelectedIndex = -1;
            txtHargaSewa.Clear();
            comboBoxStatus.SelectedIndex = -1;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            // Validasi format NomorUnit
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNomorUnit.Text.Trim(), @"^U\d{5}$"))
            {
                MessageBox.Show("Nomor Unit harus berformat U00001 (diawali 'U' dan 5 digit angka).");
                return;
            }

            if (!decimal.TryParse(txtHargaSewa.Text, out decimal harga))
            {
                MessageBox.Show("Harga sewa harus berupa angka.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                string query = @"INSERT INTO UnitApartemen (NomorUnit, Tipe, HargaSewa, Status) 
                             VALUES (@NomorUnit, @Tipe, @HargaSewa, @Status)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NomorUnit", txtNomorUnit.Text.Trim());
                cmd.Parameters.AddWithValue("@Tipe", comboBoxTipe.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@HargaSewa", harga);
                cmd.Parameters.AddWithValue("@Status", comboBoxStatus.SelectedItem?.ToString() ?? "Tersedia");

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil ditambahkan!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUnitID == -1)
            {
                MessageBox.Show("Pilih data yang ingin diperbarui terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNomorUnit.Text.Trim(), @"^U\d{5}$"))
            {
                MessageBox.Show("Nomor Unit harus berformat U00001 (diawali 'U' dan 5 digit angka).");
                return;
            }

            if (!decimal.TryParse(txtHargaSewa.Text, out decimal harga))
            {
                MessageBox.Show("Harga sewa harus berupa angka.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // **Update status unit dalam transaksi**
                    string storedProcedure = "spUpdateUnitStatus";
                    SqlCommand cmdUpdate = new SqlCommand(storedProcedure, conn, transaction);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@UnitID", selectedUnitID);
                    cmdUpdate.Parameters.AddWithValue("@NewStatus", comboBoxStatus.SelectedItem?.ToString() ?? "Tersedia");
                    cmdUpdate.ExecuteNonQuery();

                    // **Commit transaksi jika berhasil**
                    transaction.Commit();
                    MessageBox.Show("Status unit berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData(); // Refresh tampilan
                    selectedUnitID = -1; // Reset pilihan setelah update
                }
                catch (Exception ex)
                {
                    // **Rollback transaksi jika terjadi kesalahan**
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedUnitID == -1)
            {
                MessageBox.Show("Pilih data yang ingin dihapus terlebih dahulu!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                string query = "DELETE FROM UnitApartemen WHERE UnitID = @UnitID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UnitID", selectedUnitID);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil dihapus!");
                    LoadData();
                    selectedUnitID = -1; // reset setelah delete
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            MessageBox.Show("Data berhasil direfresh!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack(object sender, EventArgs e)
        {
            MainPage mainpage = new MainPage();
            mainpage.Show();
            this.Hide();
        }

        private void dgvUnitApart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvUnitApart.Rows.Count)
            {
                DataGridViewRow row = dgvUnitApart.Rows[e.RowIndex];

                // Simpan UnitID ke variabel tersembunyi
                selectedUnitID = Convert.ToInt32(row.Cells["UnitID"].Value);

                // Isi field form
                txtNomorUnit.Text = row.Cells["NomorUnit"].Value?.ToString() ?? "";
                comboBoxTipe.SelectedItem = row.Cells["Tipe"].Value?.ToString() ?? "";
                txtHargaSewa.Text = row.Cells["HargaSewa"].Value?.ToString() ?? "";
                comboBoxStatus.SelectedItem = row.Cells["Status"].Value?.ToString() ?? "";
            }
        }

        private void comboBoxTipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipe.SelectedItem != null)
            {
                string tipeTerpilih = comboBoxTipe.SelectedItem.ToString();
                if (hargaTipeKamar.ContainsKey(tipeTerpilih))
                {
                    txtHargaSewa.Text = hargaTipeKamar[tipeTerpilih].ToString("F0");
                }
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
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
                SELECT UnitID, NomorUnit, Tipe, HargaSewa, Status
                FROM UnitApartemen
                WHERE Status = 'Tersedia'
                ORDER BY HargaSewa ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Anda bisa load ke DataTable jika ingin, tapi tidak wajib untuk statistik
                            while (reader.Read()) { /* Kosongkan, hanya untuk trigger statistik */ }
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
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReport(object sender, EventArgs e)
        {
            FormReportApart reportApart = new FormReportApart();
            reportApart.Show();
            this.Hide();
        }
    }

}
