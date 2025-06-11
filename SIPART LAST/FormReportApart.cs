using Microsoft.Reporting.WinForms;
using SIPART_LAST;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRUDOrmas
{
    public partial class FormReportApart : Form
    {
        public FormReportApart()
        {
            InitializeComponent();
        }

        private void FormReportApart_Load(object sender, EventArgs e)
        {
            // Setup ReportViewer data
            SetupReportViewer();
            // Refresh report to display data
            this.reportViewer.RefreshReport();
        }

        private void SetupReportViewer()
        {
            try
            {
                // Connection string to your database
                string connectionString = "Data Source=LAPTOP-9IG4E42U\\IRZALUVSALMA;Initial Catalog=SIPART;Integrated Security=True";

                // SQL query to retrieve the required data from the database
                string query = @"
                    SELECT ua.UnitID, ua.NomorUnit, ua.Tipe, ua.Status, ks.KontrakID, ks.NamaPenyewa, ks.TanggalMulai, ks.TanggalSelesai
                    FROM UnitApartemen ua
                    LEFT JOIN KontrakSewa ks ON ua.UnitID = ks.UnitID
                    WHERE ua.Status = 'Disewa'
                    ORDER BY ks.TanggalMulai DESC;";

                // Create a DataTable to store the data
                DataTable dt = new DataTable();

                // Use SqlDataAdapter to fill the DataTable with data from the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                // Create a ReportDataSource
                ReportDataSource rds = new ReportDataSource("DataSet1", dt); // Make sure "DataSet1" matches your RDLC dataset name

                // Clear any existing data sources and add the new data source
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(rds);

                // Set the path to the report (.rdlc file)
                // Change this to the actual path of your RDLC file
                reportViewer.LocalReport.ReportPath = @"D:\PABDDATA\MAINDATA\SIPART LAST\SIPART LAST\ApartReport.rdlc";

                // Refresh the ReportViewer to show the updated report
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Optional: Method to refresh report data
        private void RefreshReportData()
        {
            SetupReportViewer();
        }

        // Optional: Method to export report
        private void ExportReport(string format)
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = reportViewer.LocalReport.Render(
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

        // Event handlers for export buttons (if you have them)
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportReport("PDF");
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportReport("EXCELOPENXML");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshReportData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ManageApart manageapartemen = new ManageApart();
            manageapartemen.Show();
            this.Hide();
        }

        // Optional: Method to export report to Excel
        private void ExportToExcel()
        {
            try
            {
                // Export report to Excel file format
                ExportReport("EXCELOPENXML");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
