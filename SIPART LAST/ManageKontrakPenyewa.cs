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
    public partial class ManageKontrakPenyewa : Form
    {
        private string connectionString = "Data Source=LAPTOP-9IG4E42U\\IRZALUVSALMA;" + "Initial Catalog=SIPART;Integrated Security=True";

        public ManageKontrakPenyewa()
        {
            InitializeComponent();
            LoadNomorUnit(); // Load combobox
            LoadStatusKontrak();
            LoadData();

            comboBoxNomorUnit.SelectedIndexChanged += comboBoxNomorUnit_SelectedIndexChanged;
        }

        private void ClearForm()
        {
            comboBoxNomorUnit.SelectedIndex = -1;
            txtTipeKamar.Clear();
            txtNamaPenyewa.Clear();
            txtKTP.Clear();
            txtTelepon.Clear();
            txtAlamat.Clear();
            comboBoxStatus.SelectedIndex = -1;
        }

        private void LoadNomorUnit()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT UnitID, NomorUnit
                             FROM UnitApartemen
                             WHERE Status = 'Tersedia'
                             AND UnitID NOT IN (
                                 SELECT UnitID FROM KontrakSewa WHERE StatusKontrak = 'Aktif'
                             )";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);

                    comboBoxNomorUnit.DisplayMember = "NomorUnit";
                    comboBoxNomorUnit.ValueMember = "UnitID";
                    comboBoxNomorUnit.DataSource = dt;

                    comboBoxNomorUnit.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat unit: " + ex.Message);
                }
            }
        }

        private void LoadStatusKontrak()
        {
            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.Add("Aktif");
            comboBoxStatus.Items.Add("Selesai");
            comboBoxStatus.Items.Add("Dibatalkan");
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
                            k.KontrakID,
                            u.NomorUnit,
                            k.unitID,
                            u.Tipe,
                            k.NamaPenyewa,
                            k.NoKTP,
                            k.NoTelepon,
                            k.AlamatPenyewa,
                            k.TanggalMulai,
                            k.TanggalSelesai,
                            k.StatusKontrak
                         FROM KontrakSewa k
                         JOIN UnitApartemen u ON k.UnitID = u.UnitID";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    dgvKontrakPenyewa.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            if (comboBoxNomorUnit.SelectedIndex < 0)
            {
                MessageBox.Show("Silakan pilih Nomor Unit terlebih dahulu.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    int unitID = Convert.ToInt32(comboBoxNomorUnit.SelectedValue);

                    // Cek status unit dulu
                    SqlCommand cekStatus = new SqlCommand("SELECT Status FROM UnitApartemen WHERE UnitID = @id", conn, trans);
                    cekStatus.Parameters.AddWithValue("@id", unitID);
                    string status = (string)cekStatus.ExecuteScalar();

                    if (status == "Disewa")
                        throw new Exception("Unit sedang disewa.");
                    if (status == "Maintenance")
                        throw new Exception("Unit sedang maintenance.");

                    // Tambahkan kontrak sewa
                    SqlCommand insertKontrak = new SqlCommand(@"
                INSERT INTO KontrakSewa (
                    UnitID, NamaPenyewa, NoKTP, NoTelepon, AlamatPenyewa,
                    TanggalMulai, TanggalSelesai, StatusKontrak
                )
                VALUES (
                    @UnitID, @Nama, @KTP, @Telepon, @Alamat,
                    @Mulai, @Selesai, @Status
                )", conn, trans);

                    insertKontrak.Parameters.AddWithValue("@UnitID", unitID);
                    insertKontrak.Parameters.AddWithValue("@Nama", txtNamaPenyewa.Text);
                    insertKontrak.Parameters.AddWithValue("@KTP", txtKTP.Text);
                    insertKontrak.Parameters.AddWithValue("@Telepon", txtTelepon.Text);
                    insertKontrak.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                    insertKontrak.Parameters.AddWithValue("@Mulai", dateMulai.Value);
                    insertKontrak.Parameters.AddWithValue("@Selesai", dateSelesai.Value);
                    insertKontrak.Parameters.AddWithValue("@Status", comboBoxStatus.Text);
                    insertKontrak.ExecuteNonQuery();

                    // Update status unit menjadi 'Disewa' melalui SP
                    SqlCommand updateStatus = new SqlCommand("EXEC spUpdateStatusUnitSewa @UnitID, @StatusBaru", conn, trans);
                    updateStatus.Parameters.AddWithValue("@UnitID", unitID);
                    updateStatus.Parameters.AddWithValue("@StatusBaru", "Disewa");
                    updateStatus.ExecuteNonQuery();

                    trans.Commit();
                    MessageBox.Show("Kontrak berhasil ditambahkan dan status unit diperbarui.");
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Gagal menambahkan kontrak: " + ex.Message);
                }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (comboBoxNomorUnit.SelectedIndex <= 0)
            {
                MessageBox.Show("Silakan pilih Nomor Unit terlebih dahulu.");
                return;
            }

            if (dgvKontrakPenyewa.CurrentRow != null)
            {
                int kontrakID = Convert.ToInt32(dgvKontrakPenyewa.CurrentRow.Cells["KontrakID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE KontrakSewa SET 
                                UnitID = @UnitID,
                                NamaPenyewa = @Nama,
                                NoKTP = @KTP,
                                NoTelepon = @Telepon,
                                AlamatPenyewa = @Alamat,
                                TanggalMulai = @Mulai,
                                TanggalSelesai = @Selesai,
                                StatusKontrak = @Status
                                WHERE KontrakID = @ID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UnitID", comboBoxNomorUnit.SelectedValue);
                    cmd.Parameters.AddWithValue("@Nama", txtNamaPenyewa.Text);
                    cmd.Parameters.AddWithValue("@KTP", txtKTP.Text);
                    cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text);
                    cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                    cmd.Parameters.AddWithValue("@Mulai", dateMulai.Value);
                    cmd.Parameters.AddWithValue("@Selesai", dateSelesai.Value);
                    cmd.Parameters.AddWithValue("@Status", comboBoxStatus.Text);
                    cmd.Parameters.AddWithValue("@ID", kontrakID);

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
            ClearForm();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            MainPage mainpage = new MainPage();
            mainpage.Show();
            this.Hide();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvKontrakPenyewa.CurrentRow != null)
            {
                var result = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;

                int kontrakID = Convert.ToInt32(dgvKontrakPenyewa.CurrentRow.Cells["KontrakID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM KontrakSewa WHERE KontrakID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", kontrakID);

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

        private void dgvKontrakPenyewa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKontrakPenyewa.Rows[e.RowIndex];

                // Safe helper function to get string value
                string GetCellString(string columnName)
                {
                    return row.Cells[columnName].Value != DBNull.Value ? row.Cells[columnName].Value.ToString() : string.Empty;
                }

                // Safe helper function to get DateTime value
                DateTime GetCellDate(string columnName)
                {
                    return row.Cells[columnName].Value != DBNull.Value ? Convert.ToDateTime(row.Cells[columnName].Value) : DateTime.Now;
                }

                // Safe helper function to get object (for comboBox SelectedValue)
                object GetCellValue(string columnName)
                {
                    return row.Cells[columnName].Value != DBNull.Value ? row.Cells[columnName].Value : null;
                }

                comboBoxNomorUnit.SelectedValue = GetCellValue("UnitID");
                txtNamaPenyewa.Text = GetCellString("NamaPenyewa");
                txtKTP.Text = GetCellString("NoKTP");
                txtTelepon.Text = GetCellString("NoTelepon");
                txtAlamat.Text = GetCellString("AlamatPenyewa");
                dateMulai.Value = GetCellDate("TanggalMulai");
                dateSelesai.Value = GetCellDate("TanggalSelesai");
                comboBoxStatus.Text = GetCellString("StatusKontrak");
            }
        }

        private void comboBoxNomorUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxNomorUnit.SelectedValue == null || comboBoxNomorUnit.SelectedIndex < 0)
            {
                txtTipeKamar.Clear();
                return;
            }

            int unitID = Convert.ToInt32(comboBoxNomorUnit.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Tipe FROM UnitApartemen WHERE UnitID = @UnitID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UnitID", unitID);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        txtTipeKamar.Text = result.ToString();
                    }
                    else
                    {
                        txtTipeKamar.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil tipe kamar: " + ex.Message);
                }
            }
        }

        private void Analyze_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                COUNT(*) AS JumlahKontrakAktif, 
                AVG(DATEDIFF(DAY, TanggalMulai, TanggalSelesai)) AS DurasiRataRataKontrak
            FROM KontrakSewa
            WHERE StatusKontrak = 'Aktif'";

                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int jumlahKontrakAktif = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        int durasiRataRataKontrak = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);

                        MessageBox.Show(
                            $"Jumlah Kontrak Aktif: {jumlahKontrakAktif}\n" +
                            $"Durasi Rata-Rata Kontrak (hari): {durasiRataRataKontrak}",
                            "Analisis Data Kontrak",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Tidak ada data kontrak yang ditemukan untuk analisis.", "Analisis Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat melakukan analisis: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ManageKontrakPenyewa_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormReportKontrak reportKontrak = new FormReportKontrak();
            reportKontrak.Show();
            this.Hide();
        }
    }
}