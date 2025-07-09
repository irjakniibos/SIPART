using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPART_LAST
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Resize += (s, e) => CenterPanel();
            CenterPanel();

            this.Resize += MainPage_Resize;
        }

        private void CenterPanel()
        {
            // Panel di tengah horizontal, tetapi tetap di atas (misal 40px dari atas)
            panelCenter.Left = (this.ClientSize.Width - panelCenter.Width) / 2;
            panelCenter.Top = 40; // Jarak dari atas, bisa diubah sesuai kebutuhan
        }
        private void MainPage_Resize(object sender, EventArgs e)
        {
            // Contoh: semakin lebar form, semakin besar font
            // 30 bisa diganti sesuai kebutuhan agar proporsinya pas
            int fontSize = Math.Max(12, this.ClientSize.Width / 60);
            label1.Font = new Font("Gill Sans Ultra Bold", fontSize);

            // Untuk label subjudul
            int fontSizeSub = Math.Max(12, this.ClientSize.Width / 60);
            label2.Font = new Font("Gill Sans Ultra Bold", fontSizeSub);
        }
        private void btnManageApart(object sender, EventArgs e)
        {
            ManageApart manageapart = new ManageApart();
            manageapart.Show();
            this.Hide();
        }

        private void btnManageKontrakPenyewa(object sender, EventArgs e)
        {
            ManageKontrakPenyewa manageKontrakPenyewa = new ManageKontrakPenyewa();
            manageKontrakPenyewa.Show();
            this.Hide();
        }

        private void btnManageTagihanPembayaran(object sender, EventArgs e)
        {
            ManageTagihanPembayaran manageTagihanPembayaran = new ManageTagihanPembayaran();
            manageTagihanPembayaran.Show();
            this.Hide();
        }
    }
}
