using Microsoft.Reporting.WinForms;
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
    public partial class FormTagihan : Form
    {
        public FormTagihan()
        {
            InitializeComponent();
        }

        private void FormTagihan_Load(object sender, EventArgs e)
        {

            SetupReportViewer();
            this.reportViewer1.RefreshReport();

        }

        private void SetupReportViewer()
        {
            // Connection string to your database
            string connectionString = "Data Source=LAPTOP-9IG4E42U\\IRZALUVSALMA;Initial Catalog=SIPART;Integrated Security=True";

            // SQL query to retrieve the required data from the database
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
                    TagihanPembayaran tp ON k.KontrakID = tp.KontrakID";

            // Create a DataTable to store the data
            DataTable dt = new DataTable();

            // Use SqlDataAdapter to fill the DataTable with data from the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            // Create a ReportDataSource
            ReportDataSource rds = new ReportDataSource("DataSetTagihan", dt); // Ensure "DataSet1" matches the dataset name in the RDLC file

            // Clear any existing data sources and add the new data source
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Set the path to the report (.rdlc file)
            // Change this to the actual path of your RDLC file
            reportViewer1.LocalReport.ReportPath = @"D:\PABDDATA\MAINDATA\SIPART LAST\SIPART LAST\ReportTagihanPembayaran.rdlc";

            // Refresh the ReportViewer to show the updated report
            reportViewer1.RefreshReport();
        }


        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ManageTagihanPembayaran mainpage = new ManageTagihanPembayaran();
            mainpage.Show();
            this.Hide();

        }
    }
}
