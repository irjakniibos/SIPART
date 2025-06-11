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

        private void MainPage_Load(object sender, EventArgs e)
        {

        }
    }
}
