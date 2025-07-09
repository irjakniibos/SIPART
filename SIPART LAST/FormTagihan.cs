using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPART_LAST
{
    public partial class FormTagihan : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";

        public FormTagihan()
        {
            InitializeComponent();
            strKonek = kn.connectionString();

            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Resize += (s, e) => panelTagihan();
            panelTagihan();
        }

        private void panelTagihan()
        {
            // Panel di tengah horizontal, tetapi tetap di atas (misal 40px dari atas)
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = 40; // Jarak dari atas, bisa diubah sesuai kebutuhan
        }

        private void FormTagihan_Load(object sender, EventArgs e)
        {
            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer(string namaPenyewa = "")
        {
            // Connection string ke database, ganti sesuai dengan yang kamu pakai
            strKonek = kn.connectionString();
            // Query, filter jika namaPenyewa tidak kosong
            string query = @"
        SELECT 
            u.UnitID, 
            u.NomorUnit, 
            u.Tipe, 
            u.HargaSewa, 
            u.Status AS UnitStatus,
            k.KontrakID, 
            k.NamaPenyewa, 
            k.NoKTP, 
            k.NoTelepon, 
            k.AlamatPenyewa, 
            k.TanggalMulai, 
            k.TanggalSelesai, 
            k.StatusKontrak,
            tp.TagihanID, 
            tp.JumlahTagihan, 
            tp.TanggalTerbit, 
            tp.TanggalJatuhTempo, 
            tp.StatusTagihan, 
            tp.TanggalPembayaran, 
            tp.JumlahBayar, 
            tp.MetodePembayaran
        FROM 
            UnitApartemen u
        JOIN 
            KontrakSewa k ON u.UnitID = k.UnitID
        LEFT JOIN 
            TagihanPembayaran tp ON k.KontrakID = tp.KontrakID
        WHERE 1=1
    ";

            if (!string.IsNullOrWhiteSpace(namaPenyewa))
            {
                query += " AND k.NamaPenyewa LIKE @NamaPenyewa";
            }

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(namaPenyewa))
                    {
                        cmd.Parameters.AddWithValue("@NamaPenyewa", "%" + namaPenyewa + "%");
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            // Setelah DataTable dt diisi:
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetTagihan", dt));
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportTagihanPembayaran.rdlc");
            reportViewer1.RefreshReport();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Data tidak ditemukan untuk penyewa: " + namaPenyewa, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportReport(string format)
        {
            try
            {
                // Ambil nama penyewa dari TextBox atau sumber input kamu
                string namaPenyewa = txtNamaPenyewa.Text.Trim();

                // Update isi ReportViewer dengan filter nama penyewa
                SetupReportViewer(namaPenyewa);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                // Pastikan ReportViewer sudah di-refresh sebelum render
                reportViewer1.RefreshReport();

                byte[] bytes = reportViewer1.LocalReport.Render(
                    format, null, out mimeType, out encoding, out extension,
                    out streamids, out warnings);

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = $"{format.ToUpper()} files (.{extension})|.{extension}";
                saveDialog.DefaultExt = extension;
                saveDialog.FileName = $"Tagihan_{namaPenyewa}_{DateTime.Now:yyyyMMddHHmmss}.{extension}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(saveDialog.FileName, bytes);
                    MessageBox.Show($"Report exported successfully to {saveDialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportReport("PDF");
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportReport("EXCEL");
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            SetupReportViewer(txtNamaPenyewa.Text.Trim());
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ManageTagihanPembayaran mainpage = new ManageTagihanPembayaran();
            mainpage.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
