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
    public partial class frmBorrow : Form
    {
        public frmBorrow()
        {
            InitializeComponent();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {

            SqlConnectionClass connectionClass = new SqlConnectionClass();

            frmUser frmUser = new frmUser();
            frmUser.ShowDialog();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
