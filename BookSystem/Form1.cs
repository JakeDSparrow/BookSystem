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
    public partial class frmLogin : Form
    {

        public string username, password;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Text;


            //dagdagan na lang to sa database, eseentially yung malalagay sa if statement here is yung select query for Admin table
            if (username == "admin")
            {
                frmAdmin frmAdmin = new frmAdmin();
                frmAdmin.ShowDialog();
                //this.Close();
            }
            else 
            {
                frmUser frmUser = new frmUser();
                frmUser.ShowDialog();
                //this.Close();
            }
        }
    }
}
