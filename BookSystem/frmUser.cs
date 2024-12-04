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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void lblBorrowbook_Click(object sender, EventArgs e)
        {
            frmBorrow frmBorrow = new frmBorrow();
            frmBorrow.ShowDialog();
        }

        private void lblReturnBook_Click(object sender, EventArgs e)
        {
            frmReturn frmReturn = new frmReturn();
            frmReturn.ShowDialog();
        }
    }
}
