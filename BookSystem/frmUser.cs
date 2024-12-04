using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BookSystem
{
    public partial class frmUser : Form
    {
        private string _username;

        public frmUser()
        {
        }

        public frmUser(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void lblBorrowbook_Click(object sender, EventArgs e)
        {
            frmBorrow frmBorrow = new frmBorrow();
            frmBorrow.ShowDialog();
        }

        private void lblReturnBook_Click(object sender, EventArgs e)
        {
            frmReturn frmReturn = new frmReturn(_username);
            frmReturn.ShowDialog();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            this.Close();
            frmLogin.Show();
        }
    }
}
