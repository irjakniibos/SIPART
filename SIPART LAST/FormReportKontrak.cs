using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIPART_LAST
{
    public partial class FormReportKontrak : Form
    {
        public FormReportKontrak()
        {
            InitializeComponent();
        }

        private void FormReportKontrak_Load(object sender, EventArgs e)
        {
            // Setup the ReportViewer with data
            SetupReportViewer();
            // Refresh the ReportViewer to show data
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            try
            {
                // Connection string to your database
                string connectionString = "Data Source=LAPTOP-9IG4E42U\\IRZALUVSALMA;" + "Initial Catalog=SIPART;Integrated Security=True";

                // SQL query to retrieve the required data from the database
                string query = @"
                    SELECT 
                        k.KontrakID, 
                        k.NamaPenyewa, 
                        k.NoKTP, 
                        k.NoTelepon, 
                        k.AlamatPenyewa, 
                        k.TanggalMulai, 
                        k.TanggalSelesai, 
                        k.StatusKontrak, 
                        u.NomorUnit, 
                        u.Tipe, 
                        u.HargaSewa
                    FROM 
                        KontrakSewa k
                    JOIN 
                        UnitApartemen u ON k.UnitID = u.UnitID";

                // Create a DataTable to store the data
                DataTable dt = new DataTable();

                // Use SqlDataAdapter to fill the DataTable with data from the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                // Create a ReportDataSource
                ReportDataSource rds = new ReportDataSource("DataSet1", dt); // Ensure "DataSet1" matches the dataset name in the RDLC file

                // Clear any existing data sources and add the new data source
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Set the path to the report (.rdlc file)
                // Change this to the actual path of your RDLC file
                reportViewer1.LocalReport.ReportPath = @"D:\PABDDATA\MAINDATA\SIPART LAST\SIPART LAST\ReportKontrakpenyewa.rdlc";

                // Refresh the ReportViewer to show the updated report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportReport(string format)
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = reportViewer1.LocalReport.Render(
                    format, null, out mimeType, out encoding, out extension,
                    out streamids, out warnings);

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = $"{format.ToUpper()} files (*.{extension})|*.{extension}";
                saveDialog.DefaultExt = extension;

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


        // Optional: If you need additional logic for the ReportViewer load event
        private void reportViewer1_Load(object sender, EventArgs e)
        {
            // Add any necessary logic to handle the ReportViewer load event here if required
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportReport("EXCELOPENXML");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetupReportViewer();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ManageKontrakPenyewa ManageKontrakPenyewa = new ManageKontrakPenyewa();
            ManageKontrakPenyewa.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ExportReport("PDF");
        }
    }
}
