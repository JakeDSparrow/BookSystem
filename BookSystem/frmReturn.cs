using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookSystem
{
    public partial class frmReturn : Form
    {
        private string _username;
        public frmReturn(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            //FUNCTION FOR UPDATE
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UserDashboard dashboard = new UserDashboard(_username);
            this.Close();
            dashboard.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            UserDashboard dashboard = new UserDashboard(_username);
            this.Close();
            dashboard.Show();
        }
    }
}
